using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                int selected = -1;
                ReadIntNumber(out selected);
                switch (selected)
                {
                    case 1:
                        Display.DisplayBoard(GameBoard, Players);
                        break;
                    case 2:
                        AddPlayer();
                        break;
                    case 3:
                        foreach (var player in Players)
                        {
                            Display.DisplayPlayer(player);
                        }
                        break;
                    case 4:
                        if (Players.Count == 0)
                        {
                            Console.WriteLine("玩家总数为0，无法开始游戏！");
                            break;
                        }
                        GameLoopSecond();
                        break;
                    case 5:
                        foreach (var log in Logs)
                        {
                            Display.DisplayLog(log, GameBoard);
                        }
                        break;
                    case 6:
                        GameLoopFix();
                        break;
                    case 0:
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
                if (PlayerMoved == 0)
                {
                    currentPlayer = Players[0];
                }
                else
                {
                    currentPlayer = Players[PlayerMoved % Players.Count];
                }
                Display.DisplayGameMenu();
                int selected = -1;
                ReadIntNumber(out selected);
                switch (selected)
                {
                    case 1:
                        var moveResult = Move();
                        break;
                    case 2:
                        var cardUse = ChanceCardUse();
                        break;
                    case 3:
                        Display.DisplayCH2(ChanceDeck, ChanceUsed, ChangeDeck, ChangeUsed, Players);
                        break;
                    case 4:
                        foreach (var player in Players)
                        {
                            Display.DisplayPlayer(player);
                        }
                        break;
                    case 0:
                        gameRun = false;
                        break;
                    default:
                        Console.WriteLine("请从菜单选项中输入：（0 - 4）");
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
                int selected = -1;
                ReadIntNumber(out selected);
                switch (selected)
                {
                    case 1:
                        var playerNameFixResult = PlayerNameFix();
                        break;
                    case 2:
                        var playerPositionFixResult = PlayerPositionFix();
                        break;
                    case 3:
                        var goldFixResult = GoldFix();
                        break;
                    case 4:
                        var levelFixResult = LevelFix();
                        break;
                    case 5:
                        var ownerFixResult = IdolOwnerFix();
                        break;
                    case 0:
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
