using Zion;

namespace QuickShell
{
    public sealed class ColoredString : IFormattedText
    {
        public readonly string String;
        public readonly RGBColor Color;

        public int Length { get; }

        public ColoredString(string String, RGBColor Color)
        {
            this.String = String.NotNull();
            this.Length = String.Length;
            this.Color = Color;
        }

        public IEnumerator<FormattedChar> GetEnumerator()
        {
            RGBColor Color = this.Color;
            return String.Select(Char => new FormattedChar(Char, Color)).GetEnumerator();
        }
    }
}