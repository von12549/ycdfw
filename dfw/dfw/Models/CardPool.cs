using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace dfw.Models
{
    public class CardPool
    {
        public Card GenerateCard(string card)
        {
            Card gc = new Card();
            string cardStr = JsonConvert.SerializeObject(Home);
            switch (card.ToUpper())
            {
                case "IDOL01":
                    cardStr = JsonConvert.SerializeObject(Idol01);
                    break;
                case "IDOL02":
                    cardStr = JsonConvert.SerializeObject(Idol02);
                    break;
                case "IDOL03":
                    cardStr = JsonConvert.SerializeObject(Idol03);
                    break;
                case "IDOL04":
                    cardStr = JsonConvert.SerializeObject(Idol04);
                    break;
                case "IDOL05":
                    cardStr = JsonConvert.SerializeObject(Idol05);
                    break;
                case "IDOL06":
                    cardStr = JsonConvert.SerializeObject(Idol06);
                    break;
                case "IDOL07":
                    cardStr = JsonConvert.SerializeObject(Idol07);
                    break;
                case "IDOL08":
                    cardStr = JsonConvert.SerializeObject(Idol08);
                    break;
                case "IDOL09":
                    cardStr = JsonConvert.SerializeObject(Idol09);
                    break;
                case "IDOL10":
                    cardStr = JsonConvert.SerializeObject(Idol10);
                    break;
                case "IDOL11":
                    cardStr = JsonConvert.SerializeObject(Idol11);
                    break;
                case "IDOL12":
                    cardStr = JsonConvert.SerializeObject(Idol12);
                    break;
                case "IDOL13":
                    cardStr = JsonConvert.SerializeObject(Idol13);
                    break;
                case "HOME":
                    cardStr = JsonConvert.SerializeObject(Home);
                    break;
                case "BACKHOME":
                    cardStr = JsonConvert.SerializeObject(BackHome);
                    break;
                case "HOLIDAY":
                    cardStr = JsonConvert.SerializeObject(Holiday);
                    break;
                case "EDU":
                    cardStr = JsonConvert.SerializeObject(Education);
                    break;
                case "SPEC1":
                    cardStr = JsonConvert.SerializeObject(Special1);
                    break;
                case "SPEC2":
                    cardStr = JsonConvert.SerializeObject(Special2);
                    break;
                case "SPEC3":
                    cardStr = JsonConvert.SerializeObject(Special3);
                    break;
                case "SPEC4":
                    cardStr = JsonConvert.SerializeObject(Special4);
                    break;
                case "CHANGE":
                    cardStr = JsonConvert.SerializeObject(Change);
                    break;
                case "CHANCE":
                    cardStr = JsonConvert.SerializeObject(Chance);
                    break;
                default:
                    break;
            }
            gc = JsonConvert.DeserializeObject<Card>(cardStr);
            return gc;
        }


        public Card Idol01 = new Card()
        {
            Type = CardType.Idol,
            Name = "Idol 1",
            Id = 1,
            Info = "Idol 1",
            InitCost = 600,
            LevelUpCost = 500,
            Fee = new int[5] { 20, 100, 300, 900, 2500 }
        };

        public Card Idol02 = new Card()
        {
            Type = CardType.Idol,
            Name = "Idol 2",
            Id = 2,
            Info = "Idol 2",
            InitCost = 1000,
            LevelUpCost = 500,
            Fee = new int[5] { 60, 300, 900, 2700, 5500 }
        };

        public Card Idol03 = new Card()
        {
            Type = CardType.Idol,
            Name = "Idol 3",
            Id = 3,
            Info = "Idol 3",
            InitCost = 1200,
            LevelUpCost = 500,
            Fee = new int[5] { 80, 400, 1000, 3000, 6000 }
        };

        public Card Idol04 = new Card()
        {
            Type = CardType.Idol,
            Name = "Idol 4",
            Id = 4,
            Info = "Idol 4",
            InitCost = 1400,
            LevelUpCost = 1000,
            Fee = new int[5] { 100, 500, 1500, 4500, 7500 }
        };

        public Card Idol05 = new Card()
        {
            Type = CardType.Idol,
            Name = "Idol 5",
            Id = 5,
            Info = "Idol 5",
            InitCost = 1600,
            LevelUpCost = 1000,
            Fee = new int[5] { 120, 600, 1800, 5000, 9000 }
        };

        public Card Idol06 = new Card()
        {
            Type = CardType.Idol,
            Name = "Idol 6",
            Id = 6,
            Info = "Idol 6",
            InitCost = 1800,
            LevelUpCost = 1000,
            Fee = new int[5] { 140, 700, 2000, 5500, 9500 }
        };

        public Card Idol07 = new Card()
        {
            Type = CardType.Idol,
            Name = "Idol 7",
            Id = 7,
            Info = "Idol 7",
            InitCost = 2000,
            LevelUpCost = 1000,
            Fee = new int[5] { 160, 800, 2200, 6000, 10000 }
        };

        public Card Idol08 = new Card()
        {
            Type = CardType.Idol,
            Name = "Idol 8",
            Id = 8,
            Info = "Idol 8",
            InitCost = 2200,
            LevelUpCost = 1500,
            Fee = new int[5] { 180, 900, 2500, 7000, 10500 }
        };

        public Card Idol09 = new Card()
        {
            Type = CardType.Idol,
            Name = "Idol 9",
            Id = 9,
            Info = "Idol 9",
            InitCost = 2600,
            LevelUpCost = 1500,
            Fee = new int[5] { 220, 1100, 3300, 8000, 11500 }
        };

        public Card Idol10 = new Card()
        {
            Type = CardType.Idol,
            Name = "Idol 10",
            Id = 10,
            Info = "Idol 10",
            InitCost = 2800,
            LevelUpCost = 1500,
            Fee = new int[5] { 240, 1200, 3600, 8500, 12000 }
        };

        public Card Idol11 = new Card()
        {
            Type = CardType.Idol,
            Name = "Idol 11",
            Id = 11,
            Info = "Idol 11",
            InitCost = 3000,
            LevelUpCost = 2000,
            Fee = new int[5] { 260, 1300, 3900, 9000, 12750 }
        };

        public Card Idol12 = new Card()
        {
            Type = CardType.Idol,
            Name = "Idol 12",
            Id = 12,
            Info = "Idol 12",
            InitCost = 3200,
            LevelUpCost = 2000,
            Fee = new int[5] { 280, 1500, 4500, 10000, 14000 }
        };

        public Card Idol13 = new Card()
        {
            Type = CardType.Idol,
            Name = "Idol 13",
            Id = 13,
            Info = "Idol 13",
            InitCost = 3200,
            LevelUpCost = 2000,
            Fee = new int[5] { 500, 2000, 6000, 14000, 20000 }
        };

        public Card Home = new Card()
        {
            Type = CardType.Home,
            Name = "Start",
            Id = 20,
            Info = "每当玩家走完一圈经过起点时，金币+2000"
        };

        public Card BackHome = new Card()
        {
            Type = CardType.Back,
            Name = "Go Home",
            Id = 21,
            Info = "直接回到起点，并获得一张【命运】卡，但不能领取起点奖励。"
        };

        public Card Holiday = new Card()
        {
            Type = CardType.Holiday,
            Name = "Holiday",
            Id = 22,
            Info = "休息一轮，并可以抽取一张【机会】卡，休息期间无收益，以跳过该玩家掷骰子为结束标志。"
        };

        public Card Education = new Card()
        {
            Type = CardType.Education,
            Name = "Again",
            Id = 23,
            Info = "再玩一次"
        };

        

        public Card Special1 = new Card()
        {
            Type = CardType.Special,
            Name = "Movie",
            Id = 24,
            Info = "影视工作室",
            InitCost = 2000,
            HolderId = "-1"
        };

        public Card Special2 = new Card()
        {
            Type = CardType.Special,
            Name = "Publish",
            Id = 25,
            Info = "宣发工作室",
            InitCost = 2000,
            HolderId = "-1"
        };

        public Card Special3 = new Card()
        {
            Type = CardType.Special,
            Name = "Music",
            Id = 26,
            Info = "音乐工作室",
            InitCost = 2000,
            HolderId = "-1"
        };

        public Card Special4 = new Card()
        {
            Type = CardType.Special,
            Name = "Design",
            Id = 27,
            Info = "造型工作室",
            InitCost = 2000,
            HolderId = "-1"
        };

        public Card Chance = new Card()
        {
            Type = CardType.Chance,
            Name = "Chance",
            Id = 28,
            Info = "抽取一张机会牌，并置于玩家手中。",
        };

        public Card Change = new Card()
        {
            Type = CardType.Change,
            Name = "Destiny",
            Id = 29,
            Info = "抽取一张命运牌，并强制执行命运牌所公示的内容。",
        };
    }
}
