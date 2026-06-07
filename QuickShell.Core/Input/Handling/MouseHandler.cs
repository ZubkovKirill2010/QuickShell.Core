namespace QuickShell
{
    public sealed class MouseHandler
    {
        public readonly MouseState State;
        public bool Handled;

        public MouseHandler(MouseState State)
        {
            this.State = State;
        }
    }
}