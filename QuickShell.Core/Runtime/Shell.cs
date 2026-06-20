using Zion;

namespace QuickShell.Runtime
{
    public sealed class Shell
    {
        private readonly Stack<Session> Sessions = new Stack<Session>();
        private readonly Host Host;
        public  readonly Module Module;

        public Shell(Host Host, Session Session)
        {
            this.Host   = Host.NotNull();
            this.Module = new Module()
            {
                CanClose = () => Sessions.All(static Session => Session.Hub.CanClose())
            };

            PushSession(Session);
        }


        public void PushSession(Session Session)
        {
            ArgumentNullException.ThrowIfNull(Session);

            Sessions.Push(Session);
            Host.CurrentSession = Session;
        }

        public void PopSession()
        {
            Sessions.Pop();

            if (Sessions.Count > 0)
            {
                Host.CurrentSession = Sessions.Peek();
            }
            else
            {
                Host.Close();
            }           
        }


        public void Close()
        {
            Host.Close();
        }
    }
}