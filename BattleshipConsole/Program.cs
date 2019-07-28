using BattleshipGame;
using System;
using System.Collections.Generic;

namespace BattleshipConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var shipFactory = new ShipFactory();
            var board = new Board(10, 10);
            var boardGenerator = new RandomBoardGenerator(board);

            var ships = new List<Ship>();
            ships.Add(shipFactory.MakeShip(ShipType.Battleship));
            ships.Add(shipFactory.MakeShip(ShipType.Destroyer));
            ships.Add(shipFactory.MakeShip(ShipType.Destroyer));

            boardGenerator.PlaceListOfShipsOnTheGrid(ships);
            board.PrintGrid(board.Grid);
        }
    }
}
