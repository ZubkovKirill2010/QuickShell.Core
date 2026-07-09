namespace QuickShell
{
    //_Writing
    public sealed partial class Terminal
    {
        public void AddFormatter(ITextFormatter Formatter)
        {
            this.Formatter.AddFormatter(Formatter);
        }


        public void Write(IFormattedText? Value)
        {
            Value ??= Formatter.Null;
        }

        public void Write<T>(T? Value)
        {
            Write(Formatter.Format(Value));
        }
    }
}