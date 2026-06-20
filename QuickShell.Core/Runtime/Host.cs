using Zion;

namespace QuickShell.Runtime
{
    public sealed class Host
    {
        public Session CurrentSession
        {
            get;
            internal set
            {
                field = value;
                TerminalVisualizerRouter.Sender  = value?.TerminalVisualizer ?.Invalidated;
                StatusBarVisualizerRouter.Sender = value?.StatusBarVisualizer?.Invalidated;
            }
        }

        public required Action Close { get; init => field = value.NotNull(); }

        public readonly EventRouter TerminalVisualizerRouter = new();
        public readonly EventRouter StatusBarVisualizerRouter = new();
    }
}