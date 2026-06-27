using System.Collections;
using Zion;

namespace QuickShell
{
    internal sealed class WidgetList : IDisposable, IEnumerable<StatusBarWidget>
    {
        private readonly List<StatusBarWidget> Widgets;
        private readonly SortedList<StatusBarWidget> SortedWidgets;
        private readonly HashSet<StatusBarWidget> ChangedWidgets;

        public int TotalLength { get; private set; }
        public int Count { get; private set; }

        public bool LengthChanged;
        public bool HasContentChanged => ChangedWidgets.Count > 0;

        public WidgetList()
        {
            Widgets = new();
            SortedWidgets = new();
            ChangedWidgets = new();
        }


        public void Add(StatusBarWidget Widget)
        {
            ArgumentNullException.ThrowIfNull(Widget);

            Widgets.Add(Widget);
            SortedWidgets.Add(Widget);

            Count++;
            TotalLength += Widget.Length;

            Widget.Changed += OnChanged;
        }

        public void ClearChanged()
        {
            ChangedWidgets.Clear();
        }


        public StatusBarWidget GetSorted(int Index)
        {
            return SortedWidgets[Index];
        }

        public int GetLength(int SeporatorLength)
        {
            return TotalLength + (Count - 1) * SeporatorLength;
        }


        private void OnChanged(StatusBarWidget Widget, int LengthOffset)
        {
            TotalLength += LengthOffset;

            if (LengthOffset == 0)
            {
                ChangedWidgets.Add(Widget);
            }
            else
            {
                LengthChanged = true;
            }
        }


        public void Dispose()
        {
            foreach (StatusBarWidget Widget in Widgets)
            {
                Widget.Changed -= OnChanged;
            }
        }


        public IEnumerable<StatusBarWidget> GetChanged()
        {
            return ChangedWidgets;
        }

        public IEnumerator<StatusBarWidget> EnumerateSorted()
        {
            return SortedWidgets.GetEnumerator();
        }

        public IEnumerator<StatusBarWidget> GetEnumerator()
        {
            return Widgets.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}