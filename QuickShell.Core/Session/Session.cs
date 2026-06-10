using Zion;

namespace QuickShell
{
    public sealed class Session
    {
        public readonly ModuleHub    Hub;
        public readonly Terminal     Terminal;
        public readonly InputManager Input;

        public StatusBarVisualizer? StatusBarVisualizer;
        public TerminalVisualizer Visualizer
        {
            get; set => field = value.NotNull();
        }

        public Session(ModuleHub Hub)
        {
            this.Hub = Hub.NotNull();

            Terminal = new Terminal();
            Input = new InputManager();
            Visualizer = new BaseTerminalVisualizer() { Source = Terminal };
        }


        public void Run(in ModuleArguments Arguments)
        {
            Hub.Main(in Arguments);
        }
    }
}