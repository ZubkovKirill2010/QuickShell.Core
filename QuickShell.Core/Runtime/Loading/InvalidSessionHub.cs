using Zion;

namespace QuickShell
{
    public sealed class InvalidSessionHub : SessionHub, IInputHandler
    {
        private readonly string Message;

        public InvalidSessionHub(string Message)
        {
            this.Message = Message.NotNull();
        }

        public override void Main(in ModuleArguments Arguments)
        {
            //TODO
            //Terminal.WriteLine(Message);
            //PushInputHandler(this)
        }

        public void OnKeyDown(KeyboardHandler Handler)
        {
            Close();
        }
    }
}