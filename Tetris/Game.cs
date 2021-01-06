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
        public TetrisPiece CurrentPiece { get; set; }
        public Position CurrentPosition { get; set; }
        public IPieceGenerator PieceGenerator { get; }
        public TetrisPiece[] NextPieces { get; }

        public Position CenterPosition { get; }
        public Cell[,] Cells { get => Field.Cells; }
        private bool _canHold = true;
        private int _numVisibilePieces = 3;

        public Game()
        {
            Field = new Field();
            PieceGenerator = new BPSGenerator();
            CenterPosition = new Position(Field.Width / 2, Field.Height - 4);
            CurrentPosition = new Position(CenterPosition.X, CenterPosition.Y);
            NextPieces = PieceGenerator.PeekNextPiece(_numVisibilePieces).ToArray();
            NextPiece();
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
            if (IsMoveValid(0, -1)) Move(0, -1);
            else SetPiece();
        }

        public void Move(int x, int y)
        {
            if (IsMoveValid(x, y))
            {
                UncolorCells();
                CurrentPosition.X += x;
                CurrentPosition.Y += y;
                ColorCells();
            }
        }

        public void HardDrop()
        {
            while (IsMoveValid(0, -1))
            {
                MoveDown();
            }
            SetPiece();
        }

        public void RotateClockwise()
        {
            if(IsRotationValid(Rotation.Clockwise))
            {
                UncolorCells();
                CurrentPiece.RotateClockwise();
                ColorCells();
            }
        }

        public void RotateCounterclockwise()
        {
            if (IsRotationValid(Rotation.Counterclockwise))
            {
                UncolorCells();
                CurrentPiece.RotateCounterclockwise();
                ColorCells();
            }
        }

        public void HoldPiece()
        {
            if(_canHold)
            {
                UncolorCells();
                if (HeldPiece == null)
                {
                    HeldPiece = CurrentPiece;
                    NextPiece();
                }
                else
                {
                    (CurrentPiece, HeldPiece) = (HeldPiece, CurrentPiece);
                }
                ResetCurrentPosition();
                ColorCells();
                _canHold = false;
            }
        }

        public void SetPiece()
        {
            FillCells();
            ClearAllLines();
            ResetCurrentPosition();
            NextPiece();
            _canHold = true;
        }

        public void NextPiece()
        {
            CurrentPiece = PieceGenerator.PopNextPiece();
            TetrisPiece[] pieces = PieceGenerator.PeekNextPiece(_numVisibilePieces).ToArray();
            for(int i = 0; i < _numVisibilePieces; i++)
            {
                NextPieces[i] = pieces[i];
            }
        }

        private void ColorCells()
        {
            foreach (Position p in CurrentPiece.RelativePositions)
            {
                int x = p.X + CurrentPosition.X;
                int y = p.Y + CurrentPosition.Y;
                Cells[x, y].Color = CurrentPiece.Color;
            }
        }

        private void UncolorCells()
        {
            foreach (Position p in CurrentPiece.RelativePositions)
            {
                int x = p.X + CurrentPosition.X;
                int y = p.Y + CurrentPosition.Y;
                Cells[x, y].Color = Color.White;
            }
        }

        private void FillCells()
        {
            foreach(Position p in CurrentPiece.RelativePositions)
            {
                int x = p.X + CurrentPosition.X;
                int y = p.Y + CurrentPosition.Y;
                Cells[x, y].Fill(CurrentPiece.Color);
            }
        }

        private void UnfillCells()
        {
            foreach (Position p in CurrentPiece.RelativePositions)
            {
                int x = p.X + CurrentPosition.X;
                int y = p.Y + CurrentPosition.Y;
                Cells[x, y].Unfill();
            }
        }

        private bool IsMoveValid(int xMove, int yMove)
        {
            bool isMoveValid = true;
            foreach (Position p in CurrentPiece.RelativePositions)
            {
                int newX = CurrentPosition.X + xMove + p.X;
                int newY = CurrentPosition.Y + yMove + p.Y;
                if (newX < 0 || newY < 0 || newX >= Field.Width || newY >= Field.Height || Cells[newX, newY].IsFilled)
                {
                    isMoveValid = false;
                    break;
                }
            }
            return isMoveValid;
        }

        private bool IsRotationValid(Rotation rotation)
        {
            bool isRotationValid = true;
            switch(rotation)
            {
                case Rotation.Clockwise:
                    CurrentPiece.RotateClockwise();
                    foreach(Position p in CurrentPiece.RelativePositions)
                    {
                        int newX = CurrentPosition.X + p.X;
                        int newY = CurrentPosition.Y + p.Y;
                        if (newX < 0 || newY < 0 || newX >= Field.Width || newY >= Field.Height || Cells[newX, newY].IsFilled)
                        {
                            isRotationValid = false;
                            break;
                        }
                    }
                    CurrentPiece.RotateCounterclockwise();
                    return isRotationValid;
                case Rotation.Counterclockwise:
                    CurrentPiece.RotateCounterclockwise();
                    foreach (Position p in CurrentPiece.RelativePositions)
                    {
                        int newX = CurrentPosition.X + p.X;
                        int newY = CurrentPosition.Y + p.Y;
                        if (newX < 0 || newY < 0 || newX >= Field.Width || newY >= Field.Height || Cells[newX, newY].IsFilled)
                        {
                            isRotationValid = false;
                            break;
                        }
                    }
                    CurrentPiece.RotateClockwise();
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

        private void ResetCurrentPosition()
        {
            CurrentPosition.X = CenterPosition.X;
            CurrentPosition.Y = CenterPosition.Y;
        }

        private Position GetGhostPiecePosition()
        {
            Position ghostPosition = new Position(CurrentPosition.X, CurrentPosition.Y);

            return ghostPosition;
        }
    }
}
