using QuickShell.Runtime;
using Zion;

namespace QuickShell
{
    public sealed class Session
    {
        internal Shell? Shell;

        public readonly SessionHub   Hub;
        public readonly Terminal     Terminal;
        public readonly InputManager Input;

        public string Title
        {
            get;
            set
            {
                value ??= "Title";
                field = value;
                TitleChanged?.Invoke(value);
            }
        } = "Title";

        public TerminalVisualizer TerminalVisualizer
        {
            get;
            set
            {
                value ??= new BaseTerminalVisualizer() { Source = Terminal };
                if (!ReferenceEquals(field, value))
                {
                    field = value;
                    Shell?.TerminalVisualizerRouter.Sender = value.Invalidated;
                }
            }
        }
        public StatusBarVisualizer? StatusBarVisualizer
        {
            get;
            set
            {
                if (!ReferenceEquals(field, value))
                {
                    field = value;
                    Shell?.StatusBarVisualizerRouter.Sender = value?.Invalidated;
                }
            }
        }

        public event Action<string>? TitleChanged;


        public Session(SessionHub Hub)
        {
            this.Hub = Hub.NotNull();

            Terminal = new Terminal();
            Input = new InputManager();

            TerminalVisualizer = new BaseTerminalVisualizer() { Source = Terminal };

            Hub.Session = this;
        }


        public void Run(in ModuleArguments Arguments)
        {
            Hub.Main(in Arguments);
        }
    }
}