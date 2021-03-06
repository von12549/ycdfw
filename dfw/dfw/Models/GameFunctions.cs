﻿using System;
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

        private bool Move(bool moveNext = true, bool autoRun = false)
        {
            if (PlayerMoved == 0)
            {
                currentPlayer = Players[0];
            }
            else
            {
                currentPlayer = Players[PlayerMoved % Players.Count];
            }
            var player = currentPlayer;
            Console.WriteLine($"玩家 {player.Name} 回合: ");
            if (player.Stop == true)
            {
                return HolidayEnd(player);
            }
            int dice = -1;
            if (autoRun == true)
            {
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                dice = rnd.Next(1, 7);
            }
            else
            {
                while (dice > 6 || dice < 1)
                {
                    Console.WriteLine("请输入玩家移动的距离：(1-6)/(Exit 退出)");
                    bool notExit = ReadIntNumber(out dice);
                    if (!notExit)
                        return false;
                }
            }

            if (player == null)
            {
                Console.WriteLine("玩家信息不存在。");
                return false;
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
                    if (playerTo.CanWin == true && positionCard.isMortgage == false)
                    {
                        var fee = positionCard.Fee[positionCard.Level];
                        var cpFee = CPManager.CPCost(GameBoard, player.PositionNumber);
                        GoldExchange(player, playerTo, fee, cpFee);
                    }
                    else
                    {
                        if (playerTo.CanWin == false)
                        {
                            Console.WriteLine($"玩家{playerTo.Name} 处于休息状态，无法获得收入。");
                        }
                        if(positionCard.isMortgage == true)
                        {
                            Console.WriteLine($"艺人{player.PositionNumber}:{positionCard.Name} 处于抵押状态，无法获得收入。");
                        }
                    }
                }
                else if (string.Equals(positionCard.HolderId, player.Id))
                {
                    if (positionCard.Level < 4)
                    {
                        if (!autoRun)
                        {
                            bool loop = true;
                            while (loop)
                            {
                                Console.WriteLine("是否进行艺人升级？（Y/n）");
                                string YesOrNo = Console.ReadLine();
                                switch (YesOrNo.Trim().ToUpper())
                                {
                                    case "":
                                        IdolLevelUp(player);
                                        loop = false;
                                        break;
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
                        else
                        {
                            IdolLevelUp(player);
                        }
                    }
                }
                else if (string.Equals(positionCard.HolderId, "-1"))
                {
                    if (!autoRun)
                    {
                        bool loop = true;
                        while (loop)
                        {
                            Console.WriteLine("该艺人没有所属公司，是否签约艺人？（Y/n）");
                            string YesOrNo = Console.ReadLine();

                            switch (YesOrNo.Trim().ToUpper())
                            {
                                case "":
                                    IdolContract(player);
                                    loop = false;
                                    break;
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
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        IdolContract(player);
                    }
                }
            }
            else if (positionCard.Type == CardType.Special)
            {
                if (!string.Equals(positionCard.HolderId, player.Id) && !string.Equals(positionCard.HolderId, "-1"))
                {
                    var playerTo = Players.FirstOrDefault(p => p.Id == positionCard.HolderId);
                    if (playerTo.CanWin == true && positionCard.isMortgage == false)
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
                        GoldExchange(player, playerTo, fee, 0);
                    }
                    else
                    {
                        if (playerTo.CanWin == false)
                        {
                            Console.WriteLine($"玩家{playerTo.Name} 处于休息状态，无法获得收入。");
                        }
                        if (positionCard.isMortgage == true)
                        {
                            Console.WriteLine($"产业{player.PositionNumber}:{positionCard.Name} 处于抵押状态，无法获得收入。");
                        }
                    }
                }
                else if (string.Equals(positionCard.HolderId, "-1"))
                {
                    if (!autoRun)
                    {
                        bool loop = true;
                        while (loop)
                        {
                            Console.WriteLine("该产业没有所属公司，是否买下产业？（Y/n）");
                            string YesOrNo = Console.ReadLine();

                            switch (YesOrNo.Trim().ToUpper())
                            {
                                case "":
                                    IdolContract(player);
                                    loop = false;
                                    break;
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
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        IdolContract(player);
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
                Move(false, autoRun);
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
                    Console.WriteLine("请输入使用机会卡的玩家ID：(Exit 退出)");
                    string playerId = Console.ReadLine().Trim();
                    if (string.Equals(playerId, "exit", StringComparison.OrdinalIgnoreCase))
                        return false;
                    player = Players.FirstOrDefault(p => p.Id == playerId || string.Equals(p.Name, playerId, StringComparison.OrdinalIgnoreCase));
                    if (player == null)
                    {
                        Console.WriteLine("该玩家不存在。");
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
                Console.WriteLine("玩家现金不够，无法购买该艺人或特殊产业。");
                return false;
            }
            if(MinContract != null && player.Gold < MinContract)
            {
                Console.WriteLine($"玩家现金低于 ${MinContract}，无法购买该艺人或特殊产业。");
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
                    Console.WriteLine("请输入使用机会卡的玩家ID：(Exit 退出)");
                    string playerId = Console.ReadLine().Trim();
                    if (string.Equals(playerId, "exit", StringComparison.OrdinalIgnoreCase))
                        return false;
                    player = Players.FirstOrDefault(p => p.Id == playerId || string.Equals(p.Name, playerId, StringComparison.OrdinalIgnoreCase));
                    if (player == null)
                    {
                        Console.WriteLine("该玩家不存在。");
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
            if(MinLevelUp != null && player.Gold < MinLevelUp)
            {
                Console.WriteLine($"玩家现金低于 ${MinContract}，无法购买该艺人或特殊产业。");
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

        private bool GoldExchange(Player playerFrom, Player playerTo, int gold, int cp)
        {
            GameLogs payLog = new GameLogs()
            {
                ID = GetLogID(),
                Type = LogEventType.GoldChange,
                PlayerName = playerFrom.Name,
                GoldWin = 0,
                GoldLoss = gold,
                CpWin = 0,
                CpLoss = cp,
                Info = "支付金币",
                Round = Round,
                currentPlayer = currentPlayer,
                PayFrom = playerFrom.Name,
                PayTo = playerTo.Name
            };
            AddLog(payLog);

            playerFrom.Gold -= gold;
            playerFrom.Gold -= cp;
            playerTo.Gold += gold;
            playerTo.Gold += cp;

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
                    Console.WriteLine("请输入使用机会卡的玩家ID：(Exit 退出)");
                    string playerId = Console.ReadLine().Trim();
                    if (string.Equals(playerId, "exit", StringComparison.OrdinalIgnoreCase))
                        return false;
                    player = Players.FirstOrDefault(p => p.Id == playerId || string.Equals(p.Name, playerId, StringComparison.OrdinalIgnoreCase));
                    if (player == null)
                    {
                        Console.WriteLine("该玩家不存在。");
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
                Console.WriteLine("请输入使用机会卡的玩家ID：(Exit 退出)");
                string playerId = Console.ReadLine().Trim();
                if (string.Equals(playerId, "exit", StringComparison.OrdinalIgnoreCase))
                    return false;
                player = Players.FirstOrDefault(p => p.Id == playerId || string.Equals(p.Name, playerId, StringComparison.OrdinalIgnoreCase));
                if(player == null)
                {
                    Console.WriteLine("该玩家不存在。");
                    continue;
                }
                break;
            }
            int cardNumber = -1;
            while (true)
            {
                Console.WriteLine($"玩家持有机会卡 {player.ChanceCards.Count} 张");
                foreach(var c in player.ChanceCards)
                {
                    Console.Write($"{c} ");
                }
                Console.WriteLine();
                if(player.ChanceCards.Count == 0)
                {
                    Console.WriteLine("无法使用机会卡。");
                    break;
                }

                Console.WriteLine("请输入机会卡ID：(Exit 退出)");
                bool notExit = ReadIntNumber(out cardNumber);
                if (!notExit)
                    return false;
                if (player.ChanceCards.Contains(cardNumber))
                    break;
                Console.WriteLine("玩家不持有该卡。");
                
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
                    Console.WriteLine("请输入使用机会卡的玩家ID：(Exit 退出)");
                    string playerId = Console.ReadLine().Trim();
                    if (string.Equals(playerId, "exit", StringComparison.OrdinalIgnoreCase))
                        return false;
                    player = Players.FirstOrDefault(p => p.Id == playerId || string.Equals(p.Name, playerId, StringComparison.OrdinalIgnoreCase));
                    if (player == null)
                    {
                        Console.WriteLine("该玩家不存在。");
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
            PlayerMoved++;
            Round = PlayerMoved / Players.Count;
            return true;
        }

        #region Data Fix
        private bool PlayerNameFix()
        {
            Player player;
            while (true)
            {
                Console.WriteLine("请输入使用机会卡的玩家ID：(Exit 退出)");
                string playerId = Console.ReadLine().Trim();
                if (string.Equals(playerId, "exit", StringComparison.OrdinalIgnoreCase))
                    return false;
                player = Players.FirstOrDefault(p => p.Id == playerId || string.Equals(p.Name, playerId, StringComparison.OrdinalIgnoreCase));
                if (player == null)
                {
                    Console.WriteLine("该玩家不存在。");
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
                Console.WriteLine("请输入使用机会卡的玩家ID：(Exit 退出)");
                string playerId = Console.ReadLine().Trim();
                if (string.Equals(playerId, "exit", StringComparison.OrdinalIgnoreCase))
                    return false;
                player = Players.FirstOrDefault(p => p.Id == playerId || string.Equals(p.Name, playerId, StringComparison.OrdinalIgnoreCase));
                if (player == null)
                {
                    Console.WriteLine("该玩家不存在。");
                    continue;
                }
                break;
            }

            int positionNumber = - 1;
            while(positionNumber < 0 || positionNumber > 35)
            {
                Console.WriteLine("请输入玩家的正确位置：(0-35)/(Exit 退出)");
                bool notExit = ReadIntNumber(out positionNumber);
                if (!notExit)
                    return false;
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
                Console.WriteLine("请输入使用机会卡的玩家ID：(Exit 退出)");
                string playerId = Console.ReadLine().Trim();
                if (string.Equals(playerId, "exit", StringComparison.OrdinalIgnoreCase))
                    return false;
                player = Players.FirstOrDefault(p => p.Id == playerId || string.Equals(p.Name, playerId, StringComparison.OrdinalIgnoreCase));
                if (player == null)
                {
                    Console.WriteLine("该玩家不存在。");
                    continue;
                }
                break;
            }
            int gold = -99999;
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("请输入玩家的正确金币：(Exit 退出)");
                bool notExit = ReadIntNumber(out gold);
                if (!notExit)
                    return false;
                Console.WriteLine($"请确认的金币数额：{gold} (Y/n)");
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
                Console.WriteLine("请输入艺人所在的位置：(0-35)/(Exit 退出)");
                bool notExit = ReadIntNumber(out positionNumber);
                if (!notExit)
                    return false;
            }
            int level = -1;
            while (level < 0 || level > 4)
            {
                Console.WriteLine("请输入艺人的正确等级：(0-4)/(Exit 退出)");
                bool notExit = ReadIntNumber(out level);
                if (!notExit)
                    return false;             
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

            int positionNumber = -1;
            while (positionNumber < 0 || positionNumber > 35)
            {
                Console.WriteLine("请输入艺人所在的位置：(0-35)/(Exit 退出)");
                bool notExit = ReadIntNumber(out positionNumber);
                if (!notExit)
                    return false;
            }

            Player player;
            while (true)
            {
                Console.WriteLine("请输入艺人的正确拥有者ID：(Exit 退出)");
                string playerId = Console.ReadLine().Trim();
                if (string.Equals(playerId, "exit", StringComparison.OrdinalIgnoreCase))
                    return false;
                player = Players.FirstOrDefault(p => p.Id == playerId || string.Equals(p.Name, playerId, StringComparison.OrdinalIgnoreCase));
                if (player == null)
                {
                    Console.WriteLine("该玩家不存在。");
                    continue;
                }
                break;
            }


            var idol = GameBoard.BoardMap[positionNumber].PositionCard;
            if (idol.Type == CardType.Idol || idol.Type == CardType.Special)
            {
                GameLogs positionFixLog = new GameLogs()
                {
                    ID = GetLogID(),
                    Type = LogEventType.IdolOwnerFix,
                    LevelChangePositionNo = positionNumber,
                    Info = $"艺人归属修正{positionNumber} : from{idol.HolderId} to {player.Id}",
                    Round = Round,
                    currentPlayer = currentPlayer
                };
                AddLog(positionFixLog);
                idol.HolderId = player.Id;
            }
            return true;
        }


        private bool Mortgage()
        {
            Player player;
            while (true)
            {
                Console.WriteLine("请输入执行抵押的玩家ID：(Exit 退出)");
                string playerId = Console.ReadLine().Trim();
                if (string.Equals(playerId, "exit", StringComparison.OrdinalIgnoreCase))
                    return false;
                player = Players.FirstOrDefault(p => p.Id == playerId || string.Equals(p.Name, playerId, StringComparison.OrdinalIgnoreCase));
                if (player == null)
                {
                    Console.WriteLine("该玩家不存在。");
                    continue;
                }
                break;
            }

            int positionNumber = -1;
            while (positionNumber < 0 || positionNumber > 35)
            {
                Console.WriteLine("请输入抵押艺人位置：(0-35)/(Exit 退出)");
                bool notExit = ReadIntNumber(out positionNumber);
                if (!notExit)
                    return false;
            }
            var Idol = GameBoard.BoardMap[positionNumber].PositionCard;
            if (Idol.Type != CardType.Idol && Idol.Type != CardType.Special)
            {
                Console.WriteLine("非艺人或特殊产业，不能抵押。");
                return false;
            }
            if (Idol.HolderId != player.Id)
            {
                Console.WriteLine("艺人或特殊产业非该玩家所属，不能抵押。");
                return false;
            }
            if(Idol.Type == CardType.Idol && Idol.Level > 0)
            {
                Console.WriteLine("艺人非素人(lv0)，不能抵押。");
                return false;
            }

            GameLogs mortLog = new GameLogs()
            {
                ID = GetLogID(),
                Type = LogEventType.Mortgage,
                PlayerName = player.Name,
                CardEvent = Idol,
                Info = $"玩家 {player.Name} 抵押 {positionNumber} : {Idol.Name}, 获得 ${Idol.InitCost/2}",
                Round = Round,
                currentPlayer = currentPlayer
            };
            AddLog(mortLog);
            Idol.isMortgage = true;
            player.Gold += Idol.InitCost / 2;

            return true;
        }


        private bool Redeem()
        {
            Player player;
            while (true)
            {
                Console.WriteLine("请输入执行赎回的玩家ID：(Exit 退出)");
                string playerId = Console.ReadLine().Trim();
                if (string.Equals(playerId, "exit", StringComparison.OrdinalIgnoreCase))
                    return false;
                player = Players.FirstOrDefault(p => p.Id == playerId || string.Equals(p.Name, playerId, StringComparison.OrdinalIgnoreCase));
                if (player == null)
                {
                    Console.WriteLine("该玩家不存在。");
                    continue;
                }
                break;
            }

            int positionNumber = -1;
            while (positionNumber < 0 || positionNumber > 35)
            {
                Console.WriteLine("请输入赎回艺人位置：(0-35)/(Exit 退出)");
                bool notExit = ReadIntNumber(out positionNumber);
                if (!notExit)
                    return false;
            }
            var Idol = GameBoard.BoardMap[positionNumber].PositionCard;
            if (Idol.isMortgage == false)
            {
                Console.WriteLine("艺人或特殊产业非抵押状态，不能赎回。");
                return false;
            }
            if (Idol.HolderId != player.Id)
            {
                Console.WriteLine("艺人或特殊产业非该玩家所属，不能赎回。");
                return false;
            }

            if (Idol.InitCost > player.Gold)
            {
                Console.WriteLine("玩家金币不足，不能赎回。");
                return false;
            }
            GameLogs mortLog = new GameLogs()
            {
                ID = GetLogID(),
                Type = LogEventType.Redeem,
                PlayerName = player.Name,
                CardEvent = Idol,
                Info = $"玩家 {player.Name} 赎回 {positionNumber} : {Idol.Name}, 支付 ${Idol.InitCost}",
                Round = Round,
                currentPlayer = currentPlayer
            };
            AddLog(mortLog);
            Idol.isMortgage = false;
            player.Gold -= Idol.InitCost;

            return true;
        }
        #endregion
    }
}
