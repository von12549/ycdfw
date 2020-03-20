using dfw.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace dfw
{
    public class GameReords
    {
        public string fileName { get; set; }
        public GameReords(string _fileName)
        {
            fileName = _fileName;
            CreateRecords();
        }
        public void AppendRecordsLine(string msg = "")
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    CreateRecords();
                }
                using (StreamWriter sw = File.AppendText(fileName))
                {
                    sw.WriteLine(msg);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void AppendRecords(string msg)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    CreateRecords();
                }

                using (StreamWriter sw = File.AppendText(fileName))
                {
                    sw.Write(msg);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void CreateRecords()
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    using (StreamWriter sw = File.CreateText(fileName))
                    {
                        sw.WriteLine("###\t 越越冲鸭！\t###");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static string GenerateFileName()
        {
            string fileNameBase = "GameLogs";
            int fileNameAppend = 0;
            while (File.Exists($"{fileNameBase}-{fileNameAppend}.txt"))
            {
                fileNameAppend++;
            }
            return $"{fileNameBase}-{fileNameAppend}.txt";
        }

        public void RecordLog(GameLogs log, Board board)
        {
            AppendRecordsLine($"[Round: {log.Round}] | [Player: {log.currentPlayer.Name} {log.currentPlayer.Id} : {log.currentPlayer.Gold}]");
            switch (log.Type)
            {
                case LogEventType.AddPlayer:
                    AppendRecordsLine($"[{log.ID}] | 添加玩家：{log.PlayerName}, {log.Info}");
                    break;
                case LogEventType.Move:
                    Card card = board.BoardMap[log.EndPoint].PositionCard;
                    AppendRecordsLine($"[{log.ID}] |玩家 {log.PlayerName} 移动: from {log.StartPoint} to {log.EndPoint}");
                    RecordCard(card);
                    break;
                case LogEventType.IdolContract:
                    AppendRecordsLine($"[{log.ID}] |玩家 {log.PlayerName} 签约艺人: {log.CardEvent.Id}，花费 ${log.CardEvent.InitCost}");
                    break;
                case LogEventType.RoundReward:
                    AppendRecordsLine($"[{log.ID}] |玩家 {log.PlayerName} 获得初始点奖励 {log.GoldWin}");
                    break;
                case LogEventType.GoldChange:
                    if (log.CpLoss == 0)
                    {
                        AppendRecordsLine($"[{log.ID}] |玩家 {log.PlayerName} 回合支付结算，{log.PayFrom} 向 {log.PayTo} 支付 ${log.GoldLoss}");
                    }
                    else
                    {
                        AppendRecordsLine($"[{log.ID}] |玩家 {log.PlayerName} 回合支付结算，{log.PayFrom} 向 {log.PayTo} 支付 ${log.GoldLoss} + CP: ${log.CpLoss}");
                    }
                    break;
                case LogEventType.LevelUp:
                    AppendRecordsLine($"[{log.ID}] |玩家 {log.PlayerName} 对艺人{log.LevelChangePositionNo}进行升级，lv{log.LevelUpFrom} - lv{log.LevelUpTo}，花费${log.CardEvent.LevelUpCost}");
                    break;
                case LogEventType.ChanceCardWin:
                    AppendRecordsLine($"[{log.ID}] |玩家 {log.PlayerName} 获得机会卡 {log.ChanceCardWin}");
                    break;
                case LogEventType.ChanceCardUse:
                    AppendRecordsLine($"[{log.ID}] |玩家 {log.PlayerName} 使用机会卡 {log.ChanceCardUse}");
                    break;
                case LogEventType.ChangeCardTrigger:
                    AppendRecordsLine($"[{log.ID}] |玩家 {log.PlayerName} 触发命运卡 {log.ChangeCardTrigger}");
                    break;
                case LogEventType.StartGame:
                    AppendRecordsLine($"[{log.ID}] 游戏开始，欢迎来到 - 杨村大富翁");
                    break;
                case LogEventType.PositionFix:
                    AppendRecordsLine($"[{log.ID}] |玩家 {log.PlayerName} 位置修正 {log.Info}");
                    break;
                case LogEventType.PlayerFix:
                    AppendRecordsLine($"[{log.ID}] |玩家 {log.PlayerName} 信息修正 {log.Info}");
                    break;
                case LogEventType.GoldFix:
                    AppendRecordsLine($"[{log.ID}] |玩家 {log.PlayerName} 金币修正 {log.Info}");
                    break;
                case LogEventType.LevelFix:
                    AppendRecordsLine($"[{log.ID}] |艺人 {log.LevelChangePositionNo} 等级修正 {log.Info}");
                    break;
                case LogEventType.IdolOwnerFix:
                    AppendRecordsLine($"[{log.ID}] |艺人 {log.LevelChangePositionNo} 所属经纪人修正 {log.Info}");
                    break;

                case LogEventType.BackHome:
                    AppendRecordsLine($"[{log.ID}] |玩家 {log.PlayerName} ：{log.Info}");
                    break;
                case LogEventType.Holiday:
                    AppendRecordsLine($"[{log.ID}] |玩家 {log.PlayerName} ：{log.Info}");
                    break;
                case LogEventType.HolidayEnd:
                    AppendRecordsLine($"[{log.ID}] |玩家 {log.PlayerName} ：{log.Info}");
                    break;
                case LogEventType.Education:
                    AppendRecordsLine($"[{log.ID}] |玩家 {log.PlayerName} ：{log.Info}");
                    break;
                case LogEventType.Mortgage:
                    AppendRecordsLine($"[{log.ID}] |玩家 {log.PlayerName} ：{log.Info}");
                    break;
                case LogEventType.Redeem:
                    AppendRecordsLine($"[{log.ID}] |玩家 {log.PlayerName} ：{log.Info}");
                    break;
                default:
                    break;
            }
        }

        public void RecordCard(Card card)
        {
            AppendRecords($"[Name : {card.Name} ]\t");
            AppendRecords($"[Info : {card.Info}]\t");
            if (card.Type == CardType.Idol)
            {
                AppendRecords($"[签约费 ：{card.InitCost}]\t");
                AppendRecords($"[升级费 ：{card.LevelUpCost}]\t");
                AppendRecords($"[等级 ：{card.Level}]\t");
                AppendRecords($"[通告费  ：{card.Fee[card.Level]}]\t");
                AppendRecords($"[是否抵押：{card.isMortgage}]\t");
                if (!string.Equals(card.HolderId, "-1"))
                {
                    AppendRecords($"[持有者 ：{card.HolderId}]\t");
                }
                else
                {
                    AppendRecords($"[持有者 ：无]\t");
                }
            }
            AppendRecordsLine();
        }

        public void RecordBoard(Board board, List<Player> players)
        {
            //foreach (var po in board.BoardMap)
            //{
            //    try
            //    {
            //        AppendRecordsLine("---------------------------------");
            //        AppendRecordsLine($"Position : {po.PositionNumber}");
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
                AppendRecords("|#############|\t");
            }
            AppendRecordsLine();
            for (int i = 18; i <= 29; i++)
            {
                AppendRecords($"|{i,-4}: {board.BoardMap[i].PositionCard.Name, 7}|\t");
            }
            AppendRecordsLine();
            for (int i = 18; i <= 29; i++)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol)
                {
                    AppendRecords($"|签约: {board.BoardMap[i].PositionCard.InitCost, 7}|\t");
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
            }
            AppendRecordsLine();
            for (int i = 18; i <= 29; i++)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol)
                {
                    AppendRecords($"|升级: {board.BoardMap[i].PositionCard.LevelUpCost, 7}|\t");
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
            }
            AppendRecordsLine();
            for (int i = 18; i <= 29; i++)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol)
                {
                    AppendRecords($"|等级: {board.BoardMap[i].PositionCard.Level, 7}|\t");
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
            }
            AppendRecordsLine();
            for (int i = 18; i <= 29; i++)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol)
                {
                    AppendRecords($"|通告: {board.BoardMap[i].PositionCard.Fee[board.BoardMap[i].PositionCard.Level], 7}|\t");
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
            }
            AppendRecordsLine();
            for (int i = 18; i <= 29; i++)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol)
                {
                    AppendRecords($"|抵押: {board.BoardMap[i].PositionCard.isMortgage, 7}|\t");
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
            }
            AppendRecordsLine();
            for (int i = 18; i <= 29; i++)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol || board.BoardMap[i].PositionCard.Type == CardType.Special)
                {
                    if (board.BoardMap[i].PositionCard.HolderId == "-1")
                    {
                        AppendRecords($"|所属: {"None",7}|\t");
                    }
                    else
                    {
                        AppendRecords($"|所属: {board.BoardMap[i].PositionCard.HolderId, 7}|\t");
                    }
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
            }
            AppendRecordsLine();
            for (int i = 18; i <= 29; i++)
            {
                var pl = players.Where(p => p.PositionNumber == i).ToList();
                string str = "|玩家:";
                foreach (var p in pl)
                {
                    str += $"{p.Id} ";
                }
                while (str.Length < 12)
                {
                    str += " ";
                }
                AppendRecords($"{str}|\t");
            }
            AppendRecordsLine();
            #endregion
            int a = 17;
            int b = 30;
            #region 17 - 12, 30 - 35
            for (int x = 0; x < 6; x++)
            {
                a = 17 - x;
                b = 30 + x;
                AppendRecords("|#############|\t");
                for (int i = 0; i < 10; i++) { AppendRecords("               \t"); }
                AppendRecords("|#############|\t");
                AppendRecordsLine();

                AppendRecords($"|{a,-4}: {board.BoardMap[a].PositionCard.Name,7}|\t");
                for (int i = 0; i < 10; i++) { AppendRecords("               \t"); }
                AppendRecords($"|{b,-4}: {board.BoardMap[b].PositionCard.Name,7}|\t");
                AppendRecordsLine();

                if (board.BoardMap[a].PositionCard.Type == CardType.Idol)
                {
                    AppendRecords($"|签约: {board.BoardMap[a].PositionCard.InitCost, 7}|\t");
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
                for (int i = 0; i < 10; i++) { AppendRecords("               \t"); }
                if (board.BoardMap[b].PositionCard.Type == CardType.Idol)
                {
                    AppendRecords($"|签约: {board.BoardMap[b].PositionCard.InitCost, 7}|\t");
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
                AppendRecordsLine();

                if (board.BoardMap[a].PositionCard.Type == CardType.Idol)
                {
                    AppendRecords($"|升级: {board.BoardMap[a].PositionCard.LevelUpCost, 7}|\t");
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
                for (int i = 0; i < 10; i++) { AppendRecords("               \t"); }
                if (board.BoardMap[b].PositionCard.Type == CardType.Idol)
                {
                    AppendRecords($"|升级: {board.BoardMap[b].PositionCard.LevelUpCost, 7}|\t");
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
                AppendRecordsLine();

                if (board.BoardMap[a].PositionCard.Type == CardType.Idol)
                {
                    AppendRecords($"|等级: {board.BoardMap[a].PositionCard.Level, 7}|\t");
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
                for (int i = 0; i < 10; i++) { AppendRecords("               \t"); }
                if (board.BoardMap[b].PositionCard.Type == CardType.Idol)
                {
                    AppendRecords($"|等级: {board.BoardMap[b].PositionCard.Level, 7}|\t");
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
                AppendRecordsLine();

                if (board.BoardMap[a].PositionCard.Type == CardType.Idol)
                {
                    AppendRecords($"|通告: {board.BoardMap[a].PositionCard.Fee[board.BoardMap[17].PositionCard.Level], 7}|\t");
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
                for (int i = 0; i < 10; i++) { AppendRecords("               \t"); }
                if (board.BoardMap[b].PositionCard.Type == CardType.Idol)
                {
                    AppendRecords($"|通告: {board.BoardMap[b].PositionCard.Fee[board.BoardMap[30].PositionCard.Level], 7}|\t");
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
                AppendRecordsLine();

                if (board.BoardMap[a].PositionCard.Type == CardType.Idol)
                {
                    AppendRecords($"|抵押: {board.BoardMap[a].PositionCard.isMortgage, 7}|\t");
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
                for (int i = 0; i < 10; i++) { AppendRecords("               \t"); }
                if (board.BoardMap[b].PositionCard.Type == CardType.Idol)
                {
                    AppendRecords($"|抵押: {board.BoardMap[b].PositionCard.isMortgage, 7}|\t");
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
                AppendRecordsLine();

                if (board.BoardMap[a].PositionCard.Type == CardType.Idol || board.BoardMap[a].PositionCard.Type == CardType.Special)
                {
                    if (board.BoardMap[a].PositionCard.HolderId == "-1")
                    {
                        AppendRecords($"|所属: {"None",7}|\t");
                    }
                    else
                    {
                        AppendRecords($"|所属: {board.BoardMap[a].PositionCard.HolderId, 7}|\t");
                    }
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
                for (int i = 0; i < 10; i++) { AppendRecords("               \t"); }
                if (board.BoardMap[b].PositionCard.Type == CardType.Idol || board.BoardMap[b].PositionCard.Type == CardType.Special)
                {
                    if (board.BoardMap[b].PositionCard.HolderId == "-1")
                    {
                        AppendRecords($"|所属: {"None",7}|\t");
                    }
                    else
                    {
                        AppendRecords($"|所属: {board.BoardMap[b].PositionCard.HolderId, 7}|\t");
                    }
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
                AppendRecordsLine();


                var pl1 = players.Where(p => p.PositionNumber == a).ToList();
                string str1 = "|玩家:";
                foreach (var p in pl1)
                {
                    str1 += $"{p.Id} ";
                }
                while (str1.Length < 12)
                {
                    str1 += " ";
                }
                AppendRecords($"{str1}|\t");
                for (int i = 0; i < 10; i++) { AppendRecords("               \t"); }
                var pl2 = players.Where(p => p.PositionNumber == b).ToList();
                string str2 = "|玩家:";
                foreach (var p in pl2)
                {
                    str2 += $"{p.Id} ";
                }
                while (str2.Length < 12)
                {
                    str2 += " ";
                }
                AppendRecords($"{str2}|\t");
                AppendRecordsLine();
            }
            #endregion
            #region 11 - 0
            for (int i = 11; i >= 0; i--)
            {
                AppendRecords("|#############|\t");
            }
            AppendRecordsLine();

            for (int i = 11; i >= 0; i--)
            {
                AppendRecords($"|{i, -4}: {board.BoardMap[i].PositionCard.Name, 7}|\t");
            }
            AppendRecordsLine();
            for (int i = 11; i >= 0; i--)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol)
                {
                    AppendRecords($"|签约: {board.BoardMap[i].PositionCard.InitCost, 7}|\t");
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
            }
            AppendRecordsLine();
            for (int i = 11; i >= 0; i--)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol)
                {
                    AppendRecords($"|升级: {board.BoardMap[i].PositionCard.LevelUpCost, 7}|\t");
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
            }
            AppendRecordsLine();
            for (int i = 11; i >= 0; i--)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol)
                {
                    AppendRecords($"|等级: {board.BoardMap[i].PositionCard.Level, 7}|\t");
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
            }
            AppendRecordsLine();
            for (int i = 11; i >= 0; i--)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol)
                {
                    AppendRecords($"|通告: {board.BoardMap[i].PositionCard.Fee[board.BoardMap[i].PositionCard.Level], 7}|\t");
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
            }
            AppendRecordsLine();


            for (int i = 11; i >= 0; i--)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol)
                {
                    AppendRecords($"|抵押: {board.BoardMap[i].PositionCard.isMortgage, 7}|\t");
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
            }
            AppendRecordsLine();
            for (int i = 11; i >= 0; i--)
            {
                if (board.BoardMap[i].PositionCard.Type == CardType.Idol || board.BoardMap[i].PositionCard.Type == CardType.Special)
                {
                    if (board.BoardMap[i].PositionCard.HolderId == "-1")
                    {
                        AppendRecords($"|所属: {"None", 7}|\t");
                    }
                    else
                    {
                        AppendRecords($"|所属: {board.BoardMap[i].PositionCard.HolderId, 7}|\t");
                    }
                }
                else
                {
                    AppendRecords("|.............|\t");
                }
            }
            AppendRecordsLine();
            for (int i = 11; i >= 0; i--)
            {
                var pl = players.Where(p => p.PositionNumber == i).ToList();
                string str = "|玩家:";
                foreach (var p in pl)
                {
                    str += $"{p.Id} ";
                }
                while (str.Length < 12)
                {
                    str += " ";
                }
                AppendRecords($"{str}|\t");
            }
            AppendRecordsLine();
            #endregion



        }

        public void RecordPlayer(Player player, Board board)
        {
            AppendRecordsLine();
            AppendRecordsLine("----------玩家信息 ----------");
            AppendRecordsLine($"玩家ID：   {player.Id}");
            AppendRecordsLine($"玩家昵称： {player.Name}");
            AppendRecordsLine($"玩家位置： {player.PositionNumber}");
            AppendRecordsLine($"玩家资产： {player.Gold}");
            var stop = player.Stop ? "休息" : "可以行动";
            AppendRecordsLine($"玩家移动状态：{stop}");
            var canWin = player.CanWin ? "可以收益" : "无法进行收益";
            AppendRecordsLine($"玩家收益状态：{canWin}");
            AppendRecordsLine($"玩家机会卡数量： {player.ChanceCards.Count}");
            foreach (var c in player.ChanceCards)
            {
                AppendRecords($"{c} ");
            }
            AppendRecordsLine();
            var CardHolds = board.BoardMap.Where(c => string.Equals(c.PositionCard.HolderId, player.Id)).ToList();
            AppendRecordsLine($"玩家签约数量： {CardHolds.Count}");
            foreach (var c in CardHolds)
            {
                AppendRecords($"{c.PositionNumber} ");
            }
            AppendRecordsLine();
        }
    }
}
