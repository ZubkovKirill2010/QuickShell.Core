using System.Collections;
using Zion;

namespace QuickShell
{
    public sealed class ColoredString : IFormattedString
    {
        private readonly string String;
        private readonly RGBColor Color;
        private readonly int _Length;

        public int Length => _Length;


        public ColoredString(string String, RGBColor Color)
        {
            ArgumentNullException.ThrowIfNull(String);

            this.String = String;
            this.Color = Color;
            _Length = String.Length;
        }
        public ColoredString(string String)
            : this(String, RGBColor.White) { }


        public FormattedChar this[int Index] => new FormattedChar(String[Index], Color);


        public static bool operator ==(ColoredString A, ColoredString B)
        {
            return A.String == B.String && A.Color == B.Color;
        }
        public static bool operator !=(ColoredString A, ColoredString B)
        {
            return A.String != B.String || A.Color != B.Color;
        }


        public override bool Equals(object? Object)
        {
            return Object is ColoredString String && this == String;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int Hash = 17;
                Hash = (Hash * 23) + String.GetHashCode();
                Hash = (Hash * 23) + Color.GetHashCode();
                return Hash;
            }
        }


        public IEnumerator<FormattedChar> GetEnumerator()
        {
            foreach (char Char in String)
            {
                yield return new FormattedChar(Char, Color);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}