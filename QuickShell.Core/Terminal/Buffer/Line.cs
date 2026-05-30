using System.Collections.Concurrent;
using Zion;

namespace QuickShell
{
    internal sealed class Line
    {
        private static readonly ConcurrentObjectPool<Line> LinePool = new(() => new Line());

        private readonly ConcurrentDictionary<int, Chunk> Chunks = new();

        public int Length { get; private set; }

        
        public static Line New()
        {
            return LinePool.Get();
        }

        public static void Return(Line Line)
        {
            Line.Chunks.Values.ForEach(Chunk.Return);
            Line.Clear();

            LinePool.Return(Line);
        }


        public bool TryGetChunk(in int X, out Chunk Chunk)
        {
            return Chunks.TryGetValue(X, out Chunk);
        }

        public Chunk GetChunk(in int X)
        {
            if (Chunks.TryGetValue(X, out Chunk Chunk))
            {
                return Chunk;
            }

            Chunk = Chunk.New();
            Chunks.TryAdd(X, Chunk);

            return Chunk;
        }


        public void AddChunk(in int X)
        {
            Chunks.TryAdd(X, Chunk.New());
            Length = Math.Max(X, Length);
        }


        public void Clear()
        {
            Chunks.Clear();
            Length = 0;
        }
    }
}