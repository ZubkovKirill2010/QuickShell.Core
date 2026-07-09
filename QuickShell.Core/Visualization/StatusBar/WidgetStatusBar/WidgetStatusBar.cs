using Zion;

namespace QuickShell
{
    public sealed class WidgetStatusBar : StatusBar, IDisposable
    {
        private readonly WidgetStatusBarCache Cache = new();
        private readonly WidgetList Left   = new(),
                                    Center = new(),
                                    Right  = new();

        public int MinSpacing   { get; init => field = Math.Max(1, value); }
        public string Seporator { get; init => field = value.NotNull(); } = " | ";

        private bool ContainsWidgets;


        public override void Draw(StatusBarVisualizingContext Context)
        {
            if (!ContainsWidgets) { return; }

            WidgetStatusBarCache Cache = this.Cache;

            int SeporatorLength = Seporator.Length;
            int BufferLength = Context.Length;

            int LeftLength = Left.GetLength(SeporatorLength);
            int CenterLength = Center.GetLength(SeporatorLength);
            int RightLength = Right.GetLength(SeporatorLength);

            int CenterStart = GetCenterStart(BufferLength, CenterLength);

            if (AnyGroupLengthChanged() || Context.LengthChanged)
            {
                Cache.Clear();

                int LeftRemoved = 0, CenterRemoved = 0, RightRemoved = 0;

                while (true)
                {
                    bool LeftConflict = CenterStart - LeftLength < MinSpacing;
                    bool RightConflict = BufferLength - RightLength - CenterStart - CenterLength < MinSpacing;

                    if (!(LeftConflict || RightConflict))
                    {
                        break;
                    }

                    IEnumerable<(WidgetList, int)> Lists = (LeftConflict, RightConflict) switch
                    {
                        (true, false) => [(Left, LeftRemoved), (Center, CenterRemoved)],
                        (false, true) => [(Center, CenterRemoved), (Right, RightRemoved)],
                        (true, true) => [(Left, LeftRemoved), (Center, CenterRemoved), (Right, RightRemoved)]
                    };

                    StatusBarWidget MostUnimportant = Min(Lists);
                    Cache.Hide(MostUnimportant);

                    switch (MostUnimportant.Position)
                    {
                        case StatusBarWidgetPosition.Left:
                            LeftLength = ++LeftRemoved == Left.Count
                                ? 0 : LeftLength - MostUnimportant.Length - SeporatorLength;
                            break;

                        case StatusBarWidgetPosition.Center:
                            CenterLength = ++CenterRemoved == Center.Count
                                ? 0 : CenterLength - MostUnimportant.Length - SeporatorLength;
                            CenterStart = GetCenterStart(BufferLength, CenterLength);
                            break;

                        case StatusBarWidgetPosition.Right:
                            RightLength = ++RightRemoved == Right.Count
                                ? 0 : RightLength - MostUnimportant.Length - SeporatorLength;
                            break;
                    }
                }

                Write(Context, Left, 0);
                Write(Context, Center, CenterStart);
                Write(Context, Right, BufferLength - RightLength);
            }
            else if (Left.HasContentChanged || Center.HasContentChanged || Right.HasContentChanged)
            {
                WriteChanged(Context, Left);
                WriteChanged(Context, Center);
                WriteChanged(Context, Right);
            }
        }


        public void Add(StatusBarWidget Widget)
        {
            ArgumentNullException.ThrowIfNull(Widget);

            GetList(Widget.Position).Add(Widget);
            ContainsWidgets = true;
        }


        public void Dispose()
        {
            Left.Dispose();
            Center.Dispose();
            Right.Dispose();
        }


        private WidgetList GetList(StatusBarWidgetPosition Position)
        {
            return Position switch
            {
                StatusBarWidgetPosition.Left => Left,
                StatusBarWidgetPosition.Center => Center,
                StatusBarWidgetPosition.Right => Right,
                _ => throw new Exception("Unknow widget position")
            };
        }

        private bool AnyGroupLengthChanged()
        {
            bool Result = Left.LengthChanged || Center.LengthChanged || Right.LengthChanged;
            Left.LengthChanged = false;
            Center.LengthChanged = false;
            Right.LengthChanged = false;
            return Result;
        }


        private void Write(StatusBarVisualizingContext Context, WidgetList Widgets, int Start)
        {
            WidgetStatusBarCache Cache = this.Cache;
            bool First = true;

            foreach (StatusBarWidget Widget in Widgets.Where(Cache.IsActive))
            {
                Cache.AddPosition(Widget, Start);

                if (!First)
                {
                    Write(Context, Seporator, Start);
                    Start += Seporator.Length;
                }
                Write(Context, Widget.Content, Start);
                Start += Widget.Content.Length;
                First = false;
            }
        }

        private void WriteChanged(StatusBarVisualizingContext Context, WidgetList Widgets)
        {
            WidgetStatusBarCache Cache = this.Cache;
            foreach (StatusBarWidget Widget in Widgets.GetChanged().Where(Cache.IsActive))
            {
                Write(Context, Widget.Content, Cache.GetPosition(Widget));
            }
            Widgets.ClearChanged();
        }


        private static void Write(StatusBarVisualizingContext Context, string Text, int Start)
        {
            int Length = Text.Length;
            for (int i = 0; i < Length; i++)
            {
                Context[Start + i] = Text[i];
            }
        }


        private static StatusBarWidget Min(params IEnumerable<(WidgetList, int)> Lists)
        {
            return Lists.Select(Pair => Pair.Item1.GetSorted(Pair.Item2)).Min().NotNull();
        }

        private static int GetCenterStart(int BufferLength, int CenterLength)
        {
            return (BufferLength - CenterLength) >> 1;
        }
    }
}