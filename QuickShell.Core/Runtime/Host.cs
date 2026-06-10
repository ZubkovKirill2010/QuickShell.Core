using Zion;

namespace QuickShell.Runtime
{
    public sealed class Host
    {
        public required Session CurrentSession;//TODO

        public required Action Close { get; init => field = value.NotNull(); }
    }
}