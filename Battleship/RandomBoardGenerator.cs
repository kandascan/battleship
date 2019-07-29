using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipGame
{
    public class RandomBoardGenerator : BoardGenerator, IRandomBoardGenerator
    {
        private Random RandFirstCoordinateOfShip { get; set; }
        private Random RandSecondCoordinateOfShip { get; set; }
        private Random Orientation { get; set; }
        private Board Board { get; set; }

        public RandomBoardGenerator(Board board) : base(board)
        {
            RandFirstCoordinateOfShip = new Random();
            RandSecondCoordinateOfShip = new Random();
            Orientation = new Random();
            Board = board;
        }

        public bool CheckIfItPossibleToPlaceAShipOnTheGrid(Ship ship, ShipOrientation orientation, Coordinates coordinates)
        {
            for (int i = 0; i < ship.CoordinatesFields.Count(); i++)
            {
                int x = orientation == ShipOrientation.Vertical ? coordinates.X + i : coordinates.X;
                int y = orientation == ShipOrientation.Horizontal ? coordinates.Y + i : coordinates.Y;

                if (Board.Grid[x, y].IsOccupied || Board.Grid[x + 1, y].IsOccupied || Board.Grid[x, y + 1].IsOccupied ||
                    Board.Grid[x + 1, y + 1].IsOccupied || Board.Grid[x - 1, y].IsOccupied || Board.Grid[x, y - 1].IsOccupied ||
                    Board.Grid[x - 1, y - 1].IsOccupied || Board.Grid[x - 1, y + 1].IsOccupied || Board.Grid[x + 1, y - 1].IsOccupied)
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
                Board.Grid[x, y].IsOccupied = true;
                Board.Grid[x, y].ShipId = ship.Id;
                Board.Grid[x, y].Status = Status.Occupied;
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
            int firstCoordinate = RandFirstCoordinateOfShip.Next(1, endIndex);
            int endIndexForSecondCoordinate = orientation == ShipOrientation.Vertical ? Board.Columns - 1 : Board.Rows - 1;
            int secondCoordinate = RandSecondCoordinateOfShip.Next(1, endIndexForSecondCoordinate);
            int x = orientation == ShipOrientation.Vertical ? firstCoordinate : secondCoordinate;
            int y = orientation == ShipOrientation.Horizontal ? firstCoordinate : secondCoordinate;
            var coordinate = new ShipCoordinates(x, y);
            return coordinate;
        }

        public Tuple<ShipOrientation, Coordinates> GenerateOrientationAndCoordinates(Ship ship)
        {
            var orientation = Orientation.Next(0, 20) % 2 == 0 ? ShipOrientation.Vertical : ShipOrientation.Horizontal;
            var coordinates = GetStartingCoordinatesForShip(ship, orientation);
            return new Tuple<ShipOrientation, Coordinates>(orientation, coordinates);
        }

        public override void PlaceShipOnTheGrid(Ship ship)
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
                    endIndex = Board.Rows - 1 - ship.CoordinatesFields.Count();
                    break;
                case ShipOrientation.Horizontal:
                    endIndex = Board.Columns - 1 - ship.CoordinatesFields.Count();
                    break;
            }
            return endIndex;
        }
    }
}
