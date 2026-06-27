using Zion;

namespace QuickShell
{
    public abstract class TerminalVisualizer
    {
        public readonly EventBus Invalidated = new();

        public required Terminal Source
        {
            protected get; init => field = value.NotNull();
        }
        

        public abstract void Draw(TerminalVisualizingContext Context);
    }
}