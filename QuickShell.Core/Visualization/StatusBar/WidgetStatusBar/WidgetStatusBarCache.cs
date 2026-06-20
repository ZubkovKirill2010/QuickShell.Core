namespace QuickShell
{
    internal sealed class WidgetStatusBarCache
    {
        private readonly HashSet<StatusBarWidget> InactiveWidgets = new();
        private readonly Dictionary<StatusBarWidget, int> WidgetPositions = new();


        public void Hide(StatusBarWidget Widget)
        {
            InactiveWidgets.Add(Widget);
        }

        public bool IsActive(StatusBarWidget Widget)
        {
            return !InactiveWidgets.Contains(Widget);
        }


        public void AddPosition(StatusBarWidget Widget, int Position)
        {
            WidgetPositions[Widget] = Position;
        }

        public int GetPosition(StatusBarWidget Widget)
        {
            return WidgetPositions[Widget];
        }


        public void Clear()
        {
            InactiveWidgets.Clear();
            WidgetPositions.Clear();
        }
    }
}