using GalaSoft.MvvmLight;
using Tetris;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_WPF
{
    public class GameViewModel : ViewModelBase
    {
        private Game _game;

        public int Height { get => _game.Field.Height; }
        public int Width { get => _game.Field.Width; }
        public Cell[,] Cells { get => _game.Cells; }
        public TetrisPiece HeldPiece { get => HeldPiece; }
        public TetrisPiece[] NextPieces { get => NextPieces; }

        public GameViewModel()
        {
            _game = new Game();
        }

        public GameViewModel(Game game)
        {
            _game = game;
        }

        public void MoveLeft()
        {
            _game.MoveLeft();
            RaisePropertyChanged(nameof(_game.Cells));
        }
        public void MoveRight()
        {
            _game.MoveRight();
            RaisePropertyChanged(nameof(_game.Cells));
        }
        public void MoveDown()
        {
            _game.MoveDown();
            RaisePropertyChanged(nameof(_game.Cells));
        }
        public void HardDrop()
        {
            _game.HardDrop();
            RaisePropertyChanged(nameof(_game.Cells));
        }
        public void RotateClockwise()
        {
            _game.RotateClockwise();
            RaisePropertyChanged(nameof(_game.Cells));
        }
        public void RotateCounterclockwise()
        {
            _game.RotateCounterclockwise();
            RaisePropertyChanged(nameof(_game.Cells));
        }
        public void HoldPiece()
        {
            _game.HoldPiece();
            RaisePropertyChanged(nameof(HeldPiece));
        }
    }
}
