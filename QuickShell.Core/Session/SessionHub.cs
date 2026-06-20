using Zion;

namespace QuickShell
{
    public abstract class SessionHub
    {
        internal Session Session
        {
            get;
            set
            {
                field = value.NotNull();
                Terminal = value.Terminal;
                InputManager = value.Input;
            }
        }

        protected Terminal Terminal { get; private set; }

        protected InputManager InputManager { get; private set; }

        protected TerminalVisualizer TerminalVisualizer
        {
            get => Session.TerminalVisualizer;
            set => Session.TerminalVisualizer = value;
        }

        protected StatusBarVisualizer StatusBarVisualizer
        {
            get => Session.StatusBarVisualizer;
            set => Session.StatusBarVisualizer = value;
        }


        public abstract void Main(in ModuleArguments Arguments);

        public virtual bool CanClose() => true;

        public virtual void OnClosed() { }


        protected void Close()
        {
            //TODO
        }
    }
}