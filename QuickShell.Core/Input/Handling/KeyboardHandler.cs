namespace QuickShell
{
    public sealed class KeyboardHandler
    {
        public readonly KeyboardInfo Info;
        public bool Handled;

        public KeyboardHandler(KeyboardInfo KeyInfo)
        {
            this.Info = KeyInfo;
        }
    }
}