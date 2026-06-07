namespace QuickShell
{
    [Serializable]
    public readonly struct KeyboardInfo
    {
        public readonly Key Key;
        public readonly ModifierKeys Modifiers;

        public readonly char Char;

        public bool Shift => Modifiers.HasFlag(ModifierKeys.Shift);
        public bool Ctrl  => Modifiers.HasFlag(ModifierKeys.Ctrl);
        public bool Alt   => Modifiers.HasFlag(ModifierKeys.Alt);
        public bool Win   => Modifiers.HasFlag(ModifierKeys.Win);

        public KeyboardInfo(Key Key, ModifierKeys Modifiers, char KeyChar)
        {
            this.Key = Key;
            this.Char = KeyChar;
        }
    }
}