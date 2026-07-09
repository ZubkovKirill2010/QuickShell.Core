using Zion;

namespace QuickShell
{
    public abstract class StatusBar
    {
        public virtual StatusBarLength TargetLength => StatusBarLength.Any;

        public event Action? Invalidated;

        public abstract void Draw(StatusBarVisualizingContext Context);
    }
}