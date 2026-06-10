namespace QuickShell
{
    public sealed class KeyboardHandler
    {
        public readonly KeyInfo Info;
        public bool Handled;

        public KeyboardHandler(KeyInfo KeyInfo)
        {
            this.Info = KeyInfo;
        }
    }
}