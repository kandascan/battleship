using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipGame
{
    public interface IGame
    {
        Status ShutShip(string fieldName, List<Ship> ships);
        bool IsGameOver(IEnumerable<Ship> ships);
    }
}
