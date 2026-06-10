using Zion;

namespace QuickShell
{
    public abstract class TerminalVisualizer
    {
        public required Terminal Source
        {
            protected get; init => field = value.NotNull();
        }

        public event Action? Changed;

        public abstract void Draw(TerminalVisualizingContext Context);
    }
}