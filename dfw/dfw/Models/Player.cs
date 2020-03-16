using System;
using System.Collections.Generic;
using System.Text;

namespace dfw.Models
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; } = "Player";
        public int Gold { get; set; } = 15000;
        public int PositionNumber { get; set; } = 0;
        public bool Stop { get; set; } = false;
        public bool CanWin { get; set; } = true;
        public List<int> ChanceCards { get; set; } = new List<int>();
    }

}
