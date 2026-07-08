using Zion;

namespace QuickShell.Runtime
{
    public sealed class Shell
    {
        private readonly Action Exit;
        private readonly List<Session> Sessions = new List<Session>();

        public  readonly GlobalInputHandler InputHandler;

        public readonly EventRouter TerminalVisualizerRouter;
        public readonly EventRouter StatusBarVisualizerRouter;

        public int SessionCount => Sessions.Count;

        public Session CurrentSession
        {
            get;
            set
            {
                value ??= Sessions[0];
                if (field != value)
                {
                    field = value;
                    TerminalVisualizerRouter.Sender = value.TerminalVisualizer.Invalidated;
                    StatusBarVisualizerRouter.Sender = value.StatusBarVisualizer?.Invalidated;
                }
            }
        }        

        public event Action? SessionsChanged;


        public Shell(Session Session, Action Close)
        {
            InputHandler = new GlobalInputHandler(this);

            TerminalVisualizerRouter  = new();
            StatusBarVisualizerRouter = new();

            Exit = Close.NotNull();

            AddSession(Session.NotNull());
        }


        public Session this[int Index]   => Sessions[Index];

        public Session this[Index Index] => Sessions[Index];


        public void AddSession(Session Session, bool SwitchFocus = false)
        {
            ArgumentNullException.ThrowIfNull(Session);

            Session.Shell = this;

            Sessions.Add(Session);
            SessionsChanged?.Invoke();

            if (SwitchFocus)
            {
                CurrentSession = Session;
            }
        }

        public bool RemoveSession(Session Session)
        {
            if (Sessions.Remove(Session))
            {
                Session.Shell = null;

                if (SessionCount == 0)
                {
                    Exit();
                }

                SessionsChanged?.Invoke();
                return true;
            }
            return false;
        }


        public void Close()
        {
            if (Sessions.All(static Session => Session.Hub.CanClose()))
            {
                Exit();
            }
        }

        public void CloseForced()
        {
            Exit();
        }
    }
}