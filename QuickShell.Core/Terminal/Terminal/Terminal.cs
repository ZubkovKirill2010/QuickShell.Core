using Zion.Vectors;

namespace QuickShell
{
    public sealed partial class Terminal
    {
        #region Data
        private readonly Buffer Buffer = new Buffer();
        private readonly RegionManager RegionManager = new RegionManager();

        public TextStyle BaseTextStyle;
        public TextFormatter TextFormatter;
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
            ClearRegions();
            Buffer.Clear();
        }

        #endregion
    }
}