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

        public Position CenterPosition { get; }
        public Cell[,] Cells { get => Field.Cells; }

        public Game()
        {
            Field = new Field();
            PieceGenerator = new BPSGenerator();
            CenterPosition = new Position(Field.Width / 2, Field.Height - 4);
            CurrentPosition = new Position(CenterPosition.X, CenterPosition.Y);
            NextPiece();
            FillCells();
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
            Move(0, -1);
        }

        public void Move(int x, int y)
        {
            if(IsMoveValid(x, y))
            {
                UnfillCells();
                CurrentPosition.X += x;
                CurrentPosition.Y += y;
                FillCells();
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
            throw new NotImplementedException();
        }

        public void RotateCounterclockwise()
        {
            throw new NotImplementedException();
        }

        public void HoldPiece()
        {
            HeldPiece = CurrentPiece;
            NextPiece();
        }

        public void SetPiece()
        {
            FillCells();
            ClearAllLines();
        }

        public void NextPiece()
        {
            CurrentPiece = PieceGenerator.PopNextPiece();
            FillCells();
        }

        private void FillCells()
        {
            foreach(Position p in CurrentPiece.Positions)
            {
                int x = p.X + CurrentPosition.X;
                int y = p.Y + CurrentPosition.Y;
                Cells[x, y].Fill(CurrentPiece.Color);
            }
        }

        private void UnfillCells()
        {
            foreach (Position p in CurrentPiece.Positions)
            {
                int x = p.X + CurrentPosition.X;
                int y = p.Y + CurrentPosition.Y;
                Cells[x, y].Unfill();
            }
        }

        private void LockCells()
        {
            foreach (Position p in CurrentPiece.Positions)
            {
                int x = p.X + CurrentPosition.X;
                int y = p.Y + CurrentPosition.Y;
                Cells[x, y].Lock();
            }
        }

        private bool IsMoveValid(int xMove, int yMove)
        {
            bool isMoveValid = true;
            foreach (Position p in CurrentPiece.Positions)
            {
                int newX = CurrentPosition.X + xMove + p.X;
                int newY = CurrentPosition.Y + yMove + p.Y;
                if (newX < 0 || newY < 0 || newX >= Field.Width || newY >= Field.Height || Cells[newX, newY].IsLocked)
                {
                    isMoveValid = false;
                    break;
                }
            }
            return isMoveValid;
        }

        private void ClearAllLines()
        {
            for (int row = 0; row < Cells.GetLength(0); row++)
            {
                if (IsRowFilled(row))
                {
                    ClearLine(row);
                }
            }
        }

        private void ClearLine(int row)
        {
            for (int col = 0; col < Cells.GetLength(1); col++)
            {
                Cells[row, col].Unfill();
                int i = col;
                while (i + 1 < Field.Height)
                {
                    if (i == Field.Height) break;

                    Cells[row, i] = Cells[row, i + 1];
                    i += 1;
                }
            }
        }

        private bool IsRowFilled(int row)
        {
            bool isFilled = true;
            for (int col = 0; col < Cells.GetLength(1); col++)
            {
                if (!Cells[row, col].IsFilled) isFilled = false;
            }
            return isFilled;
        }
    }
}
