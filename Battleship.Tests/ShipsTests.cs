using System;
using BattleshipGame;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ShipsTests
    {
        private IBoardGenerator _boardGenerator;
        private IShipFactory _shipFactory;
        private Board _board;

        [SetUp]
        public void Setup()
        {
            _board = new Board(10, 10);
            _boardGenerator = new RandomBoardGenerator(_board);
            _shipFactory = new ShipFactory();
        }

        [Test]
        public void ShouldCreatedShipOfBattleshipType()
        {
            var ship = _shipFactory.MakeShip(ShipType.Battleship);
            Assert.IsInstanceOf<Battleship>(ship);
        }

        [Test]
        public void ShouldCreatedShipOfDestroyerType()
        {
            var ship = _shipFactory.MakeShip(ShipType.Destroyer);
            Assert.IsInstanceOf<Destroyer>(ship);
        }

        [TestCase(5, ShipType.Battleship)]
        [TestCase(4, ShipType.Destroyer)]
        public void ShouldReturnLengthOfShip(int shipLength, ShipType shipType)
        {
            var ship = _shipFactory.MakeShip(shipType);
            Assert.That(ship.CoordinatesFields, Has.Length.EqualTo(shipLength));
        }

        [Test]
        public void ShouldTellThatShipIsSink()
        {
            var ship = _shipFactory.MakeShip(ShipType.Destroyer);
            _boardGenerator.PlaceShipOnTheGrid(ship);
            foreach (var shipCoordinates in ship.CoordinatesFields)
            {
                shipCoordinates.IsHit = true;
            }

            Assert.IsTrue(ship.IsSink);
        }

        [Test]
        public void ShouldNotTellThatShipIsSink()
        {
            var ship = _shipFactory.MakeShip(ShipType.Destroyer);
            _boardGenerator.PlaceShipOnTheGrid(ship);
            for (int i =0; i < ship.CoordinatesFields.Length; i++)
            {
                if(i % 2 == 0)
                    ship.CoordinatesFields[i].IsHit = true;
                ship.CoordinatesFields[i].IsHit = false;
            }

            Assert.IsFalse(ship.IsSink);
        }
    }
}