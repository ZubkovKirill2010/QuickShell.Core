namespace QuickShell
{
    //_Writing
    public sealed partial class Terminal
    {
        public void AddFormatter(ITextFormatter Formatter)
        {
            TextFormatter.AddFormatter(Formatter);
        }


        public void Write(IFormattedString? Value)
        {
            //TODO
        }

        public void Write<T>(T? Value)
        {
            Write(TextFormatter.Format(Value));
        }
    }
}