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

        #region Changing
        private readonly List<ChangeTracker> ChangeTrackers;

        #endregion

        #region Events
        public event Action? Changed;
        public event Action? Clearing;

        #endregion

        #region Constructors
        public Buffer()
        {
            Lines = new(-1, 500);
            ChangeTrackers = new List<ChangeTracker>();
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
                Change(in X, in Y);
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

            ChangeTrackers.ForEach(Tracker => Tracker.OnChanged());

            Clearing?.Invoke();
        }

        public void Draw(VisualizingContext Context)
        {
            Vector2Int Start = Context.Start;
            Vector2Int End = Context.End;

            Vector2Int StartChunk = Start >> Chunk.BinarySize;
            Vector2Int EndChunk = End >> Chunk.BinarySize;

            Vector2Int LocalStart = Start & Chunk.Filter;
            Vector2Int LocalEnd = (End & Chunk.Filter) + Vector2Int.OneOne;

            bool OnlyOneLine = StartChunk.Y == EndChunk.Y;
            bool OnlyOneColumn = StartChunk.X == EndChunk.X;

            void DrawLine(Line Line, in int LineY, in int MinLocalY, in int MaxLocalY)
            {
                if (OnlyOneColumn)
                {
                    if (Line.TryGetChunk(StartChunk.X, out Chunk OnlyChunk))
                    {
                        DrawChunk
                        (
                            OnlyChunk,
                            StartChunk.X, LineY,
                            LocalStart.X, MinLocalY,
                            LocalEnd.X, MaxLocalY
                        );
                    }
                    return;
                }

                //FirstChunk
                if (Line.TryGetChunk(StartChunk.X, out Chunk FirstChunk))
                {
                    DrawChunk
                    (
                        FirstChunk,
                        in StartChunk.X, in LineY,
                        in LocalStart.X, in MinLocalY,
                        Chunk.Size, in MaxLocalY
                    );
                }

                //MiddleChunk
                for (int i = StartChunk.X + 1; i < EndChunk.X; i++)
                {
                    if (Line.TryGetChunk(in i, out Chunk Chunk))
                    {
                        DrawChunk
                        (
                            Chunk, in i, in LineY,
                            0, 0,
                            Chunk.Size, Chunk.Size
                        );
                    }
                }

                //LastChunk
                if (Line.TryGetChunk(EndChunk.X, out Chunk LastChunk))
                {
                    DrawChunk
                    (
                        LastChunk,
                        in EndChunk.X, in LineY,
                        0, in MinLocalY,
                        in LocalEnd.X, in MaxLocalY
                    );
                }
            }

            void DrawChunk(Chunk Chunk,
                in int ChunkX, in int ChunkY,
                in int MinX, in int MinY,
                in int MaxX, in int MaxY)
            {
                int StartX = ChunkX << Chunk.BinarySize;
                int StartY = ChunkY << Chunk.BinarySize;

                for (int x = MinX; x < MaxX; x++)
                {
                    for (int y = MinY; y < MaxY; y++)
                    {
                        Context.Draw(StartX + x - Start.X, Start.Y + y - Start.Y, Chunk[x, y]);
                    }
                }
            }

            if (OnlyOneLine)
            {
                if (TryGetLine(StartChunk.Y, out Line OnlyLine))
                {
                    DrawLine(OnlyLine, StartChunk.Y, LocalStart.Y, LocalEnd.Y);
                }
                return;
            }


            //FirstLine
            if (TryGetLine(StartChunk.Y, out Line FirstLine))
            {
                DrawLine(FirstLine, in StartChunk.Y, in LocalStart.Y, Chunk.Size);
            }

            //MiddleLines
            for (int i = StartChunk.Y + 1; i < EndChunk.Y; i++)
            {
                if (TryGetLine(i, out Line Line))
                {
                    DrawLine(Line, in i, 0, Chunk.Size);
                }
            }

            //LastLine
            if (TryGetLine(EndChunk.Y, out Line LastLine))
            {
                DrawLine(LastLine, in EndChunk.Y, 0, in LocalEnd.Y);
            }
        }


        public void AddChangeTracker(ChangeTracker ChangeTracker)
        {
            ArgumentNullException.ThrowIfNull(ChangeTracker);

            ChangeTrackers.Add(ChangeTracker);
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

        private bool TryGetLine(in int Y, out Line Line)
        {
            return Lines.TryGetValue(Y, out Line);
        }


        private void Change(in int X, in int Y)
        {
            foreach (ChangeTracker Tracker in ChangeTrackers)
            {
                Tracker.Change(in X, in Y);
            }
        }

        #endregion
    }
}