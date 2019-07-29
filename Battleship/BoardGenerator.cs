using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipGame
{
    public class BoardGenerator : IBoardGenerator
    {
        private Board _board { get; set; }

        public BoardGenerator(Board board)
        {
            _board = board;
        }

        public virtual void PlaceListOfShipsOnTheGrid(IEnumerable<Ship> ships)
        {
            foreach (var ship in ships)
            {
                PlaceShipOnTheGrid(ship);
            }
        }

        public virtual void PlaceShipOnTheGrid(Ship ship)
        {
            foreach (var coordinates in ship.CoordinatesFields)
            {
                for (int i = 0; i < _board.Rows; i++)
                {
                    for (int j = 0; j < _board.Columns; j++)
                    {
                        if (_board.Grid[i, j].FieldName == coordinates.FieldName)
                        {
                            _board.Grid[i, j].Status = Status.Occupied;
                            _board.Grid[i, j].ShipId = ship.Id;
                        }
                    }
                }
            }
        }
    }
}
