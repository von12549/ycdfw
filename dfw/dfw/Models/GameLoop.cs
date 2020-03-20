using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace dfw.Models
{
    public partial class Game
    {
        #region 主菜单
        public void GameMainLoop()
        {
            bool gameRun = true;
            while (gameRun)
            {
                Display.DisplayMainMenu();
                string selected = Console.ReadLine().Trim();
                while(selected == "")
                {
                    selected = Console.ReadLine().Trim();
                }
                if(string.Equals(selected, "exit", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
                switch (selected)
                {
                    case "1":
                        Display.DisplayBoard(GameBoard, Players);
                        break;
                    case "2":
                        AddPlayer();
                        break;
                    case "3":
                        foreach (var player in Players)
                        {
                            Display.DisplayPlayer(player, GameBoard);
                        }
                        break;
                    case "4":
                        if (Players.Count == 0)
                        {
                            Console.WriteLine("玩家总数为0，无法开始游戏！");
                            break;
                        }
                        GameLoopSecond();
                        break;
                    case "5":
                        foreach (var log in Logs)
                        {
                            Display.DisplayLog(log, GameBoard);
                            Records.RecordLog(log, GameBoard);
                        }
                        break;
                    case "6":
                        GameLoopFix();
                        break;
                    case "0":
                        gameRun = false;
                        break;
                    default:
                        Console.WriteLine("请从菜单选项中输入：（0 - 6）");
                        break;
                }
            }
        }

        #endregion

        #region 游戏菜单
        public void GameLoopSecond()
        {
            bool gameRun = true;
            while (gameRun)
            {
                
                Display.DisplayGameMenu();
                string selected = Console.ReadLine().Trim();
                while (selected == "")
                {
                    selected = Console.ReadLine().Trim();
                }
                if (string.Equals(selected, "exit", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
                switch (selected)
                {
                    case "1":
                        var moveResult = Move();
                        break;
                    case "2":
                        bool autoRun = true;
                        while (autoRun)
                        {
                            var autoMoveResult = Move(true, true);
                            Thread.Sleep(50);
                            foreach (var p in Players)
                            {
                                if (p.Gold < 0)
                                {
                                    autoRun = false;
                                }
                            }
                        }
                        EndGame();
                        break;
                    case "3":
                        var cardUse = ChanceCardUse();
                        break;
                    case "4":
                        var mortResult = Mortgage();
                        break;
                    case "5":
                        var redeemResult = Redeem();
                        break;
                    case "6":
                        Display.DisplayCH2(ChanceDeck, ChanceUsed, ChangeDeck, ChangeUsed, Players);
                        break;
                    case "7":
                        foreach (var player in Players)
                        {
                            Display.DisplayPlayer(player, GameBoard);
                        }
                        break;
                    case "0":
                        gameRun = false;
                        break;
                    default:
                        Console.WriteLine("请从菜单选项中输入：（0 - 6）");
                        break;
                }
            }
        }
        #endregion

        #region 数据修正菜单
        public void GameLoopFix()
        {
            bool gameRun = true;
            while (gameRun)
            {
                Display.DisplayDataFixMenu();
                string selected = Console.ReadLine().Trim();
                while (selected == "")
                {
                    selected = Console.ReadLine().Trim();
                }
                if (string.Equals(selected, "exit", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
                switch (selected)
                {
                    case "1":
                        var playerNameFixResult = PlayerNameFix();
                        break;
                    case "2":
                        var playerPositionFixResult = PlayerPositionFix();
                        break;
                    case "3":
                        var goldFixResult = GoldFix();
                        break;
                    case "4":
                        var levelFixResult = LevelFix();
                        break;
                    case "5":
                        var ownerFixResult = IdolOwnerFix();
                        break;
                    case "0":
                        gameRun = false;
                        break;
                    default:
                        Console.WriteLine("请从菜单选项中输入：（0 - 5）");
                        break;
                }
            }
        }
        #endregion
    }
}
