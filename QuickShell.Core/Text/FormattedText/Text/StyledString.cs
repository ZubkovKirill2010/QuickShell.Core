using Zion;

namespace QuickShell
{
    public sealed class StyledString : IFormattedText
    {
        public readonly string String;
        public readonly TextStyle Style;

        public int Length { get; }

        public StyledString(string String, TextStyle Style)
        {
            this.String = String.NotNull();
            this.Length = String.Length;
            this.Style = Style;
        }

        public IEnumerator<FormattedChar> GetEnumerator()
        {
            TextStyle Style = this.Style;
            return String.Select(Char => new FormattedChar(Char, Style)).GetEnumerator();
        }
    }
}