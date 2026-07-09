using Zion;
using Zion.Vectors;

namespace QuickShell
{
    public sealed partial class Terminal
    {
        #region Data
        private readonly Buffer Buffer;
        private readonly TextFormatter Formatter;

        public TextStyle BaseTextStyle = new TextStyle(new RGBColor(220));

        public readonly TerminalBinding Binding;

        #endregion

        #region Cursor
        public Vector2Int CursorPosition
        {
            get => new Vector2Int(CursorX, CursorY);
            set
            {
                CursorX = value.X;
                CursorY = value.Y;
            }
        }

        public int CursorX
        {
            get;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value);
                field = value;
            }
        }

        public int CursorY
        {
            get;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value);
                field = value;
            }
        }

        public IFormattedText Null
        {
            get => Formatter.Null;
            set => Formatter.Null = value;
        }

        #endregion

        #region Constructors
        public Terminal()
        {
            Buffer = new Buffer();
            Formatter = new TextFormatter();
            Binding = new TerminalBinding();
        }

        #endregion

        #region Functions
        public void Clear()
        {
            CursorPosition = Vector2Int.Zero;
            Buffer.Clear();
        }

        #endregion
    }
}