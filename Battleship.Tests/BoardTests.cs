using System;
using System.Collections.Generic;
using System.Text;
using BattleshipGame;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Tests
{
    [TestFixture]
    public class BoardTests
    {
        private Board _board;
        private IRandomBoardGenerator _boardGenerator;
        private IShipFactory _shipFactory;

        [SetUp]
        public void Setup()
        {
            _board = new Board(10, 10);
            _boardGenerator = new RandomBoardGenerator(_board);
            _shipFactory = new ShipFactory();
        }

        [TestCase(ShipType.Battleship, ShipOrientation.Horizontal, 6)]
        [TestCase(ShipType.Battleship, ShipOrientation.Vertical, 6)]
        [TestCase(ShipType.Destroyer, ShipOrientation.Horizontal, 7)]
        [TestCase(ShipType.Destroyer, ShipOrientation.Vertical, 7)]
        public void ShouldReturnEndIndexForRandomToPlaceShipOnTheGrid(ShipType shipType, ShipOrientation orientation,
            int expectedValue)
        {
            var ship = _shipFactory.MakeShip(shipType);
            var endIndex = _boardGenerator.GetEndIndexForShipRandom(ship, orientation);
            Assert.AreEqual(expectedValue, endIndex);
        }

        [TestCase(ShipType.Battleship, ShipOrientation.Horizontal)]
        [TestCase(ShipType.Destroyer, ShipOrientation.Vertical)]
        public void ShouldReturnValidCoordinatesForShip(ShipType shipType, ShipOrientation orientation)
        {
            var ship = _shipFactory.MakeShip(shipType);
            var coordinates = _boardGenerator.GetStartingCoordinatesForShip(ship, orientation);

            Assert.IsInstanceOf<Coordinates>(coordinates);
            Assert.GreaterOrEqual(coordinates.X, 0);
            Assert.GreaterOrEqual(coordinates.Y, 0);
            Assert.LessOrEqual(coordinates.X, 10);
            Assert.LessOrEqual(coordinates.Y, 10);
        }

        [TestCase(ShipType.Battleship, 5)]
        [TestCase(ShipType.Destroyer, 4)]
        public void ShouldPlaceShipOnTheGrid(ShipType shipType, int shipLength)
        {
            var ships = new List<Ship>();
            var ship = _shipFactory.MakeShip(shipType);
            ships.Add(ship);
            _boardGenerator.PlaceListOfShipsOnTheGrid(ships);

            int cellCounter = 0;
            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Columns; j++)
                {
                    if (ship.Id == _board.Grid[i, j].ShipId)
                    {
                        cellCounter++;
                    }
                }
            }

            Assert.AreEqual(shipLength, cellCounter);
        }

        [Test]
        public void ShouldPlaceAllShipsOnTheGrid()
        {
            var ships = new List<Ship>();
            ships.Add(_shipFactory.MakeShip(ShipType.Battleship));
            ships.Add(_shipFactory.MakeShip(ShipType.Destroyer));
            ships.Add(_shipFactory.MakeShip(ShipType.Destroyer));
            _boardGenerator.PlaceListOfShipsOnTheGrid(ships);

            int cellCounter = 0;
            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Columns; j++)
                {
                    if (_board.Grid[i, j].IsOccupied)
                    {
                        cellCounter++;
                    }
                }
            }

            Assert.AreEqual(13, cellCounter);
            Assert.AreEqual(87, 100 - cellCounter);
        }

        [TestCase(ShipType.Battleship)]
        [TestCase(ShipType.Destroyer)]
        public void ShouldGenerateOrientationAndCoordinatesForPlacinShip(ShipType shipType)
        {
            var ship = _shipFactory.MakeShip(shipType);
            var restult = _boardGenerator.GenerateOrientationAndCoordinates(ship);

            Assert.IsInstanceOf<ShipOrientation>(restult.Item1);
            Assert.IsNotNull(restult.Item2.FieldName);
            Assert.GreaterOrEqual(restult.Item2.X, 0);
            Assert.GreaterOrEqual(restult.Item2.Y, 0);
        }
    }
}
