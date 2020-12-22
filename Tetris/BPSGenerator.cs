using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    /// <summary>
    /// The BPS Generator is the standard generator for pieces in Tetris.
    /// This generator guarantees that every piece is seen twice within the next 14 pieces (inclusive).
    /// In other words, the generator creates a batch of 7 pieces, scrambles them, and then adds them into the queue.
    /// </summary>
    public class BPSGenerator : IPieceGenerator
    {
        private Queue<TetrisPiece> Queue { get; }

        public BPSGenerator()
        {
            Queue = new Queue<TetrisPiece>();
        }

        public IEnumerable<TetrisPiece> PeekNextPiece(int n)
        {
            List<TetrisPiece> nextPieces = new List<TetrisPiece>() { };
            while(Queue.Count < n)
            {
                GenerateNextBatch();
            }
            for(int i = 0; i < n; i++)
            {
                nextPieces.Add(Queue.ElementAt(i));
            }

            return nextPieces;
        }

        public TetrisPiece PopNextPiece()
        {
            if (Queue.Count == 0) GenerateNextBatch();
            return Queue.Dequeue();
        }

        private void GenerateNextBatch()
        {
            List<int> ints = new List<int>() { };
            for(int i = 0; i < Enum.GetNames(typeof(Tetrimino)).Length; i++)
            {
                ints.Add(i);
            }

            Random random = new Random();
            while(ints.Count > 0)
            {
                int index = random.Next(0, ints.Count);
                int randomInt = ints[index];
                ints.RemoveAt(index);
                TetrisPiece piece = EnumToTetrisPiece((Tetrimino)randomInt);
                Queue.Enqueue(piece);
            }
        }

        private TetrisPiece EnumToTetrisPiece(Tetrimino tetrimino)
        {
            switch(tetrimino)
            {
                case Tetrimino.I:
                    return new IPiece();
                case Tetrimino.J:
                    return new JPiece();
                case Tetrimino.L:
                    return new LPiece();
                case Tetrimino.O:
                    return new OPiece();
                case Tetrimino.S:
                    return new SPiece();
                case Tetrimino.T:
                    return new TPiece();
                case Tetrimino.Z:
                    return new ZPiece();
                default:
                    throw new ArgumentOutOfRangeException("Could not determine the TetrisPiece corresponding to this Tetrimino to return.");
            }
        }

        private enum Tetrimino
        {
            I,
            J,
            L,
            S,
            Z,
            O,
            T
        }
    }
}
