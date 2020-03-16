using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dfw.Models
{
    public class Position
    {
        public int PositionNumber { get; set; }
        public Card PositionCard { get; set; }
    }

    public class PositionCP
    {
        public List<List<int>> CPList { get; set; }
        public decimal CP2 = (decimal)0.25;
        public decimal CP3 = (decimal)0.5;
        public PositionCP()
        {
            CPList = InitCPList();
        }

        public List<int> CPWith(int PositionNumber)
        {
            var CpWithList = CPList.FirstOrDefault(l => l.Contains(PositionNumber));
            return CpWithList;
        }

        public bool CPAvaliableCheck(Board board, int PositionNumber)
        {
            var CpWithList = CPWith(PositionNumber);
            if (CpWithList == null || CpWithList.Count == 0)
                return false;
            bool avaliable = true;
            string cpHolder = board.BoardMap[CpWithList[0]].PositionCard.HolderId;
            foreach(int p in CpWithList)
            {
                if(board.BoardMap[p].PositionCard.HolderId != cpHolder)
                {
                    avaliable = false;
                }
            }
            return avaliable;
        }

        public int CPCost(Board board, int positionNumber)
        {
            int cost = 0;
            var cpList = CPWith(positionNumber);
            bool isCPAvalible = CPAvaliableCheck(board, positionNumber);
            if (isCPAvalible)
            {
                if ( cpList.Count == 2 )
                {
                    var card = board.BoardMap[positionNumber].PositionCard;
                    cost = (int)Math.Ceiling((decimal)(card.Fee[card.Level] * CP2)/10) * 10;
                }
                if ( cpList.Count == 3 )
                {
                    var card = board.BoardMap[positionNumber].PositionCard;
                    cost = (int)Math.Ceiling((decimal)(card.Fee[card.Level] * CP3)/10) * 10;
                }
            }

            return cost;
        }

        private List<List<int>> InitCPList()
        {
            List<List<int>> initList = new List<List<int>>();
            List<int> group1 = new List<int>() { 1, 2, 3 }; 
            List<int> group2 = new List<int>() { 5, 6 };
            List<int> group3 = new List<int>() { 8, 9 };
            List<int> group4 = new List<int>() { 12, 13 };
            List<int> group5 = new List<int>() { 16, 17 };
            List<int> group6 = new List<int>() { 19, 20, 21 };
            List<int> group7 = new List<int>() { 24, 25 };
            List<int> group8 = new List<int>() { 27, 28 };
            List<int> group9 = new List<int>() { 31, 32 };
            List<int> group10 = new List<int>() { 34, 35 };
            initList.Add(group1);
            initList.Add(group2);
            initList.Add(group3);
            initList.Add(group4);
            initList.Add(group5);
            initList.Add(group6);
            initList.Add(group7);
            initList.Add(group8);
            initList.Add(group9);
            initList.Add(group10);
            return initList;
        }
    }

    public class Board
    {
        public List<Position> BoardMap { get; set; }
    }
}
