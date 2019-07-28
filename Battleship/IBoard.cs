using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipGame
{
    public interface IBoard
    {
         void PrintGrid(BoardCoordinates[,] grid);
    }
}
