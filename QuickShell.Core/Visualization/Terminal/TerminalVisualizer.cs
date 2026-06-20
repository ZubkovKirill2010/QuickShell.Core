using Zion;

namespace QuickShell
{
    public abstract class TerminalVisualizer
    {
        public required Terminal Source
        {
            protected get; init => field = value.NotNull();
        }

        public readonly EventBus Invalidated = new();

        public abstract void Draw(TerminalVisualizingContext Context);
    }
}