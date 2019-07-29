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
            var game = new Game(board);

            var ships = new List<Ship>();
            ships.Add(shipFactory.MakeShip(ShipType.Battleship));
            ships.Add(shipFactory.MakeShip(ShipType.Destroyer));
            ships.Add(shipFactory.MakeShip(ShipType.Destroyer));

            boardGenerator.PlaceListOfShipsOnTheGrid(ships);

            string fieldName = String.Empty;
            while (!game.IsGameOver(ships))
            {
                board.PrintGrid(board.Grid);
                Console.Write("Please enter the coordinate to shut the ship: ");
                fieldName = Console.ReadLine();
                var status = game.ShutShip(fieldName, ships);
                Console.Clear();
                Console.WriteLine($"You: {status} the ship");
            }
            Console.Clear();
            Console.WriteLine("Game over;");
            board.PrintGrid(board.Grid);
            Console.ReadKey();
        }
    }
}
