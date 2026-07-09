namespace QuickShell
{
    public sealed class NullStatusBarVisualizer : StatusBar
    {
        public static readonly NullStatusBarVisualizer Instance = new NullStatusBarVisualizer();

        public override void Draw(StatusBarVisualizingContext Context)
        {

        }
    }
}