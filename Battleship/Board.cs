using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipGame
{
    public class Board : IBoard
    {
        public BoardCoordinates[,] Grid { get; set; }
        public int Rows;
        public int Columns;
        private char[] ColumnName = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', };

        public Board(int rows, int columns)
        {
            Rows = rows+2;
            Columns = columns+2;
            Grid = new BoardCoordinates[Rows, Columns];
            FillGrid();
        }

        public void PrintGrid(BoardCoordinates [,] grid)
        {
            Console.Write("__|");
            foreach (var columnName in ColumnName)
            {
                Console.Write($"{columnName} ");
            }
            Console.WriteLine();
            for (int i = 1; i < Rows-1; i++)
            {
                if (i < 10) Console.Write($"{i} |");
                else Console.Write($"{i}|");

                for (int j = 1; j < Columns-1; j++)
                {
                    Console.Write($"{grid[i,j].DisplayValue} ");
                }
                Console.WriteLine();
            }
        }

        private void FillGrid()
        {
            for(int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Grid[i, j] = new BoardCoordinates(i, j);
                }
            }
        }
    }
}
