using Zion;
using Zion.Vectors;

namespace QuickShell
{
    public sealed partial class Terminal
    {
        #region Data
        private readonly Buffer Buffer = new Buffer();
        private readonly RegionManager RegionManager = new RegionManager();

        public TextFormatter TextFormatter = new TextFormatter();
        public TextStyle BaseTextStyle = new TextStyle(new RGBColor(220));
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

        #endregion

        #region Functions
        public void Clear()
        {
            CursorPosition = Vector2Int.Zero;
            Buffer.Clear();
            ClearRegions();
        }

        #endregion
    }
}