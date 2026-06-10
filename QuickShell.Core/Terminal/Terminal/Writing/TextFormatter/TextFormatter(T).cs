using Zion;

namespace QuickShell
{
    public sealed class TextFormatter<T> : ITextFormatter
    {
        public Type TargetType { get; } = typeof(T);

        private readonly Func<T, IFormattedString> Formatter;

        public TextFormatter(Func<T, IFormattedString> Formatter)
        {
            this.Formatter = Formatter.NotNull();
        }

        public IFormattedString Format(object Value)
        {
            return Formatter((T)Value);
        }
    }
}