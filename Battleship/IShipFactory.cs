using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipGame
{
    public interface IShipFactory
    {
        Ship MakeShip(ShipType shipType);
    }
}
