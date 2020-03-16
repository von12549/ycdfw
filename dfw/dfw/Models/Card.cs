using System;
using System.Collections.Generic;
using System.Text;

namespace dfw.Models
{

    public class Card
    {
        public CardType Type { get; set; } = CardType.Idol;
        public string Name { get; set; }
        public int Id { get; set; }
        public string Image { get; set; }
        public int Level { get; set; } = 0;
        public int InitCost { get; set; }
        public int LevelUpCost { get; set; }
        public int[] Fee { get; set; }
        public string Info { get; set; }
        public string HolderId { get; set; } = "-1";
        public bool isMortgage { get; set; } = false;
    }

    public enum CardType
    {
        Idol,
        Special,
        Chance,
        Change,
        Home,
        Back,
        Holiday,
        Education
    }
}
