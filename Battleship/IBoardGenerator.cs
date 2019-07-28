using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipGame
{
    public interface IBoardGenerator
    {
        void PlaceShipOnTheGrid(Ship ship);
        void PlaceListOfShipsOnTheGrid(IEnumerable<Ship> ships);
    }
}
