using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipGame
{
    public class Battleship : Ship
    {
        public Battleship(int length) : base(length)
        {
            ShipType = ShipType.Battleship;
        }
    }
}
