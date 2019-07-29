using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleshipGame
{
    public abstract class Ship
    {
        public ShipType ShipType { get; set; }
        public bool IsSink
        {
            get
            {
                return CoordinatesFields.All(x => x.IsHit);
            }
        }
        public ShipCoordinates[] CoordinatesFields { get; set; }
        public Guid Id { get; set; }
        public Ship(int length)
        {
            Id = Guid.NewGuid();
            CoordinatesFields = new ShipCoordinates[length];
        }
    }
}
