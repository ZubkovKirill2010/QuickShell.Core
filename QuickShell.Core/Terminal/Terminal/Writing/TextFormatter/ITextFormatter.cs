namespace QuickShell
{
    public interface ITextFormatter
    {
        public Type TargetType { get; }

        public IFormattedString Format(object Value);
    }
}