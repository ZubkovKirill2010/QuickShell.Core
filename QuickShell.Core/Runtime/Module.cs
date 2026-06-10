using Zion;

namespace QuickShell.Runtime
{
    public sealed class Module
    {
        public required Func<bool> CanClose { get; init => field = value.NotNull(); }
    }
}