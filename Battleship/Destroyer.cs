using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipGame
{
    public class Destroyer : Ship
    {
        public Destroyer(int length) : base(length)
        {
            ShipType = ShipType.Destroyer;
        }
    }
}
