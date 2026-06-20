using Zion;

namespace QuickShell
{
    public sealed class StatusBarWidget : IComparable<StatusBarWidget>
    {
        public readonly int Priority;
        public readonly StatusBarWidgetPosition Position;

        public int Length { get; private set; }
        public string Content
        {
            get;
            set
            {
                if (field == value) { return; }

                field = value.NotNull();

                int LengthOffset = value.Length - Length;
                Length = value.Length;

                Changed?.Invoke(this, LengthOffset);
            }
        }

        public event Action<StatusBarWidget, int>? Changed;

        public int CompareTo(StatusBarWidget? Other)
        {
            return Priority.CompareTo(Other.NotNull().Priority);
        }
    }
}