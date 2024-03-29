﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleshipGame;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class GameTests
    {
        private IShipFactory _shipFactory;
        private IBoardGenerator _boardGenerator;
        private IGame _game;
        private Board _board;

        [SetUp]
        public void Setup()
        {
            _shipFactory = new ShipFactory();
            _board = new Board(10, 10);
            _game = new Game(_board);
            _boardGenerator = new RandomBoardGenerator(_board);
        }

        [Test]
        public void ShouldEndTheGame()
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

            ships.ForEach(s => s.CoordinatesFields.All(c => c.IsHit = true));
            bool gameOver = _game.IsGameOver(ships);

            Assert.IsTrue(gameOver);
        }

        [Test]
        public void ShouldntEndTheGame()
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

            ships.ForEach(s => s.CoordinatesFields.Where(a => a.X % 2 == 0 && a.Y % 2 == 0).All(c => c.IsHit = true));
            bool gameOver = _game.IsGameOver(ships);

            Assert.IsFalse(gameOver);
        }

        [Test]
        public void ShouldShutTheShip()
        {
            var ships = CreateShips();

            (_boardGenerator as BoardGenerator).PlaceListOfShipsOnTheGrid(ships);

            var result = _game.ShutShip("B2", ships);

            Assert.AreEqual(Status.Hit, result);
        }

        private List<Ship> CreateShips()
        {
            var ships = new List<Ship>();

            ships.Add(_shipFactory.MakeShip(ShipType.Battleship));
            ships.Add(_shipFactory.MakeShip(ShipType.Destroyer));
            ships.Add(_shipFactory.MakeShip(ShipType.Destroyer));
            int[] column = new int[] { 2, 4, 6 };
            int columnIterator = 0;


            foreach (var ship in ships)
            {
                for (int i = 0; i < ship.CoordinatesFields.Length; i++)
                {
                    ship.CoordinatesFields[i] = new ShipCoordinates(column[columnIterator], i + 2);
                }

                columnIterator++;
            }

            return ships;
        }

        [Test]
        public void ShouldSinkTheShip()
        {
            var ships = CreateShips();

            (_boardGenerator as BoardGenerator).PlaceListOfShipsOnTheGrid(ships);

            var result = Status.Empty;
            foreach (var coordinates in ships[0].CoordinatesFields)
            {
                result = _game.ShutShip(coordinates.FieldName, ships);
            }

            Assert.AreEqual(Status.Sink, result);

        }
    }
}
