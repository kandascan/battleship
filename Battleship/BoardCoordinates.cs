using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipGame
{
    public class BoardCoordinates : Coordinates
    {
        public bool IsOccupied { get; set; }
        public Guid ShipId { get; set; }
        public bool ShowShips { get; set; }
        public BoardCoordinates(int x, int y, bool showShips) : base(x, y)
        {
            IsOccupied = false;
            ShowShips = showShips;
        }

        public char DisplayValue
        {
            get
            {
                char status;
                switch (Status)
                {
                    case Status.Occupied:
                        status = ShowShips? '#' : '.';
                        break;
                    case Status.Sink:
                    case Status.Hit:
                        status = 'X';
                        break;
                    case Status.Miss:
                        status = 'O';
                        break;
                    default:
                        status = '.';
                        break;
                }

                return status;
            }
        }
    }
}
