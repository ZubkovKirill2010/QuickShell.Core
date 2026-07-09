using System.Collections;
using Zion;

namespace QuickShell.Runtime
{
    public sealed class Shell : IEnumerable<Session>
    {
        #region Data
        #region Reveived
        private readonly Action Exit;

        #endregion

        #region Input
        public readonly GlobalInputHandler InputHandler;

        #endregion

        #region Sessions
        private readonly List<Session> Sessions = new();

        public int SessionCount => Sessions.Count;

        public Session CurrentSession
        {
            get;
            private set
            {
                ArgumentNullException.ThrowIfNull(value);
                if (ReferenceEquals(field, value)) { return; }

                if (field is not null)
                {
                    var OldTerminal = field.Terminal.Binding;
                    OldTerminal.Visualizer.Changed -= UpdateVisualizer;
                    OldTerminal.Invalidated -= TerminalBinding.Invalidate;

                    var OldStatusBar = field.StatusBarBinding;
                    OldStatusBar.Visualizer.Changed -= UpdateStatusBar;
                    OldStatusBar.Invalidated -= StatusBarBinding.Invalidate;
                }

                var NewTerminal = value.Terminal.Binding;
                NewTerminal.Visualizer.Changed += UpdateVisualizer;
                NewTerminal.Invalidated += TerminalBinding.Invalidate;

                var NewStatusBar = value.StatusBarBinding;
                NewStatusBar.Visualizer.Changed += UpdateStatusBar;
                NewStatusBar.Invalidated += StatusBarBinding.Invalidate;

                field = value;

                CurrentSessionChanged?.Invoke();
            }
        }

        public event Action? CurrentSessionChanged;
        public event Action? SessionsChanged;

        #endregion

        #region Bindings
        public readonly TerminalBinding TerminalBinding   = new();
        public readonly StatusBarBinding StatusBarBinding = new();

        #endregion

        #endregion

        #region Constructor
        public Shell(Session Session, Action Close)
        {
            InputHandler = new(this);

            Exit = Close.NotNull();
            Add(Session.NotNull());

            CurrentSession = Session;
        }

        #endregion

        #region Indexers
        public Session this[int Index] => Sessions[Index];

        public Session this[Index Index] => Sessions[Index];

        #endregion

        #region PublicMethods
        public void Add(Session Session, bool SwitchFocus = false)
        {
            ArgumentNullException.ThrowIfNull(Session);

            Sessions.Add(Session);
            SessionsChanged?.Invoke();

            if (SwitchFocus)
            {
                CurrentSession = Session;
            }
        }

        public bool Remove(Session Session, bool Forced = false)
        {
            int Index = Sessions.IndexOf(Session);

            if (Index == -1) { return false; }
            
            if (Forced || Session.CanClose())
            {
                Session.CloseForced();

                if (Sessions.Count == 1)
                {
                    Exit();
                    return true;
                }

                Sessions.RemoveAt(Index);
                SessionsChanged?.Invoke();

                if (ReferenceEquals(Session, CurrentSession))
                {
                    CurrentSession = Index == 0 ? Sessions[0] : Sessions[^1];
                }
            }

            return true;
        }

        public bool Contains(Session Session)
        {
            return Sessions.Contains(Session);
        }


        public void Close()
        {
            if (Sessions.All(static Session => Session.CanClose()))
            {
                Exit();
            }
        }

        public void CloseForced()
        {
            Exit();
        }

        #endregion

        #region IEnumerable
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<Session> GetEnumerator()
        {
            return Sessions.GetEnumerator();
        }

        #endregion

        #region PrivateMethods
        private void UpdateVisualizer(TerminalVisualizer NewVisualizer)
        {
            TerminalBinding.Visualizer.Value = NewVisualizer;
        }

        private void UpdateStatusBar(StatusBar NewVisualizer)
        {
            StatusBarBinding.Visualizer.Value = NewVisualizer;
        }

        #endregion
    }
}