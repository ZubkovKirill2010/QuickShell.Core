using Zion;
using Zion.Vectors;

namespace QuickShell
{
    public sealed class TerminalBinding
    {
        public readonly Changeable<TerminalVisualizer> Visualizer;

        public event Action? Invalidated;

        public TerminalBinding() : this(new BaseTerminalVisualizer()) { }

        public TerminalBinding(TerminalVisualizer Visualizer)
        {
            this.Visualizer = new(Visualizer, CanChangeVisualizer);
            this.Visualizer.Changed += New => Invalidate();
        }


        internal void Change(in int X, in int Y)
        {
            //TODO
        }

        internal void Change(Vector2IntRange Range)
        {
            //TODO
        }

        internal void Invalidate()
        {
            Invalidated?.Invoke();
        }


        private bool CanChangeVisualizer(TerminalVisualizer Old, TerminalVisualizer New)
        {
            ArgumentNullException.ThrowIfNull(New);
            Old?.Invalidated -= Invalidate;
            New. Invalidated += Invalidate;
            return true;
        }
    }
}