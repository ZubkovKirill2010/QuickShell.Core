using Zion;

namespace QuickShell
{
    public readonly struct TextFormatter<T> : ITextFormatter
    {
        public Type TargetType { get; } = typeof(T);

        private readonly Func<T, IFormattedText> Formatter;

        public TextFormatter(Func<T, IFormattedText> Formatter)
        {
            this.Formatter = Formatter.NotNull();
        }

        public IFormattedText Format(object Value)
        {
            return Formatter((T)Value);
        }
    }
}