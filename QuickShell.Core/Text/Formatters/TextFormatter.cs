using Zion;

namespace QuickShell
{
    public sealed class TextFormatter
    {
        private readonly Dictionary<Type, ITextFormatter> SealedFormatters = new();
        private readonly List<ITextFormatter> Formatters = new();

        public IFormattedText Null { get; set => field = value.NotNull(); }
            = new ColoredString("null", new RGBColor(69, 161, 232));

        public TextStyle BaseStyle { get; init; }
            = new TextStyle(RGBColor.White);


        public IFormattedText Format<T>(T Value)
        {
            if (Value is null)
            {
                return Null;
            }

            if (TryGetFormatter<T>(out ITextFormatter Formatter))
            {
                return Formatter.Format(Value);
            }

            return new StyledString(Value.ToNotNullString(), BaseStyle);
        }


        public void AddFormatter(ITextFormatter Formatter)
        {
            ArgumentNullException.ThrowIfNull(Formatter);

            Type Type = Formatter.TargetType;

            if (Type.IsSealed)
            {
                SealedFormatters[Type] = Formatter;
            }
            else
            {
                Formatters.Add(Formatter);
            }
        }


        private bool TryGetFormatter<T>(out ITextFormatter TextFormatter)
        {
            Type TargetType = typeof(T);

            if (SealedFormatters.TryGetValue(TargetType, out TextFormatter))
            {
                return true;
            }
            foreach (ITextFormatter Formatter in Formatters)
            {
                if (Formatter.TargetType.IsAssignableFrom(TargetType))
                {
                    TextFormatter = Formatter;
                    return true;
                }
            }
            return false;
        }
    }
}