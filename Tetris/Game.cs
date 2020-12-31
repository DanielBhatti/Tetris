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
        public IPieceGenerator PieceGenerator { get; }

        public List<Position> FilledPositions { get; set; }
        public Position CenterPosition { get; }
        public Cell[,] Cells { get => Field.Cells; }

        public Game()
        {
            PieceGenerator = new BPSGenerator();
            CenterPosition = new Position(Field.Width / 2, Field.Height - 2);
            FilledPositions = new List<Position>() { };
            NextPiece();
            foreach (Position p in CurrentPiece.Positions)
            {
                FilledPositions.Add(new Position(p.X + CenterPosition.X, p.Y + CenterPosition.Y));
            }
            NextPiece();
        }

        public void MoveLeft()
        {
            bool isMoveValid = true;
            foreach (Position p in FilledPositions)
            {
                if (p.X - 1 >= 0 || Cells[p.X - 1, p.Y].IsFilled)
                {
                    isMoveValid = false;
                }
            }

            if (isMoveValid)
            {
                foreach (Position p in FilledPositions)
                {
                    p.X = p.X - 1;
                }
            }
        }

        public void MoveRight()
        {
            if (IsMoveValid(0, 1))
            {
                foreach (Position p in FilledPositions)
                {
                    p.X = p.X + 1;
                }
            }
        }

        public void MoveDown()
        {
            if (IsMoveValid(0, -1))
            {
                foreach (Position p in FilledPositions)
                {
                    p.Y = p.Y - 1;
                }
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
            ResetPositions();
        }

        public void SetPiece()
        {
            foreach (Position p in FilledPositions)
            {
                Cells[p.X, p.Y] = new Cell(CurrentPiece.Color);
            }

            ClearAllLines();
        }

        public void NextPiece()
        {
            CurrentPiece = PieceGenerator.PopNextPiece();
        }

        private bool IsMoveValid(int xMove, int yMove)
        {
            bool isMoveValid = true;
            foreach (Position p in FilledPositions)
            {
                if (p.Y - 1 >= 0 || Cells[p.X, p.Y - 1].IsFilled)
                {
                    isMoveValid = false;
                }
            }
            return isMoveValid;
        }

        private void ResetPositions()
        {
            FilledPositions.Clear();
            foreach (Position p in CurrentPiece.Positions)
            {
                FilledPositions.Add(new Position(p.X + CenterPosition.X, p.Y + CenterPosition.Y));
            }
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
                Cells[row, col].Reset();
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
