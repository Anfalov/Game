using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool isAdjacent(Coordinates secondElement)
        {
            if ((secondElement.X - X == 0 && Math.Abs(secondElement.Y - Y) == 1)
                || (secondElement.Y - Y == 0 && Math.Abs(secondElement.X - X) == 1))
                return true;
            else return false;
        }
    }
}
