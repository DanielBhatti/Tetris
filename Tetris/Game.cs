using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Game : IGame
    {
        public IField Field { get; }
        public TetrisPiece HeldPiece { get; set; }
        public TetrisPiece CurrentPiece { get { return CurrentBoardPiece.TetrisPiece; } set { CurrentBoardPiece.TetrisPiece = value; } }
        public IPieceGenerator PieceGenerator { get; }
        public TetrisPiece[] NextPieces { get; }

        public Position CenterPosition { get; }
        public Cell[,] Cells { get => Field.Cells; }
        private bool _canHold = true;
        private int _numVisibilePieces = 3;

        private BoardPiece _currentBoardPiece;
        private BoardPiece CurrentBoardPiece
        {
            get => _currentBoardPiece;
            set => _currentBoardPiece = value;
        }
        private BoardPiece _ghostBoardPiece;
        private BoardPiece GhostBoardPiece
        {
            get => _ghostBoardPiece;
            set => _ghostBoardPiece = value;
        }

        public Game()
        {
            Field = new Field();
            PieceGenerator = new BPSGenerator();
            CenterPosition = new Position(Field.Width / 2, Field.Height - 4);
            NextPieces = PieceGenerator.PeekNextPiece(_numVisibilePieces).ToArray();
            CurrentBoardPiece = new BoardPiece();
            NextPiece();
            SetGhostPiecePosition();
        }

        public void MoveLeft()
        {
            Move(-1, 0);
        }

        public void MoveRight()
        {
            Move(1, 0);
        }

        public void MoveDown()
        {
            if (IsMoveValid(ref _currentBoardPiece, 0, -1)) Move(0, -1);
            else SetPiece();
        }

        public void Move(int x, int y)
        {
            if (IsMoveValid(ref _currentBoardPiece, x, y))
            {
                UncolorCells(ref _currentBoardPiece);
                CurrentBoardPiece.Position.X += x;
                CurrentBoardPiece.Position.Y += y;
                ColorCells(ref _currentBoardPiece);

                UncolorCells(ref _ghostBoardPiece);
                SetGhostPiecePosition();
                ColorCells(ref _ghostBoardPiece);
            }
        }

        public void HardDrop()
        {
            while (IsMoveValid(ref _currentBoardPiece, 0, -1))
            {
                MoveDown();
            }
            SetPiece();
        }

        public void RotateClockwise()
        {
            if(IsRotationValid(ref _currentBoardPiece, Rotation.Clockwise))
            {
                UncolorCells(ref _currentBoardPiece);
                CurrentPiece.RotateClockwise();
                ColorCells(ref _currentBoardPiece);
            }
        }

        public void RotateCounterclockwise()
        {
            if (IsRotationValid(ref _currentBoardPiece, Rotation.Counterclockwise))
            {
                UncolorCells(ref _currentBoardPiece);
                CurrentPiece.RotateCounterclockwise();
                ColorCells(ref _currentBoardPiece);
            }
        }

        public void HoldPiece()
        {
            if(_canHold)
            {
                UncolorCells(ref _currentBoardPiece);
                if (HeldPiece == null)
                {
                    HeldPiece = CurrentPiece;
                    NextPiece();
                }
                else
                {
                    (CurrentPiece, HeldPiece) = (HeldPiece, CurrentPiece);
                }
                ResetCurrentPosition(ref _currentBoardPiece);
                ColorCells(ref _currentBoardPiece);
                _canHold = false;
            }
        }

        public void SetPiece()
        {
            FillCells(ref _currentBoardPiece);
            ClearAllLines();
            ResetCurrentPosition(ref _currentBoardPiece);
            NextPiece();
            _canHold = true;
        }

        public void NextPiece()
        {
            CurrentBoardPiece.TetrisPiece = PieceGenerator.PopNextPiece();
            CurrentBoardPiece.Position = new Position(CenterPosition.X, CenterPosition.Y);
            TetrisPiece[] pieces = PieceGenerator.PeekNextPiece(_numVisibilePieces).ToArray();
            for(int i = 0; i < _numVisibilePieces; i++)
            {
                NextPieces[i] = pieces[i];
            }
        }

        private void ColorCells(ref BoardPiece boardPiece)
        {
            foreach (Position p in boardPiece.TetrisPiece.RelativePositions)
            {
                int x = p.X + boardPiece.Position.X;
                int y = p.Y + boardPiece.Position.Y;
                Cells[x, y].Color = boardPiece.TetrisPiece.Color;
            }
        }

        private void UncolorCells(ref BoardPiece boardPiece)
        {
            foreach (Position p in boardPiece.TetrisPiece.RelativePositions)
            {
                int x = p.X + boardPiece.Position.X;
                int y = p.Y + boardPiece.Position.Y;
                Cells[x, y].Color = Color.White;
            }
        }

        private void FillCells(ref BoardPiece boardPiece)
        {
            foreach(Position p in boardPiece.TetrisPiece.RelativePositions)
            {
                int x = p.X + boardPiece.Position.X;
                int y = p.Y + boardPiece.Position.Y;
                Cells[x, y].Fill(boardPiece.TetrisPiece.Color);
            }
        }

        private void UnfillCells(ref BoardPiece boardPiece)
        {
            foreach (Position p in boardPiece.TetrisPiece.RelativePositions)
            {
                int x = p.X + boardPiece.Position.X;
                int y = p.Y + boardPiece.Position.Y;
                Cells[x, y].Unfill();
            }
        }

        private bool IsMoveValid(ref BoardPiece boardPiece, int xMove, int yMove)
        {
            bool isMoveValid = true;
            foreach (Position p in boardPiece.TetrisPiece.RelativePositions)
            {
                int newX = boardPiece.Position.X + xMove + p.X;
                int newY = boardPiece.Position.Y + yMove + p.Y;
                if (newX < 0 || newY < 0 || newX >= Field.Width || newY >= Field.Height || Cells[newX, newY].IsFilled)
                {
                    isMoveValid = false;
                    break;
                }
            }
            return isMoveValid;
        }

        private bool IsRotationValid(ref BoardPiece boardPiece, Rotation rotation)
        {
            bool isRotationValid = true;
            switch(rotation)
            {
                case Rotation.Clockwise:
                    boardPiece.TetrisPiece.RotateClockwise();
                    foreach(Position p in boardPiece.TetrisPiece.RelativePositions)
                    {
                        int newX = boardPiece.Position.X + p.X;
                        int newY = boardPiece.Position.Y + p.Y;
                        if (newX < 0 || newY < 0 || newX >= Field.Width || newY >= Field.Height || Cells[newX, newY].IsFilled)
                        {
                            isRotationValid = false;
                            break;
                        }
                    }
                    boardPiece.TetrisPiece.RotateCounterclockwise();
                    return isRotationValid;
                case Rotation.Counterclockwise:
                    boardPiece.TetrisPiece.RotateCounterclockwise();
                    foreach (Position p in boardPiece.TetrisPiece.RelativePositions)
                    {
                        int newX = boardPiece.Position.X + p.X;
                        int newY = boardPiece.Position.Y + p.Y;
                        if (newX < 0 || newY < 0 || newX >= Field.Width || newY >= Field.Height || Cells[newX, newY].IsFilled)
                        {
                            isRotationValid = false;
                            break;
                        }
                    }
                    boardPiece.TetrisPiece.RotateClockwise();
                    return isRotationValid;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ClearAllLines()
        {
            for (int yRow = Cells.GetLength(1) - 1; yRow >= 0; yRow--)
            {
                if (IsRowFilled(yRow))
                {
                    ClearLine(yRow);
                }
            }
        }

        private void ClearLine(int yRow)
        {
            for (int xCol = 0; xCol < Cells.GetLength(0); xCol++)
            {
                Cells[xCol, yRow].Unfill();
                int i = yRow;
                while (i < Field.Height - 2)
                {
                    if (Cells[xCol, i + 1].IsFilled) Cells[xCol, i].Fill(Cells[xCol, i + 1].Color);
                    else Cells[xCol, i].Unfill();
                    i += 1;
                }
            }
        }

        private bool IsRowFilled(int yRow)
        {
            for (int xCol = 0; xCol < Cells.GetLength(0); xCol++)
            {
                if (!Cells[xCol, yRow].IsFilled) return false;
            }
            return true;
        }

        private void ResetCurrentPosition(ref BoardPiece boardPiece)
        {
            boardPiece.Position.X = CenterPosition.X;
            boardPiece.Position.Y = CenterPosition.Y;
        }

        private BoardPiece SetGhostPiecePosition()
        {
            GhostBoardPiece.TetrisPiece = (TetrisPiece)CurrentPiece.GetType().GetConstructor(new Type[] { }).Invoke(new object[] { });
            GhostBoardPiece.Position = new Position(CurrentBoardPiece.Position.X, CurrentBoardPiece.Position.Y);
            while(IsMoveValid(ref _ghostBoardPiece, 0, -1))
            {
                GhostBoardPiece.Position.Y -= 1;
            }

            return _ghostBoardPiece;
        }
    }
}
