using System;
using System.Collections.ObjectModel;

namespace ChessApp.Models
{
    public abstract class ChessPiece
    {
        public string Color { get; set; } 
        public (int X, int Y) Position { get; set; }

        protected ChessPiece(string color, (int, int) position)
        {
            Color = color;
            Position = position;
        }

        public bool Move((int, int) newPosition)
        {
            if (CanMove(newPosition))
            {
                Position = newPosition;
                return true;
            }
            return false;
        }

        protected abstract bool CanMove((int, int) position);
    }
}
