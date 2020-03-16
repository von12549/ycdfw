using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dfw.Models
{
    public class Display
    {
        public static void DisplayBoard(Board board, List<Player> players)
        {
            //foreach (var po in board.BoardMap)
            //{
            //    try
            //    {
            //        Console.WriteLine("---------------------------------");
            //        Console.WriteLine($"Position : {po.PositionNumber}");
            //        DisplayCard(po.PositionCard);
            //    }
            //    catch(Exception ex)
            //    {
            //        throw;
            //    }
            //}
            #region Display 18-29
            for (int i = 18; i <= 29; i++)
            {
                Console.Write("############\t");
            }
            Console.WriteLine();
            for (int i = 18; i <= 29; i++)
            {
                Console.Write($"{i} : {board.BoardMap[i].PositionCard.Name}\t");
            }
            Console.WriteLine();
            for (int i = 18; i <= 29; i++)
            {
                if(board.BoardMap[i].PositionCard.Type == CardType.Idol)
                {
                    Console.Write($"签约 :  {board.BoardMap[i].PositionCard.InitCost}\t");
                }
                else
                {
                    Console.Write("............\t");
                }
            }
            Console.WriteLine();
            for (int i = 18; i <= 29; i++)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol)
                {
                    Console.Write($"升级 :  {board.BoardMap[i].PositionCard.LevelUpCost}\t");
                }
                else
                {
                    Console.Write("............\t");
                }
            }
            Console.WriteLine();
            for (int i = 18; i <= 29; i++)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol)
                {
                    Console.Write($"等级 :  {board.BoardMap[i].PositionCard.Level}\t");
                }
                else
                {
                    Console.Write("............\t");
                }
            }
            Console.WriteLine();
            for (int i = 18; i <= 29; i++)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol)
                {
                    Console.Write($"通告 :  {board.BoardMap[i].PositionCard.Fee[board.BoardMap[i].PositionCard.Level]}\t");
                }
                else
                {
                    Console.Write("............\t");
                }
            }
            Console.WriteLine();
            for (int i = 18; i <= 29; i++)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol)
                {
                    Console.Write($"抵押 :  {board.BoardMap[i].PositionCard.isMortgage}\t");
                }
                else
                {
                    Console.Write("............\t");
                }
            }
            Console.WriteLine();
            for (int i = 18; i <= 29; i++)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol || board.BoardMap[i].PositionCard.Type == CardType.Special)
                {
                    if (board.BoardMap[i].PositionCard.HolderId == "-1")
                    {
                        Console.Write($"所属 :  无\t");
                    }
                    else
                    {
                        Console.Write($"所属 :  {board.BoardMap[i].PositionCard.HolderId}\t");
                    }
                }
                else
                {
                    Console.Write("............\t");
                }
            }
            Console.WriteLine();
            for (int i = 18; i <= 29; i++)
            {
                var pl = players.Where(p => p.PositionNumber == i).ToList();
                string str = "玩家:";
                foreach(var p in pl)
                {
                    str += $"{p.Id} ";
                }
                while(str.Length < 10)
                {
                    str += " ";
                }
                Console.Write($"{str}\t");
            }
            Console.WriteLine();
            #endregion
            int a = 17;
            int b = 30;
            #region 17 - 12, 30 - 35
            for (int x = 0; x < 6; x++)
            {
                a = 17 - x;
                b = 30 + x;
                Console.Write("############\t");
                for (int i = 0; i < 10; i++) { Console.Write("            \t"); }
                Console.Write("############\t");
                Console.WriteLine();

                Console.Write($"{a} : {board.BoardMap[a].PositionCard.Name}\t");
                for (int i = 0; i < 10; i++) { Console.Write("            \t"); }
                Console.Write($"{b} : {board.BoardMap[b].PositionCard.Name}\t");
                Console.WriteLine();

                if (board.BoardMap[a].PositionCard.Type == CardType.Idol)
                {
                    Console.Write($"签约 :  {board.BoardMap[a].PositionCard.InitCost}\t");
                }
                else
                {
                    Console.Write("............\t");
                }
                for (int i = 0; i < 10; i++) { Console.Write("            \t"); }
                if (board.BoardMap[b].PositionCard.Type == CardType.Idol)
                {
                    Console.Write($"签约 :  {board.BoardMap[b].PositionCard.InitCost}\t");
                }
                else
                {
                    Console.Write("............\t");
                }
                Console.WriteLine();

                if (board.BoardMap[a].PositionCard.Type == CardType.Idol)
                {
                    Console.Write($"升级 :  {board.BoardMap[a].PositionCard.LevelUpCost}\t");
                }
                else
                {
                    Console.Write("............\t");
                }
                for (int i = 0; i < 10; i++) { Console.Write("            \t"); }
                if (board.BoardMap[b].PositionCard.Type == CardType.Idol)
                {
                    Console.Write($"升级 :  {board.BoardMap[b].PositionCard.LevelUpCost}\t");
                }
                else
                {
                    Console.Write("............\t");
                }
                Console.WriteLine();

                if (board.BoardMap[a].PositionCard.Type == CardType.Idol)
                {
                    Console.Write($"等级 :  {board.BoardMap[a].PositionCard.Level}\t");
                }
                else
                {
                    Console.Write("............\t");
                }
                for (int i = 0; i < 10; i++) { Console.Write("            \t"); }
                if (board.BoardMap[b].PositionCard.Type == CardType.Idol)
                {
                    Console.Write($"等级 :  {board.BoardMap[b].PositionCard.Level}\t");
                }
                else
                {
                    Console.Write("............\t");
                }
                Console.WriteLine();

                if (board.BoardMap[a].PositionCard.Type == CardType.Idol)
                {
                    Console.Write($"通告 :  {board.BoardMap[a].PositionCard.Fee[board.BoardMap[17].PositionCard.Level]}\t");
                }
                else
                {
                    Console.Write("............\t");
                }
                for (int i = 0; i < 10; i++) { Console.Write("            \t"); }
                if (board.BoardMap[b].PositionCard.Type == CardType.Idol)
                {
                    Console.Write($"通告 :  {board.BoardMap[b].PositionCard.Fee[board.BoardMap[30].PositionCard.Level]}\t");
                }
                else
                {
                    Console.Write("............\t");
                }
                Console.WriteLine();

                if (board.BoardMap[a].PositionCard.Type == CardType.Idol)
                {
                    Console.Write($"抵押 :  {board.BoardMap[a].PositionCard.isMortgage}\t");
                }
                else
                {
                    Console.Write("............\t");
                }
                for (int i = 0; i < 10; i++) { Console.Write("            \t"); }
                if (board.BoardMap[b].PositionCard.Type == CardType.Idol)
                {
                    Console.Write($"抵押 :  {board.BoardMap[b].PositionCard.isMortgage}\t");
                }
                else
                {
                    Console.Write("............\t");
                }
                Console.WriteLine();

                if (board.BoardMap[a].PositionCard.Type == CardType.Idol || board.BoardMap[a].PositionCard.Type == CardType.Special)
                {
                    if (board.BoardMap[a].PositionCard.HolderId == "-1")
                    {
                        Console.Write($"所属 :  无\t");
                    }
                    else
                    {
                        Console.Write($"所属 :  {board.BoardMap[a].PositionCard.HolderId}\t");
                    }
                }
                else
                {
                    Console.Write("............\t");
                }
                for (int i = 0; i < 10; i++) { Console.Write("            \t"); }
                if (board.BoardMap[b].PositionCard.Type == CardType.Idol || board.BoardMap[b].PositionCard.Type == CardType.Special)
                {
                    if (board.BoardMap[b].PositionCard.HolderId == "-1")
                    {
                        Console.Write($"所属 :  无\t");
                    }
                    else
                    {
                        Console.Write($"所属 :  {board.BoardMap[b].PositionCard.HolderId}\t");
                    }
                }
                else
                {
                    Console.Write("............\t");
                }
                Console.WriteLine();


                var pl1 = players.Where(p => p.PositionNumber == a).ToList();
                string str1 = "玩家:";
                foreach (var p in pl1)
                {
                    str1 += $"{p.Id} ";
                }
                while (str1.Length < 10)
                {
                    str1 += " ";
                }
                Console.Write($"{str1}\t");
                for (int i = 0; i < 10; i++) { Console.Write("            \t"); }
                var pl2 = players.Where(p => p.PositionNumber == b).ToList();
                string str2 = "玩家:";
                foreach (var p in pl2)
                {
                    str2 += $"{p.Id} ";
                }
                while (str2.Length < 10)
                {
                    str2 += " ";
                }
                Console.Write($"{str2}\t");
                Console.WriteLine();
            }
            #endregion
            #region 11 - 0
            for (int i = 11; i >= 0; i--)
            {
                Console.Write("############\t");
            }
            Console.WriteLine();

            for (int i = 11; i >=0; i--)
            {
                Console.Write($"{i} : {board.BoardMap[i].PositionCard.Name}\t");
            }
            Console.WriteLine();
            for (int i = 11; i >= 0; i--)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol)
                {
                    Console.Write($"签约 :  {board.BoardMap[i].PositionCard.InitCost}\t");
                }
                else
                {
                    Console.Write("............\t");
                }
            }
            Console.WriteLine();
            for (int i = 11; i >= 0; i--)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol)
                {
                    Console.Write($"升级 :  {board.BoardMap[i].PositionCard.LevelUpCost}\t");
                }
                else
                {
                    Console.Write("............\t");
                }
            }
            Console.WriteLine();
            for (int i = 11; i >= 0; i--)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol)
                {
                    Console.Write($"等级 :  {board.BoardMap[i].PositionCard.Level}\t");
                }
                else
                {
                    Console.Write("............\t");
                }
            }
            Console.WriteLine();
            for (int i = 11; i >= 0; i--)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol)
                {
                    Console.Write($"通告 :  {board.BoardMap[i].PositionCard.Fee[board.BoardMap[i].PositionCard.Level]}\t");
                }
                else
                {
                    Console.Write("............\t");
                }
            }
            Console.WriteLine();

            
            for (int i = 11; i >= 0; i--)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol)
                {
                    Console.Write($"抵押 :  {board.BoardMap[i].PositionCard.isMortgage}\t");
                }
                else
                {
                    Console.Write("............\t");
                }
            }
            Console.WriteLine();
            for (int i = 11; i >= 0; i--)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol || board.BoardMap[i].PositionCard.Type == CardType.Special)
                {
                    if (board.BoardMap[i].PositionCard.HolderId == "-1")
                    {
                        Console.Write($"所属 :  无\t");
                    }
                    else
                    {
                        Console.Write($"所属 :  {board.BoardMap[i].PositionCard.HolderId}\t");
                    }
                }
                else
                {
                    Console.Write("............\t");
                }
            }
            Console.WriteLine();
            for (int i = 11; i >= 0; i--)
            {
                var pl = players.Where(p => p.PositionNumber == i).ToList();
                string str = "玩家:";
                foreach (var p in pl)
                {
                    str += $"{p.Id} ";
                }
                while (str.Length < 10)
                {
                    str += " ";
                }
                Console.Write($"{str}\t");
            }
            Console.WriteLine();
            #endregion



        }

        public static void DisplayCard(Card card)
        {
            Console.WriteLine($"Name : {card.Name}");
            Console.WriteLine($"Info : {card.Info}");
            if (card.Type == CardType.Idol)
            {
                Console.WriteLine($"签约费  ：{card.InitCost}");
                Console.WriteLine($"升级费  ：{card.LevelUpCost}");
                Console.WriteLine($"等级    ：{card.Level}");
                Console.WriteLine($"通告费  ：{card.Fee[card.Level]}");
                Console.WriteLine($"是否抵押：{card.isMortgage}");
                if (!string.Equals(card.HolderId, "-1"))
                {
                    Console.WriteLine($"持有者    ：{card.HolderId}");
                }
                else
                {
                    Console.WriteLine($"持有者    ：无");
                }
            }
        }

        public static void DisplayCH2(List<int> ChanceDeck, List<int> ChanceUsed, List<int> ChangeDeck, List<int> ChangeUsed, List<Player> players)
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine("可使用的机会卡：");
            foreach(var c in ChanceDeck)
            {
                Console.Write($"{c},");
            }
            Console.WriteLine();
            Console.WriteLine("使用过的机会卡：");
            foreach (var c in ChanceUsed)
            {
                Console.Write($"{c},");
            }
            Console.WriteLine();
            Console.WriteLine("可使用的命运卡：");
            foreach (var c in ChangeDeck)
            {
                Console.Write($"{c},");
            }
            Console.WriteLine();
            Console.WriteLine("使用过的命运卡：");
            foreach (var c in ChangeUsed)
            {
                Console.Write($"{c},");
            }
            Console.WriteLine();
            Console.WriteLine("玩家持有的机会卡：");
            foreach(var p in players)
            {
                Console.WriteLine($"玩家 {p.Id} 持有 {p.ChanceCards.Count} 张机会卡：");
                foreach(var c in p.ChanceCards)
                {
                    Console.Write($"{c},");
                }
                Console.WriteLine();
            }
            Console.WriteLine("----------------------------");
        }

        public static void DisplayPlayer(Player player)
        {
            Console.WriteLine();
            Console.WriteLine("----------玩家信息 ----------");
            Console.WriteLine($"玩家ID：   {player.Id}");
            Console.WriteLine($"玩家昵称： {player.Name}");
            Console.WriteLine($"玩家位置： {player.PositionNumber}");
            Console.WriteLine($"玩家资产： {player.Gold}");
            var stop = player.Stop ? "休息" : "可以行动";
            Console.WriteLine($"玩家移动状态：{stop}");
            var canWin = player.CanWin ? "可以收益" : "无法进行收益";
            Console.WriteLine($"玩家收益状态：{canWin}");
            Console.WriteLine($"玩家机会卡数量： {player.ChanceCards.Count}");
            foreach(var c in player.ChanceCards)
            {
                Console.Write($"{c} ");
            }
            Console.WriteLine();
        }

        public static void DisplayMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("----------主菜单  ----------");
            Console.WriteLine("1. 显示棋盘：");
            Console.WriteLine("2. 添加玩家：");
            Console.WriteLine("3. 显示玩家信息：");
            Console.WriteLine("4. 进入游戏菜单：");
            Console.WriteLine("5. 当前游戏复盘：");
            Console.WriteLine("6. 数值修正：");
            Console.WriteLine("0. 退出游戏：");
            Console.WriteLine("----------等待输入----------");
            Console.WriteLine();
        }

        public static void DisplayGameMenu()
        {
            Console.WriteLine();
            Console.WriteLine("---------游戏菜单-----------");
            Console.WriteLine("1. 玩家行动：");
            Console.WriteLine("2. 玩家使用机会卡：");
            Console.WriteLine("3. 显示卡池信息：");
            Console.WriteLine("4. 显示玩家信息：");
            Console.WriteLine("0. 退出游戏菜单：");
            Console.WriteLine("---------等待输入-----------");
            Console.WriteLine();
        }

        public static void DisplayDataFixMenu()
        {
            Console.WriteLine();
            Console.WriteLine("---------数据修正菜单-------");
            Console.WriteLine("1. 玩家信息修正：");
            Console.WriteLine("2. 玩家位置修正：");
            Console.WriteLine("3. 玩家金币修正：");
            Console.WriteLine("4. 艺人等级修正：");
            Console.WriteLine("5. 艺人归属修正：");
            Console.WriteLine("0. 退出修正菜单：");
            Console.WriteLine("----------等待输入----------");
            Console.WriteLine();
        }


        public static void DisplayLog(GameLogs log, Board board)
        {
            Console.WriteLine($"#####[Round: {log.Round}] | [Player: {log.currentPlayer.Name} {log.currentPlayer.Id}] #####");
            switch(log.Type)
            {
                case LogEventType.AddPlayer:
                    Console.WriteLine($"[{log.TimeStamp} - {log.ID}] | 添加玩家：{log.PlayerName}, {log.Info}");
                    break;
                case LogEventType.Move:
                    Card card = board.BoardMap[log.EndPoint].PositionCard;
                    Console.WriteLine($"[{log.TimeStamp} - {log.ID}] |玩家 {log.PlayerName} 移动: from {log.StartPoint} to {log.EndPoint}");
                    DisplayCard(card);
                    break;
                case LogEventType.IdolContract:
                    Console.WriteLine($"[{log.TimeStamp} - {log.ID}] |玩家 {log.PlayerName} 签约艺人: {log.CardEvent.Id}，花费 ${log.CardEvent.InitCost}");
                    break;
                case LogEventType.RoundReward:
                    Console.WriteLine($"[{log.TimeStamp} - {log.ID}] |玩家 {log.PlayerName} 获得初始点奖励 {log.GoldWin}");
                    break;
                case LogEventType.GoldChange:
                    Console.WriteLine($"[{log.TimeStamp} - {log.ID}] |玩家 {log.PlayerName} 进行支付结算，收入:${log.GoldWin}，支出:${log.GoldLoss}");
                    break;
                case LogEventType.CPFee:
                    Console.WriteLine($"[{log.TimeStamp} - {log.ID}] |玩家 {log.PlayerName} 进行CP支付结算，收入:${log.GoldWin}，支出:${log.GoldLoss}");
                    break;
                case LogEventType.LevelUp:
                    Console.WriteLine($"[{log.TimeStamp} - {log.ID}] |玩家 {log.PlayerName} 对艺人{log.LevelChangePositionNo}进行升级，lv{log.LevelUpFrom} - lv{log.LevelUpTo}，花费${log.CardEvent.LevelUpCost}");
                    break;
                case LogEventType.ChanceCardWin:
                    Console.WriteLine($"[{log.TimeStamp} - {log.ID}] |玩家 {log.PlayerName} 获得机会卡 {log.ChanceCardWin}");
                    break;
                case LogEventType.ChanceCardUse:
                    Console.WriteLine($"[{log.TimeStamp} - {log.ID}] |玩家 {log.PlayerName} 使用机会卡 {log.ChanceCardUse}");
                    break;
                case LogEventType.ChangeCardTrigger:
                    Console.WriteLine($"[{log.TimeStamp} - {log.ID}] |玩家 {log.PlayerName} 触发命运卡 {log.ChangeCardTrigger}");
                    break;
                case LogEventType.StartGame:
                    Console.WriteLine($"[{log.TimeStamp} - {log.ID}] 游戏开始，欢迎来到 - 杨村大富翁");
                    break;
                case LogEventType.PositionFix:
                    Console.WriteLine($"[{log.TimeStamp} - {log.ID}] |玩家 {log.PlayerName} 位置修正 {log.Info}");
                    break;
                case LogEventType.PlayerFix:
                    Console.WriteLine($"[{log.TimeStamp} - {log.ID}] |玩家 {log.PlayerName} 信息修正 {log.Info}");
                    break;
                case LogEventType.GoldFix:
                    Console.WriteLine($"[{log.TimeStamp} - {log.ID}] |玩家 {log.PlayerName} 金币修正 {log.Info}");
                    break;
                case LogEventType.LevelFix:
                    Console.WriteLine($"[{log.TimeStamp} - {log.ID}] |艺人 {log.LevelChangePositionNo} 等级修正 {log.Info}");
                    break;
                case LogEventType.IdolOwnerFix:
                    Console.WriteLine($"[{log.TimeStamp} - {log.ID}] |艺人 {log.LevelChangePositionNo} 所属经纪人修正 {log.Info}");
                    break;

                case LogEventType.BackHome:
                    Console.WriteLine($"[{log.TimeStamp} - {log.ID}] |玩家 {log.PlayerName} ：{log.Info}");
                    break;
                case LogEventType.Holiday:
                    Console.WriteLine($"[{log.TimeStamp} - {log.ID}] |玩家 {log.PlayerName} ：{log.Info}");
                    break;
                case LogEventType.Education:
                    Console.WriteLine($"[{log.TimeStamp} - {log.ID}] |玩家 {log.PlayerName} ：{log.Info}");
                    break;
                default:
                    break;
            }
        }

    }
}
