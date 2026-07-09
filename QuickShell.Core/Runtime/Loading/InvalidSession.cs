using Zion;

namespace QuickShell
{
    public sealed class InvalidSession : Session, IInputHandler
    {
        private readonly string Message;

        public InvalidSession(string Message)
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