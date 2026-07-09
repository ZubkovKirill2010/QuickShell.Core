using Zion;
using Zion.Vectors;

namespace QuickShell
{
    internal sealed class Chunk
    {
        private static readonly ConcurrentObjectPool<Chunk> ChunkPool = new()
        {
            New = static () => new Chunk(),
            Getter = static Chunk => { Chunk.Clear(); return Chunk; }
        };

        public const int BinarySize = 4;
        public const int Size = 1 << BinarySize;
        public const int Filter = Size - 1;

        private readonly object Lock = new();
        private readonly Matrix<FormattedChar> Matrix;

        public Chunk()
        {
            Matrix = new Matrix<FormattedChar>(Size);
        }


        public FormattedChar this[in int X, in int Y]
        {
            get
            {
                lock (Lock)
                {
                    return Matrix[X, Y];
                }
            }
            set
            {
                lock (Lock)
                {
                    Matrix[X, Y] = value;
                }
            }
        }

        public FormattedChar this[in Vector2Int Position]
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


        public static Chunk New()
        {
            return ChunkPool.Get();
        }

        public static void Return(Chunk Chunk)
        {
            Chunk.Clear();
            ChunkPool.Add(Chunk);
        }


        public void Fill(in FormattedChar Char)
        {
            lock (Lock)
            {
                foreach (int X in ZEnumerable.Range(Size))
                {
                    foreach (int Y in ZEnumerable.Range(Size))
                    {
                        Matrix[X, Y] = Char;
                    }
                }
            }
        }

        public void Fill(Func<FormattedChar> CharGetter)
        {
            lock (Lock)
            {
                foreach (int X in ZEnumerable.Range(Size))
                {
                    foreach (int Y in ZEnumerable.Range(Size))
                    {
                        Matrix[X, Y] = CharGetter();
                    }
                }
            }
        }

        public bool All(Func<FormattedChar, bool> Condition)
        {
            lock (Lock)
            {
                return Matrix.All(Condition);
            }
        }


        public void Clear()
        {
            Fill(in FormattedChar.Default);
        }


        public static bool IsInside(in int X, in int Y)
        {
            return int.IsPositive(X) && int.IsPositive(Y) && X < Size && Y < Size;
        }
    }
}