using Zion;
using Zion.Vectors;

namespace QuickShell
{
    public delegate void DrawAction(int X, int Y, FormattedChar Char);

    public sealed class TerminalVisualizingContext
    {
        public readonly Vector2Int Start;
        public readonly Vector2Int Size;
        public readonly Vector2Int End;

        public readonly Action<int> MarkCollapsedLine;
        public readonly DrawAction Draw;

        public TerminalVisualizingContext(Vector2Int Start, Vector2Int Size, Action<int> MarkCollapsedLine, DrawAction Draw)
        {
            ArgumentOutOfRangeException.ThrowIf(Vector2Int.IsNegative(Start), $"Start(={Start}) is negative");
            ArgumentOutOfRangeException.ThrowIf(Vector2Int.IsNegative(Size), $"Size(={Size}) is negative");
            ArgumentNullException.ThrowIfNull(MarkCollapsedLine);
            ArgumentNullException.ThrowIfNull(Draw);

            this.Start = Start;
            this.Size = Size;
            this.End = Start + Size - Vector2Int.OneOne;

            this.MarkCollapsedLine = MarkCollapsedLine;
            this.Draw = Draw;
        }
    }
}