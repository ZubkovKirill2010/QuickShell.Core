using System.Collections.Concurrent;
using Zion;
using Zion.Vectors;
using Char = QuickShell.FormattedChar;

namespace QuickShell
{
    public sealed class Buffer
    {
        #region Data
        private readonly ConcurrentDictionary<int, Line> Lines;
        #endregion

        #region Events

        public event Action? Changed;
        public event Action? Clearing;
        #endregion

        #region Constructors
        public Buffer()
        {
            Lines = new(-1, 500);
        }

        #endregion

        #region Indexers
        public Char this[in int X, in int Y]
        {
            get
            {
                if (TryGetChunk(X >> Chunk.BinarySize, Y >> Chunk.BinarySize, out Chunk TargetChunk))
                {
                    return TargetChunk[X & Chunk.Filter, Y & Chunk.Filter];
                }

                return Char.Default;
            }
            set
            {
                Chunk Chunk = GetChunk(X >> Chunk.BinarySize, Y >> Chunk.BinarySize);
                Chunk[X & Chunk.Filter, Y & Chunk.Filter] = value;
            }
        }

        public Char this[in Vector2Int Position]
        {
            get
            {
                return this[Position.X, Position.Y];
            }
            set
            {
                this[Position.X, Position.Y] = value;
            }
        }

        #endregion

        #region Methods
        public void Clear()
        {
            Lines.Values.ForEach(Line.Return);
            Lines.Clear();

            Clearing?.Invoke();
        }

        #endregion

        #region PrivateMethods
        private Chunk GetChunk(in int X, in int Y)
        {
            return GetLine(Y).GetChunk(X);
        }

        private bool TryGetChunk(in int X, in int Y, out Chunk Result)
        {
            Result = default!;
            return Lines.TryGetValue(Y, out Line Line)
                && Line.TryGetChunk(X, out Result);
        }


        private Line GetLine(in int Y)
        {
            if (Lines.TryGetValue(Y, out Line Line))
            {
                return Line;
            }

            Line = Line.New();
            Lines.TryAdd(Y, Line);
            return Line;
        }

        #endregion
    }
}