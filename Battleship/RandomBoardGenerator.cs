using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipGame
{
    public class RandomBoardGenerator : BoardGenerator, IRandomBoardGenerator
    {
        private Random _randFirstCoordinateOfShip { get; set; }
        private Random _randSecondCoordinateOfShip { get; set; }
        private Random _orientation { get; set; }
        private Board _board { get; set; }

        public RandomBoardGenerator(Board board) : base(board)
        {
            _randFirstCoordinateOfShip = new Random();
            _randSecondCoordinateOfShip = new Random();
            _orientation = new Random();
            _board = board;
        }

        public bool CheckIfItPossibleToPlaceAShipOnTheGrid(Ship ship, ShipOrientation orientation, Coordinates coordinates)
        {
            for (int i = 0; i < ship.CoordinatesFields.Count(); i++)
            {
                int x = orientation == ShipOrientation.Vertical ? coordinates.X + i : coordinates.X;
                int y = orientation == ShipOrientation.Horizontal ? coordinates.Y + i : coordinates.Y;

                if (_board.Grid[x, y].IsOccupied || _board.Grid[x + 1, y].IsOccupied || _board.Grid[x, y + 1].IsOccupied ||
                    _board.Grid[x + 1, y + 1].IsOccupied || _board.Grid[x - 1, y].IsOccupied || _board.Grid[x, y - 1].IsOccupied ||
                    _board.Grid[x - 1, y - 1].IsOccupied || _board.Grid[x - 1, y + 1].IsOccupied || _board.Grid[x + 1, y - 1].IsOccupied)
                    return false;
            }

            return true;
        }

        private void UpdateGrid(Ship ship, ShipOrientation orientation, Coordinates coordinates)
        {
            for (int i = 0; i < ship.CoordinatesFields.Count(); i++)
            {
                int x = orientation == ShipOrientation.Vertical ? coordinates.X + i : coordinates.X;
                int y = orientation == ShipOrientation.Horizontal ? coordinates.Y + i : coordinates.Y;
                _board.Grid[x, y].IsOccupied = true;
                _board.Grid[x, y].ShipId = ship.Id;
                _board.Grid[x, y].Status = Status.Occupied;
                ship.CoordinatesFields[i] = new ShipCoordinates(x, y);
            }
        }

        private void ValidateGeneratiedCoordinatesForPlacingShip(Ship ship, ShipOrientation orientation, Coordinates coordinates)
        {
            if (!CheckIfItPossibleToPlaceAShipOnTheGrid(ship, orientation, coordinates))
                PlaceShipOnTheGrid(ship);
            else
                UpdateGrid(ship, orientation, coordinates);
        }

        public Coordinates GetStartingCoordinatesForShip(Ship ship, ShipOrientation orientation)
        {
            int endIndex = GetEndIndexForShipRandom(ship, orientation);
            int firstCoordinate = _randFirstCoordinateOfShip.Next(1, endIndex);
            int endIndexForSecondCoordinate = orientation == ShipOrientation.Vertical ? _board.Columns - 1 : _board.Rows - 1;
            int secondCoordinate = _randSecondCoordinateOfShip.Next(1, endIndexForSecondCoordinate);
            int x = orientation == ShipOrientation.Vertical ? firstCoordinate : secondCoordinate;
            int y = orientation == ShipOrientation.Horizontal ? firstCoordinate : secondCoordinate;
            var coordinate = new ShipCoordinates(x, y);
            return coordinate;
        }

        public Tuple<ShipOrientation, Coordinates> GenerateOrientationAndCoordinates(Ship ship)
        {
            var orientation = _orientation.Next(0, 20) % 2 == 0 ? ShipOrientation.Vertical : ShipOrientation.Horizontal;
            var coordinates = GetStartingCoordinatesForShip(ship, orientation);
            return new Tuple<ShipOrientation, Coordinates>(orientation, coordinates);
        }

        public new void PlaceListOfShipsOnTheGrid(IEnumerable<Ship> ships)
        {
            foreach (var ship in ships)
            {
                PlaceShipOnTheGrid(ship);
            }
        }

        public new void PlaceShipOnTheGrid(Ship ship)
        {
            var result = GenerateOrientationAndCoordinates(ship);
            ValidateGeneratiedCoordinatesForPlacingShip(ship, result.Item1, result.Item2);
        }

        public int GetEndIndexForShipRandom(Ship ship, ShipOrientation orientation)
        {
            int endIndex = 0;
            switch (orientation)
            {
                case ShipOrientation.Vertical:
                    endIndex = _board.Rows - 1 - ship.CoordinatesFields.Count();
                    break;
                case ShipOrientation.Horizontal:
                    endIndex = _board.Columns - 1 - ship.CoordinatesFields.Count();
                    break;
            }
            return endIndex;
        }
    }
}
