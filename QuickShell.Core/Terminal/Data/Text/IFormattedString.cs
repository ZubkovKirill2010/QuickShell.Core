namespace QuickShell
{
    public interface IFormattedString : IEnumerable<FormattedChar>
    {
        public int Length { get; }

        public FormattedChar this[int Index] { get; }
    }
}