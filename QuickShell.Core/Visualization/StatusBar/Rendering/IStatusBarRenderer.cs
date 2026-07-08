namespace QuickShell
{
    public interface IStatusBarRenderer
    {
        public StatusBarVisualizer? Source { get; set; }

        public void Invalidate();
    }
}