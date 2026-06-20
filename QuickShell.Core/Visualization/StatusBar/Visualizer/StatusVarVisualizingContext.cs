using Zion;

namespace QuickShell
{
    public sealed class StatusBarVisualizingContext
    {
        private readonly char[] Buffer;

        public readonly int Length;
        public readonly bool LengthChanged;

        public StatusBarVisualizingContext(char[] Buffer, int Length, bool LengthChanged)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(Length, Buffer.Length);

            this.Buffer = Buffer.NotNull();
            this.Length = Length;
            this.LengthChanged = LengthChanged;
        }


        public char this[int Index]
        {
            get => Buffer[Index];
            set => Buffer[Index] = Filter(in value);
        }

        public char this[Index Index]
        {
            get => Buffer[Index];
            set => Buffer[Index] = Filter(in value);
        }


        public void Clear()
        {
            char[] Buffer = this.Buffer;
            int Length = this.Length;

            for (int i = 0; i < Length; i++)
            {
                Buffer[i] = '\0';
            }
        }


        private static char Filter(in char Char)
        {
            return char.IsWhiteSpace(Char) ? '\0' : Char;
        }
    }
}