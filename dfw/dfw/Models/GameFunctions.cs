using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dfw.Models
{
    public partial class Game
    {
        private bool AddPlayer()
        {
            Player newPlayer = new Player();
            newPlayer.Id = (Players.Count + 1).ToString();
            Console.WriteLine("请输入玩家姓名：");
            newPlayer.Name = Console.ReadLine();
            Players.Add(newPlayer);
            GameLogs newPlayerLog = new GameLogs()
            {
                ID = GetLogID(),
                Type = LogEventType.AddPlayer,
                PlayerName = newPlayer.Name,
                Info = $"ID : {newPlayer.Id}",
                Round = Round,
                currentPlayer = currentPlayer
            };
            AddLog(newPlayerLog);
            return true;
        }

        private bool Move(bool moveNext = true)
        {

            var player = currentPlayer;
            Console.WriteLine($"玩家 {player.Name} 回合: ");
            int dice = -1;
            while(dice > 6 || dice < 1)
            {
                Console.WriteLine("请输入玩家移动的距离：(1-6)");
                ReadIntNumber(out dice);
            }

            if (player == null)
            {
                Console.WriteLine("玩家信息不存在，请重新输入：");
                return false;
            }
            if (player.Stop == true)
            {
                return HolidayEnd(player);
            }
            GameLogs moveLog = new GameLogs()
            {
                ID = GetLogID(),
                Type = LogEventType.Move,
                PlayerName = player.Name,
                StartPoint = player.PositionNumber,
                EndPoint = (player.PositionNumber + dice) % 36,
                Info = "玩家移动",
                Round = Round,
                currentPlayer = currentPlayer
            };
            AddLog(moveLog);


            player.PositionNumber += dice;
            int roundAdd = player.PositionNumber / 36;
            player.PositionNumber = player.PositionNumber % 36;

            if (roundAdd > 0)
            {
                RoundReward(player);
            }
            var positionCard = GameBoard.BoardMap[player.PositionNumber].PositionCard;

            if (positionCard.Type == CardType.Idol)
            {
                if (!string.Equals(positionCard.HolderId, player.Id) && !string.Equals(positionCard.HolderId, "-1"))
                {
                    var playerTo = Players.FirstOrDefault(p => p.Id == positionCard.HolderId);
                    if (playerTo.CanWin == true)
                    {
                        var fee = positionCard.Fee[positionCard.Level];
                        var cpFee = CPManager.CPCost(GameBoard, player.PositionNumber);
                        GoldExchange(player, playerTo, fee);
                        CPFee(player, playerTo, cpFee);
                    }
                    else
                    {
                        Console.WriteLine($"玩家{playerTo.Name} 处于休息状态，无法获得收入。");
                    }
                }
                else if (string.Equals(positionCard.HolderId, player.Id))
                {
                    if (positionCard.Level < 4)
                    {
                        bool loop = true;
                        while (loop)
                        {
                            Console.WriteLine("是否进行艺人升级？（Y/N）");
                            string YesOrNo = Console.ReadLine();
                            switch (YesOrNo.ToUpper())
                            {
                                case "Y":
                                    IdolLevelUp(player);
                                    loop = false;
                                    break;
                                case "YES":
                                    IdolLevelUp(player);
                                    loop = false;
                                    break;
                                case "N":
                                    Console.WriteLine("不进行升级。");
                                    loop = false;
                                    break;
                                case "NO":
                                    Console.WriteLine("不进行升级。");
                                    loop = false;
                                    break;
                            }
                        }
                    }
                }
                else if(string.Equals(positionCard.HolderId, "-1"))
                {
                    bool loop = true;
                    while (loop)
                    {
                        Console.WriteLine("该艺人没有所属公司，是否签约艺人？（Y/N）");
                        string YesOrNo = Console.ReadLine();
                        switch (YesOrNo.ToUpper())
                        {
                            case "Y":
                                IdolContract(player);
                                loop = false;
                                break;
                            case "YES":
                                IdolContract(player);
                                loop = false;
                                break;
                            case "N":
                                Console.WriteLine("不进行签约。");
                                loop = false;
                                break;
                            case "NO":
                                Console.WriteLine("不进行签约。");
                                loop = false;
                                break;
                        }
                    }
                }
            }
            else if (positionCard.Type == CardType.Special)
            {
                if (!string.Equals(positionCard.HolderId, player.Id) && !string.Equals(positionCard.HolderId, "-1"))
                {
                    var playerTo = Players.FirstOrDefault(p => p.Id == positionCard.HolderId);
                    if (playerTo.CanWin == true)
                    {
                        int fee = 0;
                        var specialHolds = GameBoard.BoardMap.Where(x => x.PositionCard.Type == CardType.Special && x.PositionCard.HolderId == positionCard.HolderId).ToList();
                        switch (specialHolds.Count)
                        {
                            case 1:
                                fee = 1000;
                                break;
                            case 2:
                                fee = 3000;
                                break;
                            case 3:
                                fee = 6000;
                                break;
                            case 4:
                                fee = 10000;
                                break;
                            default:
                                break;
                        }
                        GoldExchange(player, playerTo, fee);
                    }
                    else
                    {
                        Console.WriteLine($"玩家{playerTo.Name} 处于休息状态，无法获得收入。");
                    }
                }
            }
            else if (positionCard.Type == CardType.Back)
            {
                BackHome(player);
            }
            else if (positionCard.Type == CardType.Holiday)
            {
                Holiday(player);
            }
            else if (positionCard.Type == CardType.Education)
            {
                GameLogs eduLog = new GameLogs()
                {
                    ID = GetLogID(),
                    Type = LogEventType.Education,
                    PlayerName = player.Name,
                    Info = "深造进修，再玩一次",
                    Round = Round,
                    currentPlayer = currentPlayer
                };
                AddLog(eduLog);
                Move(false);
            }
            else if (positionCard.Type == CardType.Chance)
            {
                ChanceCardWin(player);
            }
            else if (positionCard.Type == CardType.Change)
            {
                ChangeCardTrigger(player);
            }
            if (moveNext)
            {
                PlayerMoved++;
                Round = PlayerMoved / Players.Count;
            }


            return true;
        }

        private bool RoundReward(Player player)
        {
            player.Gold += 2000;
            GameLogs rewardLog = new GameLogs()
            {
                ID = GetLogID(),
                Type = LogEventType.RoundReward,
                PlayerName = player.Name,
                GoldWin = 2000,
                Info = "起点奖励",
                Round = Round,
                currentPlayer = currentPlayer
            };
            AddLog(rewardLog);

            return true;
        }

        private bool IdolContract(Player player = null)
        {
            if (player == null)
            {
                while (true)
                {
                    Console.WriteLine("请输入使用机会卡的玩家ID：");
                    string playerId = Console.ReadLine();
                    player = Players.FirstOrDefault(p => p.Id == playerId || string.Equals(p.Name, playerId, StringComparison.OrdinalIgnoreCase));
                    if (player == null)
                    {
                        Console.WriteLine("该玩家不存在，请重新输入：");
                        continue;
                    }
                    break;
                }
            }
            int position = player.PositionNumber;

            if (GameBoard.BoardMap[position].PositionCard.Type != CardType.Idol && GameBoard.BoardMap[position].PositionCard.Type != CardType.Special)
            {
                Console.WriteLine("非艺人或特殊产业，不得买卖。");
                return false;
            }
            if (GameBoard.BoardMap[position].PositionCard.HolderId != "-1" &&
                GameBoard.BoardMap[position].PositionCard.HolderId == player.Id)
            {
                Console.WriteLine("该艺人或特殊产业已属于当前玩家，不得重复购买。");
                return false;
            }

            if (GameBoard.BoardMap[position].PositionCard.HolderId != "-1" &&
                GameBoard.BoardMap[position].PositionCard.HolderId != player.Id)
            {
                Console.WriteLine("该艺人或特殊产业已属于其他玩家，不得强制购买。");
                return false;
            }
            if (player.Gold < GameBoard.BoardMap[position].PositionCard.InitCost)
            {
                Console.WriteLine("玩家现金不够，无法购买该艺人。");
                return false;
            }
            GameBoard.BoardMap[position].PositionCard.HolderId = player.Id;
            player.Gold = player.Gold - GameBoard.BoardMap[position].PositionCard.InitCost;

            GameLogs signContractLog = new GameLogs()
            {
                ID = GetLogID(),
                Type = LogEventType.IdolContract,
                PlayerName = player.Name,
                CardEvent = GameBoard.BoardMap[position].PositionCard,
                Info = "购买艺人或特殊产业",
                Round = Round,
                currentPlayer = currentPlayer
            };
            AddLog(signContractLog);

            return true;
        }

        private bool IdolLevelUp(Player player = null)
        {
            if (player == null)
            {
                while (true)
                {
                    Console.WriteLine("请输入使用机会卡的玩家ID：");
                    string playerId = Console.ReadLine();
                    player = Players.FirstOrDefault(p => p.Id == playerId || string.Equals(p.Name, playerId, StringComparison.OrdinalIgnoreCase));
                    if (player == null)
                    {
                        Console.WriteLine("该玩家不存在，请重新输入：");
                        continue;
                    }
                    break;
                }
            }
            int position = player.PositionNumber;

            if (GameBoard.BoardMap[position].PositionCard.Type != CardType.Idol)
            {
                Console.WriteLine("指定位置非艺人，不能升级。");
                return false;
            }
            if (GameBoard.BoardMap[position].PositionCard.HolderId != player.Id)
            {
                Console.WriteLine("该艺人不属于当前玩家。");
                return false;
            }

            if (GameBoard.BoardMap[position].PositionCard.Level >= 4)
            {
                Console.WriteLine("该艺人已达到满级。");
                return false;
            }
            if (player.Gold < GameBoard.BoardMap[position].PositionCard.LevelUpCost)
            {
                Console.WriteLine("玩家现金不够，无法升级该艺人。");
                return false;
            }

            GameBoard.BoardMap[position].PositionCard.Level++;
            player.Gold = player.Gold - GameBoard.BoardMap[position].PositionCard.LevelUpCost;

            GameLogs LevelUpLog = new GameLogs()
            {
                ID = GetLogID(),
                Type = LogEventType.LevelUp,
                PlayerName = player.Name,
                CardEvent = GameBoard.BoardMap[position].PositionCard,
                LevelChangePositionNo = position,
                LevelUpFrom = GameBoard.BoardMap[position].PositionCard.Level - 1,
                LevelUpTo = GameBoard.BoardMap[position].PositionCard.Level,
                Info = "艺人升级",
                Round = Round,
                currentPlayer = currentPlayer
            };
            AddLog(LevelUpLog);

            return true;
        }

        private bool GoldExchange(Player playerFrom, Player playerTo, int gold)
        {
            GameLogs payLog = new GameLogs()
            {
                ID = GetLogID(),
                Type = LogEventType.GoldChange,
                PlayerName = playerFrom.Name,
                GoldWin = 0,
                GoldLoss = gold,
                Info = "支付金币",
                Round = Round,
                currentPlayer = currentPlayer
            };
            AddLog(payLog);

            GameLogs recieveLog = new GameLogs()
            {
                ID = GetLogID(),
                Type = LogEventType.GoldChange,
                PlayerName = playerTo.Name,
                GoldWin = gold,
                GoldLoss = 0,
                Info = "收获金币",
                Round = Round,
                currentPlayer = currentPlayer
            };
            AddLog(recieveLog);

            playerFrom.Gold -= gold;
            playerTo.Gold += gold;

            return true;
        }

        private bool CPFee(Player playerFrom, Player playerTo, int gold)
        {
            GameLogs payLog = new GameLogs()
            {
                ID = GetLogID(),
                Type = LogEventType.CPFee,
                PlayerName = playerFrom.Name,
                GoldWin = 0,
                GoldLoss = gold,
                Info = "支付CP费",
                Round = Round,
                currentPlayer = currentPlayer
            };
            AddLog(payLog);

            GameLogs recieveLog = new GameLogs()
            {
                ID = GetLogID(),
                Type = LogEventType.CPFee,
                PlayerName = playerTo.Name,
                GoldWin = gold,
                GoldLoss = 0,
                Info = "支付CP费",
                Round = Round,
                currentPlayer = currentPlayer
            };
            AddLog(recieveLog);

            playerFrom.Gold -= gold;
            playerTo.Gold += gold;

            return true;
        }

        private bool ChanceCardWin(Player player = null)
        {
            if (ChanceDeck.Count == 0)
            {
                ChanceDeck = InitChanceDeck();
            }
            if (player == null)
            {
                while (true)
                {
                    Console.WriteLine("请输入使用机会卡的玩家ID：");
                    string playerId = Console.ReadLine();
                    player = Players.FirstOrDefault(p => p.Id == playerId || string.Equals(p.Name, playerId, StringComparison.OrdinalIgnoreCase));
                    if (player == null)
                    {
                        Console.WriteLine("该玩家不存在，请重新输入：");
                        continue;
                    }
                    break;
                }
            }
            int cardNumber = PickChanceCard();
            Console.WriteLine($"触发的机会卡ID为：{cardNumber}");


            if (ChanceDeck.Contains(cardNumber))
            {
                ChanceDeck.Remove(cardNumber);
                player.ChanceCards.Add(cardNumber);
                GameLogs chanceCardWinLog = new GameLogs()
                {
                    ID = GetLogID(),
                    Type = LogEventType.ChanceCardWin,
                    PlayerName = player.Name,
                    ChanceCardWin = cardNumber,
                    Info = "获得机会卡",
                    Round = Round,
                    currentPlayer = currentPlayer
                };
                AddLog(chanceCardWinLog);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ChanceCardUse()
        {
            Player player = new Player();
            while (true)
            {
                Console.WriteLine("请输入使用机会卡的玩家ID：");
                string playerId = Console.ReadLine();
                player = Players.FirstOrDefault(p => p.Id == playerId || string.Equals(p.Name, playerId, StringComparison.OrdinalIgnoreCase));
                if(player == null)
                {
                    Console.WriteLine("该玩家不存在，请重新输入：");
                    continue;
                }
                break;
            }
            int cardNumber = -1;
            while (true)
            {
                Console.WriteLine("请输入机会卡ID：");
                ReadIntNumber(out cardNumber);
                if (player.ChanceCards.Contains(cardNumber))
                    break;
                
            }

            if (player.ChanceCards.Contains(cardNumber))
            {
                player.ChanceCards.Remove(cardNumber);
                ChanceUsed.Add(cardNumber);
                ChanceUsed = ChangeUsed.OrderBy(x => x).ToList();
                GameLogs chanceCardLossLog = new GameLogs()
                {
                    ID = GetLogID(),
                    Type = LogEventType.ChanceCardUse,
                    PlayerName = player.Name,
                    ChanceCardUse = cardNumber,
                    Info = "使用机会卡",
                    Round = Round,
                    currentPlayer = currentPlayer
                };
                AddLog(chanceCardLossLog);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ChangeCardTrigger(Player player = null)
        {
            if (ChangeDeck.Count == 0)
            {
                ChangeDeck = InitChangeDeck();
            }
            if (player == null)
            {
                while (true)
                {
                    Console.WriteLine("请输入使用机会卡的玩家ID：");
                    string playerId = Console.ReadLine();
                    player = Players.FirstOrDefault(p => p.Id == playerId || string.Equals(p.Name, playerId, StringComparison.OrdinalIgnoreCase));
                    if (player == null)
                    {
                        Console.WriteLine("该玩家不存在，请重新输入：");
                        continue;
                    }
                    break;
                }
            }
            int cardNumber = PickChangeCard();
            Console.WriteLine($"触发的命运卡ID为：{cardNumber}");

            if (ChangeDeck.Contains(cardNumber))
            {
                ChangeDeck.Remove(cardNumber);
                ChangeUsed.Add(cardNumber);
                GameLogs changeCardTrigger = new GameLogs()
                {
                    ID = GetLogID(),
                    Type = LogEventType.ChangeCardTrigger,
                    PlayerName = player.Name,
                    ChangeCardTrigger = cardNumber,
                    Info = "触发命运卡",
                    Round = Round,
                    currentPlayer = currentPlayer
                };
                AddLog(changeCardTrigger);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool BackHome(Player player)
        {
            GameLogs backHomeLog = new GameLogs()
            {
                ID = GetLogID(),
                Type = LogEventType.BackHome,
                PlayerName = player.Name,
                StartPoint = player.PositionNumber,
                EndPoint = 0,
                Info = "【回家过年】直接回到起点，并获得一张【命运】卡，但不能领取起点奖励。",
                Round = Round,
                currentPlayer = currentPlayer
            };
            player.PositionNumber = 0;
            AddLog(backHomeLog);
            return ChangeCardTrigger(player);
        }

        private bool Holiday(Player player)
        {
            GameLogs holidayLog = new GameLogs()
            {
                ID = GetLogID(),
                Type = LogEventType.Holiday,
                PlayerName = player.Name,
                Info = "【出国看秀】休息一轮，并可以抽取一张【机会】卡，休息期间无收益。",
                Round = Round,
                currentPlayer = currentPlayer
            };
            AddLog(holidayLog);
            player.Stop = true;
            player.CanWin = false;
            return ChanceCardWin(player);
        }
        private bool HolidayEnd(Player player)
        {
            GameLogs holidayLog = new GameLogs()
            {
                ID = GetLogID(),
                Type = LogEventType.HolidayEnd,
                PlayerName = player.Name,
                Info = "休息状态结束",
                Round = Round,
                currentPlayer = currentPlayer
            };
            AddLog(holidayLog);
            player.Stop = false;
            player.CanWin = true;
            return true;
        }

        #region Data Fix
        private bool PlayerNameFix()
        {
            Player player;
            while (true)
            {
                Console.WriteLine("请输入使用机会卡的玩家ID：");
                string playerId = Console.ReadLine();
                player = Players.FirstOrDefault(p => p.Id == playerId || string.Equals(p.Name, playerId, StringComparison.OrdinalIgnoreCase));
                if (player == null)
                {
                    Console.WriteLine("该玩家不存在，请重新输入：");
                    continue;
                }
                break;
            }
            Console.WriteLine("请输入玩家新昵称：");
            string playerName = Console.ReadLine();
            GameLogs playerFixLog = new GameLogs()
            {
                ID = GetLogID(),
                Type = LogEventType.PlayerFix,
                PlayerName = player.Name,
                Info = $"玩家信息修正 {player.Name} to {playerName}",
                Round = Round,
                currentPlayer = currentPlayer
            };
            AddLog(playerFixLog);
            player.Name = playerName;

            return true;
        }

        private bool PlayerPositionFix()
        {
            Player player;
            while (true)
            {
                Console.WriteLine("请输入使用机会卡的玩家ID：");
                string playerId = Console.ReadLine();
                player = Players.FirstOrDefault(p => p.Id == playerId || string.Equals(p.Name, playerId, StringComparison.OrdinalIgnoreCase));
                if (player == null)
                {
                    Console.WriteLine("该玩家不存在，请重新输入：");
                    continue;
                }
                break;
            }

            int positionNumber = - 1;
            while(positionNumber < 0 || positionNumber > 35)
            {
                Console.WriteLine("请输入玩家的正确位置：");
                ReadIntNumber(out positionNumber);
            }
            GameLogs positionFixLog = new GameLogs()
            {
                ID = GetLogID(),
                Type = LogEventType.PositionFix,
                PlayerName = player.Name,
                StartPoint = player.PositionNumber,
                EndPoint = positionNumber,
                Info = $"玩家位置修正 {player.PositionNumber} to {positionNumber}",
                Round = Round,
                currentPlayer = currentPlayer
            };
            AddLog(positionFixLog);
            player.PositionNumber = positionNumber;

            return true;
        }

        private bool GoldFix()
        {
            Player player;
            while (true)
            {
                Console.WriteLine("请输入使用机会卡的玩家ID：");
                string playerId = Console.ReadLine();
                player = Players.FirstOrDefault(p => p.Id == playerId || string.Equals(p.Name, playerId, StringComparison.OrdinalIgnoreCase));
                if (player == null)
                {
                    Console.WriteLine("该玩家不存在，请重新输入：");
                    continue;
                }
                break;
            }
            int gold = -99999;
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("请输入玩家的正确金币：");
                ReadIntNumber(out gold);
                Console.WriteLine($"请确认的金币数额：{gold} (Y/N)");
                string YesOrNo = Console.ReadLine();
                switch (YesOrNo.ToUpper())
                {
                    case "Y":
                        Console.WriteLine($"确认为：{gold}");
                        loop = false;
                        break;
                    case "YES":
                        Console.WriteLine($"确认为：{gold}");
                        loop = false;
                        break;
                    case "N":
                        Console.WriteLine($"重新输入");
                        loop = true;
                        break;
                    case "NO":
                        Console.WriteLine("重新输入");
                        loop = true;
                        break;
                }

            }
            
            GameLogs positionFixLog = new GameLogs()
            {
                ID = GetLogID(),
                Type = LogEventType.GoldFix,
                PlayerName = player.Name,
                Info = $"金币修正 {player.Gold} to {gold}",
                Round = Round,
                currentPlayer = currentPlayer
            };
            AddLog(positionFixLog);
            player.Gold = gold;

            return true;
        }

        private bool LevelFix()
        {
            int positionNumber = -1;
            while (positionNumber < 0 || positionNumber > 35)
            {
                Console.WriteLine("请输入艺人所在的位置：");
                ReadIntNumber(out positionNumber);
            }
            int level = -1;
            while (level < 0 || level > 4)
            {
                Console.WriteLine("请输入艺人的正确等级：");
                ReadIntNumber(out level);
            }

            var idol = GameBoard.BoardMap[positionNumber].PositionCard;
            if (idol.Type == CardType.Idol)
            {
                GameLogs positionFixLog = new GameLogs()
                {
                    ID = GetLogID(),
                    Type = LogEventType.LevelFix,
                    LevelChangePositionNo = positionNumber,
                    LevelUpFrom = idol.Level,
                    LevelUpTo = level,
                    Info = $"艺人等级修正{positionNumber} : lv{idol.Level} to lv{level}",
                    Round = Round,
                    currentPlayer = currentPlayer
                };
                AddLog(positionFixLog);
                idol.Level = level;
            }
            return true;
        }

        private bool IdolOwnerFix()
        {

            Console.WriteLine("请输入艺人所在的位置：");
            string input = Console.ReadLine();
            int positionNumber;
            Int32.TryParse(input, out positionNumber);
            Console.WriteLine("请输入艺人的正确拥有者：");
            string playerId = Console.ReadLine();

            var idol = GameBoard.BoardMap[positionNumber].PositionCard;
            if (idol.Type == CardType.Idol || idol.Type == CardType.Special)
            {
                GameLogs positionFixLog = new GameLogs()
                {
                    ID = GetLogID(),
                    Type = LogEventType.IdolOwnerFix,
                    LevelChangePositionNo = positionNumber,
                    Info = $"艺人归属修正{positionNumber} : from{idol.HolderId} to {playerId}",
                    Round = Round,
                    currentPlayer = currentPlayer
                };
                AddLog(positionFixLog);
                idol.HolderId = playerId;
            }
            return true;
        }
        #endregion
    }
}
