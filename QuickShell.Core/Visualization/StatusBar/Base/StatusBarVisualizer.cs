using Zion;

namespace QuickShell
{
    public abstract class StatusBarVisualizer
    {
        public readonly EventBus Invalidated = new();

        public virtual StatusBarLength TargetLength => StatusBarLength.Any;

        public abstract void Draw(StatusBarVisualizingContext Context);


        protected void Invalidate()
        {
            Invalidated.Invalidate();
        }
    }
}