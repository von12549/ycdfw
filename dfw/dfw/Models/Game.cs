using System;
using System.Collections.Generic;
using System.Text;

namespace dfw.Models
{
    public partial class Game
    {
        public Board GameBoard { get; set; }
        public List<Player> Players { get; set; }
        public List<int> ChanceDeck { get; set; }
        public List<int> ChanceUsed { get; set; }
        public List<int> ChangeDeck { get; set; }
        public List<int> ChangeUsed { get; set; }
        public List<GameLogs> Logs { get; set; }
        private PositionCP CPManager { get; set; }
        private int Round { get; set; }
        private int PlayerMoved { get; set; }
        private Player currentPlayer { get; set; }
        private int? MinContract { get; set; }
        private int? MinLevelUp { get; set; }
        private string FileName { get; set; }
        private GameReords Records { get; set; }
        public Game()
        {
            Init();
            GameMainLoop();
            
        }

        public void Init()
        {
            GameBoard = InitBoard();
            Players = new List<Player>();
            CPManager = new PositionCP();
            ChangeDeck = InitChangeDeck();
            ChangeUsed = InitChangeUsed();
            ChanceDeck = InitChanceDeck();
            ChanceUsed = InitChanceUsed();
            Records = InitGameRecords();
            MinContract = null;
            MinLevelUp = null;
            Round = 0;
            PlayerMoved = 0;
            currentPlayer = new Player() { Id = "-1" };
            Logs = InitLogs();
        }

        

        

        



        #region Board Loading
        private Board InitBoard()
        {
            Board board = new Board();
            CardPool cardPool = new CardPool();

            Position position00 = new Position() { PositionNumber = 0, PositionCard = cardPool.GenerateCard("Home") };
            Position position01 = new Position() { PositionNumber = 1, PositionCard = cardPool.GenerateCard("Idol02") };
            Position position02 = new Position() { PositionNumber = 2, PositionCard = cardPool.GenerateCard("Idol13") };
            Position position03 = new Position() { PositionNumber = 3, PositionCard = cardPool.GenerateCard("Idol01") };
            Position position04 = new Position() { PositionNumber = 4, PositionCard = cardPool.GenerateCard("Chance") };
            Position position05 = new Position() { PositionNumber = 5, PositionCard = cardPool.GenerateCard("Idol06") };
            Position position06 = new Position() { PositionNumber = 6, PositionCard = cardPool.GenerateCard("Idol09") };
            Position position07 = new Position() { PositionNumber = 7, PositionCard = cardPool.GenerateCard("SPEC1") };
            Position position08 = new Position() { PositionNumber = 8, PositionCard = cardPool.GenerateCard("Idol10") };
            Position position09 = new Position() { PositionNumber = 9, PositionCard = cardPool.GenerateCard("Idol04") };
            Position position10 = new Position() { PositionNumber = 10, PositionCard = cardPool.GenerateCard("Change") };
            Position position11 = new Position() { PositionNumber = 11, PositionCard = cardPool.GenerateCard("EDU") };
            Position position12 = new Position() { PositionNumber = 12, PositionCard = cardPool.GenerateCard("Idol05") };
            Position position13 = new Position() { PositionNumber = 13, PositionCard = cardPool.GenerateCard("Idol08") };
            Position position14 = new Position() { PositionNumber = 14, PositionCard = cardPool.GenerateCard("SPEC2") };
            Position position15 = new Position() { PositionNumber = 15, PositionCard = cardPool.GenerateCard("Chance") };
            Position position16 = new Position() { PositionNumber = 16, PositionCard = cardPool.GenerateCard("Idol04") };
            Position position17 = new Position() { PositionNumber = 17, PositionCard = cardPool.GenerateCard("Idol09") };
            Position position18 = new Position() { PositionNumber = 18, PositionCard = cardPool.GenerateCard("BACKHOME") };
            Position position19 = new Position() { PositionNumber = 19, PositionCard = cardPool.GenerateCard("Idol01") };
            Position position20 = new Position() { PositionNumber = 20, PositionCard = cardPool.GenerateCard("Idol12") };
            Position position21 = new Position() { PositionNumber = 21, PositionCard = cardPool.GenerateCard("Idol02") };
            Position position22 = new Position() { PositionNumber = 22, PositionCard = cardPool.GenerateCard("Change") };
            Position position23 = new Position() { PositionNumber = 23, PositionCard = cardPool.GenerateCard("SPEC3") };
            Position position24 = new Position() { PositionNumber = 24, PositionCard = cardPool.GenerateCard("Idol03") };
            Position position25 = new Position() { PositionNumber = 25, PositionCard = cardPool.GenerateCard("Idol11") };
            Position position26 = new Position() { PositionNumber = 26, PositionCard = cardPool.GenerateCard("Chance") };
            Position position27 = new Position() { PositionNumber = 27, PositionCard = cardPool.GenerateCard("Idol08") };
            Position position28 = new Position() { PositionNumber = 28, PositionCard = cardPool.GenerateCard("Idol07") };
            Position position29 = new Position() { PositionNumber = 29, PositionCard = cardPool.GenerateCard("HOLIDAY") };
            Position position30 = new Position() { PositionNumber = 30, PositionCard = cardPool.GenerateCard("Change") };
            Position position31 = new Position() { PositionNumber = 31, PositionCard = cardPool.GenerateCard("Idol06") };
            Position position32 = new Position() { PositionNumber = 32, PositionCard = cardPool.GenerateCard("Idol05") };
            Position position33 = new Position() { PositionNumber = 33, PositionCard = cardPool.GenerateCard("SPEC4") };
            Position position34 = new Position() { PositionNumber = 34, PositionCard = cardPool.GenerateCard("Idol07") };
            Position position35 = new Position() { PositionNumber = 35, PositionCard = cardPool.GenerateCard("Idol03") };


            board.BoardMap = new List<Position>()
            {
                position00,
                position01,
                position02,
                position03,
                position04,
                position05,
                position06,
                position07,
                position08,
                position09,
                position10,
                position11,
                position12,
                position13,
                position14,
                position15,
                position16,
                position17,
                position18,
                position19,
                position20,
                position21,
                position22,
                position23,
                position24,
                position25,
                position26,
                position27,
                position28,
                position29,
                position30,
                position31,
                position32,
                position33,
                position34,
                position35,
            };
            return board;

        }
        #endregion
        #region Chance Loading
        private List<int> InitChanceDeck()
        {
            List<int> cards = new List<int>()
            {1,2,3,4,5,6,7,8,9,10,
            11,12,13,14,15,16,17,18,19,20,
            21,22,23,24,25,26,27};
            return cards;
        }

