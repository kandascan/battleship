using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipGame
{
    public class ShipCoordinates : Coordinates
    {
        public bool IsHit { get; set; }  
        public ShipCoordinates(int x, int y) :base(x,y)
        {
            IsHit = false;
        }
    }
}
