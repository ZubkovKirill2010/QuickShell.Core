using Zion;

namespace QuickShell
{
    [Serializable]
    public readonly struct FormattedChar
    {
        public static readonly FormattedChar Default = new FormattedChar();

        public readonly char Char;
        public readonly RGBColor Color;

        public FormattedChar(char Char, RGBColor Color)
        {
            this.Char = Char;
            this.Color = Color;
        }
        public FormattedChar(char Char, TextStyle Style)
        {
            this.Char = Char;
            this.Color = Style.Color;
        }
        public FormattedChar(char Char)
            : this(Char, RGBColor.White) { }


        public static bool operator ==(FormattedChar A, FormattedChar B)
        {
            return A.Char == B.Char && A.Color == B.Color;
        }
        public static bool operator ==(FormattedChar A, char B)
        {
            return A.Char == B;
        }
        public static bool operator ==(char A, FormattedChar B)
        {
            return A == B.Char;
        }

        public static bool operator !=(FormattedChar A, FormattedChar B)
        {
            return A.Char != B.Char || A.Color != B.Color;
        }
        public static bool operator !=(FormattedChar A, char B)
        {
            return A.Char != B;
        }
        public static bool operator !=(char A, FormattedChar B)
        {
            return A != B.Char;
        }


        public override bool Equals(object? Object)
        {
            return Object is FormattedChar Char && this == Char;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int Hash = 17;
                Hash = (Hash * 23) + Char.GetHashCode();
                Hash = (Hash * 23) + Color.GetHashCode();
                return Hash;
            }
        }
    }
}