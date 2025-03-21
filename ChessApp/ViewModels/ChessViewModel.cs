using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ReactiveUI;
using System.Reactive;
using ChessApp.Models;

namespace ChessApp.ViewModels
{
    public class ChessViewModel : ViewModelBase
    {
        private ChessPiece? _selectedPiece;
        private string _selectedPieceType = "Ферзь";
        private string _selectedColor = "Белый";
        private string _position = "A1";
        private string _moveTo = "A2";
        private string _status = "Выберите фигуру и сделайте ход.";

        private readonly HashSet<(int, int)> _occupiedPositions = new();

        public ObservableCollection<string> PieceTypes { get; } = new() { "Ферзь", "Ладья", "Слон" };
        public ObservableCollection<string> Colors { get; } = new() { "Белый", "Чёрный" };

        public string SelectedPieceType
        {
            get => _selectedPieceType;
            set => this.RaiseAndSetIfChanged(ref _selectedPieceType, value);
        }

        public string SelectedColor
        {
            get => _selectedColor;
            set => this.RaiseAndSetIfChanged(ref _selectedColor, value);
        }

        public string Position
        {
            get => _position;
            set => this.RaiseAndSetIfChanged(ref _position, value);
        }

        public string MoveTo
        {
            get => _moveTo;
            set => this.RaiseAndSetIfChanged(ref _moveTo, value);
        }

        public string Status
        {
            get => _status;
            set => this.RaiseAndSetIfChanged(ref _status, value);
        }

        public ReactiveCommand<Unit, Unit> CreatePiece { get; }
        public ReactiveCommand<Unit, Unit> MakeMove { get; }

        public ChessViewModel()
        {
            CreatePiece = ReactiveCommand.Create(CreatePieceAction);
            MakeMove = ReactiveCommand.Create(MakeMoveAction);
        }

        private void CreatePieceAction()
        {
            if (!TryParsePosition(Position, out (int x, int y) startPos))
            {
                Status = "Ошибка! Некорректный формат позиции.";
                return;
            }

            if (_occupiedPositions.Contains(startPos))
            {
                Status = "Ошибка! Позиция уже занята другой фигурой.";
                return;
            }

            _selectedPiece = SelectedPieceType switch
            {
                "Ферзь" => new Queen(SelectedColor, startPos),
                "Ладья" => new Rook(SelectedColor, startPos),
                "Слон" => new Bishop(SelectedColor, startPos),
                _ => null
            };

            if (_selectedPiece != null)
            {
                _occupiedPositions.Add(startPos);
                Status = $"Создана {SelectedPieceType} ({SelectedColor}) на {Position}";
            }
            else
            {
                Status = "Ошибка создания фигуры!";
            }
        }

        private void MakeMoveAction()
        {
            if (_selectedPiece == null)
            {
                Status = "Сначала создайте фигуру!";
                return;
            }

            if (!TryParsePosition(MoveTo, out (int x, int y) newPosition))
            {
                Status = "Ошибка! Некорректный формат позиции.";
                return;
            }

            if (_occupiedPositions.Contains(newPosition))
            {
                Status = "Ошибка! Позиция уже занята другой фигурой.";
                return;
            }

            if (_selectedPiece.Move(newPosition))
            {
                _occupiedPositions.Remove(_selectedPiece.Position);
                _selectedPiece.Position = newPosition;
                _occupiedPositions.Add(newPosition);

                Position = MoveTo;
                Status = $"Ход выполнен: {SelectedPieceType} на {MoveTo}";
            }
            else
            {
                Status = "Невозможный ход!";
            }
        }

        private bool TryParsePosition(string pos, out (int, int) position)
        {
            position = (0, 0);
            if (pos.Length != 2) return false;

            char letter = pos[0];
            char number = pos[1];

            if (letter < 'A' || letter > 'H' || number < '1' || number > '8')
                return false;

            position = (letter - 'A', number - '1');
            return true;
        }
    }
}
