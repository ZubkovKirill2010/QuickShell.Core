using Zion.Vectors;

namespace QuickShell
{
    public delegate void DrawAction(in int X, in int Y, in FormattedChar Char);

    public sealed class TerminalVisualizingContext
    {
        public readonly Vector2IntRange Size;

        public readonly DrawAction Draw;

        public TerminalVisualizingContext(Vector2IntRange Size, DrawAction Draw)
        {
            ArgumentNullException.ThrowIfNull(Draw);

            this.Size = Size;
            this.Draw = Draw;
        }
    }
}