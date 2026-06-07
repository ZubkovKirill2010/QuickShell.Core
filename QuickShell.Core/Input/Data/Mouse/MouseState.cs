namespace QuickShell
{
    public readonly struct MouseState
    {
        public readonly int X;
        public readonly int Y;
        public readonly MouseButton Button;

        public MouseState(int X, int Y, MouseButton Button)
        {
            this.X = X;
            this.Y = Y;
            this.Button = Button;
        }
    }
}