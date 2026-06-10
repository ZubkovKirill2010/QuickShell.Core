using Zion;

namespace QuickShell
{
    [Serializable]
    public readonly struct TextStyle
    {
        public readonly RGBColor Color;

        public TextStyle(RGBColor Color)
        {
            this.Color = Color;
        }
    }
}