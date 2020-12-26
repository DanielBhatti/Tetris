using System;
using System.Collections.Generic;

namespace Tetris
{
    /// <summary>
    /// An n x m grid where <see cref="TetrisPiece"/>s can spawn.
    /// This is meant to be a typical Tetris board, but has a bit of flexibility in terms of the playing field size.
    /// The field size represents the boundaries of the board, pieces can only be filled within the region specified.
    /// If a block is placed into the region (i.e. within the bounds), the grid should automatically attempt to find a portion of the region where it does fit.
    /// If a piece appears where a <see cref="Cell"/> has already been filled by a block, this is a game over.
    /// </summary>
    public class Grid : IGrid
    {
        public int Width { get; }
        public int Height { get; }
        public Cell[,] Cells { get; set; }
        public TetrisPiece HeldPiece { get; set; }
        public TetrisPiece CurrentPiece { get; set; }
        public List<Position> CurrentPositions { get; set; }
        public Position CenterPosition { get; }

        private IPieceGenerator PieceGenerator { get; }

        public Grid(int width = 10, int height = 24)
        {
            if (width < 4 || height < 4) throw new ArgumentOutOfRangeException("Width and height of the Grid must both be greater than 4.");

            Width = width;
            Height = height;
            Cells = new Cell[width, height];

            PieceGenerator = new BPSGenerator();
            CenterPosition = new Position(width / 2, height - 2);
            CurrentPositions = new List<Position>() { };
            NextPiece();
            foreach(Position p in CurrentPiece.Positions)
            {
                CurrentPositions.Add(new Position(p.X + CenterPosition.X, p.Y + CenterPosition.Y));
            }
            NextPiece();
        }

        public void MoveLeft()
        {
            bool isMoveValid = true;
            foreach(Position p in CurrentPositions)
            {
                if(p.X - 1 >= 0 || Cells[p.X - 1, p.Y].IsFilled)
                {
                    isMoveValid = false;
                }
            }

            if(isMoveValid)
            {
                foreach(Position p in CurrentPositions)
                {
                    p.X = p.X - 1;
                }
            }
        }

        public void MoveRight()
        {
            if (IsMoveValid(0, 1))
            {
                foreach (Position p in CurrentPositions)
                {
                    p.X = p.X + 1;
                }
            }
        }

        public void MoveDown()
        {
            if (IsMoveValid(0, -1))
            {
                foreach (Position p in CurrentPositions)
                {
                    p.Y = p.Y - 1;
                }
            }
        }

        public void HardDrop()
        {
            while(IsMoveValid(0, -1))
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
            foreach(Position p in CurrentPositions)
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
            foreach (Position p in CurrentPositions)
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
            CurrentPositions.Clear();
            foreach(Position p in CurrentPiece.Positions)
            {
                CurrentPositions.Add(new Position(p.X + CenterPosition.X, p.Y + CenterPosition.Y));
            }
        }

        private void ClearAllLines()
        {
            for(int row = 0; row < Cells.GetLength(0); row++)
            {
                if(IsRowFilled(row))
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
                while (i + 1 < Height)
                {
                    if (i == Height) break;

                    Cells[row, i] = Cells[row, i + 1];
                    i += 1;
                }
            }
        }

        private bool IsRowFilled(int row)
        {
            bool isFilled = true;
            for(int col = 0; col < Cells.GetLength(1); col++)
            {
                if (!Cells[row, col].IsFilled) isFilled = false;
            }
            return isFilled;
        }
    }
}
