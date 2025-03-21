using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;

namespace ChessApp.Models
{
    public class Queen : ChessPiece
    {
        public Queen(string color, (int, int) position) : base(color, position) { }

        protected override bool CanMove((int, int) position)
        {
            int dx = Math.Abs(position.Item1 - Position.Item1);
            int dy = Math.Abs(position.Item2 - Position.Item2);

            return dx == dy || position.Item1 == Position.Item1 || position.Item2 == Position.Item2;
        }
    }
}


