using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;

namespace ChessApp.Models
{
    public class Bishop : ChessPiece
    {
        public Bishop(string color, (int, int) position) : base(color, position) { }

        protected override bool CanMove((int, int) position)
        {
            return Math.Abs(position.Item1 - Position.Item1) == Math.Abs(position.Item2 - Position.Item2);
        }
    }
}

