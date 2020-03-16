using System;
using System.Collections.Generic;
using System.Text;

namespace dfw.Models
{
    public class GameLogs
    {
        public LogEventType Type { get; set; }
        public int ID { get; set; } = -1;
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public int Round { get; set; }
        public string PlayerName { get; set; }
        public Card CardEvent { get; set; }
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
        public int GoldWin { get; set; }
        public int GoldLoss { get; set; }
        public int ChanceCardWin { get; set; }
        public int ChanceCardUse { get; set; }
        public int ChangeCardTrigger { get; set; }
        public int LevelChangePositionNo { get; set; }
        public int LevelUpFrom { get; set; }
        public int LevelUpTo { get; set; }
        public string Info { get; set; }
        public Player currentPlayer { get; set; }
        public int MoveToPositon { get; set; }
    }

    public enum LogEventType
    {
        AddPlayer,
        Move,
        IdolContract,
        RoundReward,

        GoldChange,
        CPFee,

        LevelUp,

        ChanceCardWin,
        ChanceCardUse,
        ChangeCardTrigger,
        
        StartGame,

        PositionFix,
        PlayerFix,
        GoldFix,
        LevelFix,
        IdolOwnerFix,

        BackHome,
        Holiday,
        Education,
        HolidayEnd




    }
}
