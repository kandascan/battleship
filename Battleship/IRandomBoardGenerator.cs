using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipGame
{
    public interface IRandomBoardGenerator : IBoardGenerator
    {
        int GetEndIndexForShipRandom(Ship ship, ShipOrientation orientation);
        Coordinates GetStartingCoordinatesForShip(Ship ship, ShipOrientation orientation);
        Tuple<ShipOrientation, Coordinates> GenerateOrientationAndCoordinates(Ship ship);

    }
}
