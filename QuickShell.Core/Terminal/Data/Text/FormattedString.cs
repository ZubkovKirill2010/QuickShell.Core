using System.Collections;
using Zion;

namespace QuickShell
{
    public sealed class FormattedString : IFormattedString
    {
        private readonly FormattedChar[] Chars;
        private readonly int _Length;

        public int Length => _Length;


        public FormattedString(int Length)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(Length, nameof(Length));

            Chars = new FormattedChar[Length];
            _Length = Length;
        }


        public FormattedChar this[int Index]
        {
            get => Chars[Index];
            set => Chars[Index] = value;
        }
        public FormattedChar this[Index Index]
        {
            get => Chars[Index];
            set => Chars[Index] = value;
        }


        public IEnumerable<FormattedChar> Range(int Start, int Length)
        {
            return Chars.Range(Start, Length);
        }


        public IEnumerator<FormattedChar> GetEnumerator()
        {
            return ((IEnumerable<FormattedChar>)Chars).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Chars.GetEnumerator();
        }
    }
}