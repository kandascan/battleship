using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleshipGame
{
    public class Game : IGame
    {
        private Board _board;
        public Game(Board board)
        {
            _board = board;
        }

        public bool IsGameOver(IEnumerable<Ship> ships)
        {
            return ships.All(s => s.IsSink);
        }

        public Status ShutShip(string fieldName, List<Ship> ships)
        {
            Status result = Status.Miss;
            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Columns; j++)
                {
                    if (_board.Grid[i, j].FieldName == fieldName.ToUpper())
                    {
                        var ship = ships.FirstOrDefault(s => s.Id == _board.Grid[i, j].ShipId);
                        if (ship != null)
                        {
                            var coordinate = ship.CoordinatesFields.FirstOrDefault(c => c.X == i && c.Y == j);
                            if (coordinate != null)
                            {
                                result = Status.Hit;
                                coordinate.Status = result;
                                coordinate.IsHit = true;
                            }

                            if (ship.IsSink)
                                result = Status.Sink;
                        }
                        _board.Grid[i, j].Status = result;
                    }
                }
            }

            return result;
        }
    }
}
