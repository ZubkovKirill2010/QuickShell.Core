using QuickShell.Runtime;
using Zion;

namespace QuickShell
{
    public sealed class StatusBarRendererSynchronizer : IDisposable
    {
        private readonly Shell Shell;
        private readonly IStatusBarRenderer Recipient;

        public StatusBarRendererSynchronizer(Shell Shell, IStatusBarRenderer Recipient)
        {
            this.Shell = Shell.NotNull();
            this.Recipient = Recipient.NotNull();

            Shell.StatusBarVisualizerRouter.Invalidated += Recipient.Invalidate;
        }

        public void Dispose()
        {
            Shell.StatusBarVisualizerRouter.Invalidated -= Recipient.Invalidate;
        }
    }
}