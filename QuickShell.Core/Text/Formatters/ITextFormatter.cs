namespace QuickShell
{
    public interface ITextFormatter
    {
        public Type TargetType { get; }

        public IFormattedText Format(object Value);
    }
}