using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipGame
{
    public class ShipFactory : IShipFactory
    {
       private Ship Ship { get; set; }

        public Ship MakeShip(ShipType shipType)
        {
            switch (shipType)
            {
                case ShipType.Battleship:
                    Ship = new Battleship(5);
                    break;
                case ShipType.Destroyer:
                    Ship = new Destroyer(4);
                    break;
            }

            return Ship;
        }
    }
}
