namespace QuickShell
{
    public abstract class TerminalVisualizer
    {
        public event Action? Invalidated;

        public abstract void Draw(Terminal Terminal, TerminalVisualizingContext Context);
    }
}