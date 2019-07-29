using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipGame
{
    public class BoardCoordinates : Coordinates
    {
        public bool IsOccupied { get; set; }
        public Guid ShipId { get; set; }
        public BoardCoordinates(int x, int y) : base(x, y)
        {
            IsOccupied = false;
        }

        public char DisplayValue
        {
            get
            {
                char status;
                switch (Status)
                {
                    //case Status.Occupied:
                    //    status = '#';
                    //    break;
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
