using Zion;

namespace QuickShell
{
    public sealed class StatusBarBinding
    {
        public readonly Changeable<StatusBar> Visualizer;

        public event Action? Invalidated;


        public StatusBarBinding() : this(NullStatusBarVisualizer.Instance) { }

        public StatusBarBinding(StatusBar Visualizer)
        {
            this.Visualizer = new(Visualizer, CanChangeVisualizer);
            this.Visualizer.Changed += New => Invalidate();
        }


        internal void Invalidate()
        {
            Invalidated?.Invoke();
        }


        private bool CanChangeVisualizer(StatusBar Old, StatusBar New)
        {
            ArgumentNullException.ThrowIfNull(New);
            if (ReferenceEquals(Old, New))
            {
                return false;
            }

            Old?.Invalidated -= Invalidate;
            New.Invalidated += Invalidate;
            return true;
        }
    }
}