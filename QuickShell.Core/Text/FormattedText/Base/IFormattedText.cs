using System.Collections;

namespace QuickShell
{
    public interface IFormattedText : IEnumerable<FormattedChar>
    {
        public int Length { get; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}