        private List<int> InitChanceUsed()
        {
            List<int> cards = new List<int>();
            return cards;
        }
        #endregion
        #region Change Loading
        private List<int> InitChangeDeck()
        {
            List<int> cards = new List<int>()
            {1,2,3,4,5,6,7,8,9,10,
            11,12,13,14,15,16,17,18,19,20,
            21,22,23,24,25,26,27};
            return cards;
        }

        private List<int> InitChangeUsed()
        {
            List<int> cards = new List<int>();
            return cards;
        }
        #endregion
        #region Logs Loading
        private List<GameLogs> InitLogs()
        {
            Logs = new List<GameLogs>();
            AddLog(new GameLogs()
            {
                ID = 0,
                Type = LogEventType.StartGame,
                TimeStamp = DateTime.Now,
                Info = "游戏启动",
                Round = Round,
                currentPlayer = currentPlayer
            });
            return Logs;

        }

        private GameReords InitGameRecords()
        {
            FileName = GameReords.GenerateFileName();
            GameReords record = new GameReords(FileName);
            return record;
        }
        #endregion
        #region Assist Check Function
        private bool NumberCheck(string str)
        {
            bool isNum = true;
            foreach (var c in str)
            {
                if (!(c >= '0' && c <= '9'))
                {
                    isNum = false;
                    break;
                }
            }
            return isNum;
        }
        private int GetLogID()
        {
            if(Logs == null || Logs.Count == 0)
            {
                return 0;
            }
            return Logs.Count;
        }

        private List<GameLogs> AddLog(GameLogs log)
        {
            Logs.Add(log);
            Display.DisplayLog(log, GameBoard);
            Records.RecordLog(log, GameBoard);
            return Logs;
        }

        private int PickChanceCard()
        {
            Random rnd = new Random();
            int num = rnd.Next(0, 500);
            num = num % ChanceDeck.Count;
            return ChanceDeck[num];
        }
        private int PickChangeCard()
        {
            Random rnd = new Random();
            int num = rnd.Next(0, 500);
            num = num % ChangeDeck.Count;
            return ChangeDeck[num];
        }

        private bool ReadIntNumber(out int number)
        {
            number = -1;
            try
            {
                bool result = false;
                string input = Console.ReadLine().Trim();
                while(input.Length == 0)
                {
                    input = Console.ReadLine().Trim();
                }

                if (string.Equals(input.Trim(), "exit", StringComparison.OrdinalIgnoreCase))
                { return false; }

                else if (string.Equals(input.Trim(), "quit", StringComparison.OrdinalIgnoreCase))
                { return false; }
                else if (string.Equals(input.Trim(), "bye", StringComparison.OrdinalIgnoreCase))
                { return false; }
                else
                {
                    result = NumberCheck(input);
                }
                Int32.TryParse(input.Trim(), out number);
                return result;
            }
            catch (Exception)
            {
                
            }
            return false;
        }


        #endregion

        #region 游戏结束
        private void EndGame()
        {
            Records.AppendRecordsLine("###\t 游戏结束 \t###");
            Records.AppendRecordsLine("###\t 棋盘信息 \t###");
            Records.RecordBoard(GameBoard, Players);
            Records.AppendRecordsLine("###\t 玩家信息 \t###");
            foreach (var p in Players)
            {
                Records.RecordPlayer(p, GameBoard);
            }
        }

        #endregion

    }
}
