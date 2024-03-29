﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipGame
{
    public abstract class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string FieldName { get; set; }
        private char[] _columnName = new char[] { '-', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', '-'};
        public Status Status { get; set; }
        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
            FieldName = $"{_columnName[y]}{x}";
            Status = Status.Empty;
        }
    }
}
