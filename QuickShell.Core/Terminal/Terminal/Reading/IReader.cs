using Zion.STP;

namespace QuickShell
{
    public interface IReader<T> where T : allows ref struct
    {
        public bool Read(TextSource Source, out T Result);
    }
}