using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;

namespace ChessApp.Models
{
    public class Rook : ChessPiece
    {
        public Rook(string color, (int, int) position) : base(color, position) { }

        protected override bool CanMove((int, int) position)
        {
            return position.Item1 == Position.Item1 || position.Item2 == Position.Item2;
        }
    }
}
