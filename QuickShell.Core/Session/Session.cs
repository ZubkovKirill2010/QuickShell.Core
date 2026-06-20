using Zion;

namespace QuickShell
{
    public sealed class Session
    {
        public readonly SessionHub   Hub;
        public readonly Terminal     Terminal;
        public readonly InputManager Input;

        public TerminalVisualizer   TerminalVisualizer;
        public StatusBarVisualizer? StatusBarVisualizer;

        public Session(SessionHub Hub)
        {
            this.Hub = Hub.NotNull();

            Terminal = new Terminal();
            Input = new InputManager();

            TerminalVisualizer  = new BaseTerminalVisualizer() { Source = Terminal };

            Hub.Session = this;
        }


        public void Run(in ModuleArguments Arguments)
        {
            Hub.Main(in Arguments);
        }
    }
}