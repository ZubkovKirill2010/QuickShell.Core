namespace QuickShell
{
    public abstract class StatusBarVisualizer
    {
        public event Action? Changed;

        public abstract void Draw(StatusBarVisualizingContext Context);
    }
}