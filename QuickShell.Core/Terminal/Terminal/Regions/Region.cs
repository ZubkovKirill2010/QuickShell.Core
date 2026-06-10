using System.Diagnostics.CodeAnalysis;
using Zion;

namespace QuickShell
{
    public sealed class Region : IComparable<Region>
    {
        public readonly int Start;
        public readonly int Size;
        public bool Collapsed;

        public Region(int Start, int Size)
        {
            ArgumentOutOfRangeException.ThrowIf(Start < 0, $"Start(={Start}) < 0");
            ArgumentOutOfRangeException.ThrowIf(Size <= 1, $"Size(={Size}) <= 1");

            this.Start = Start;
            this.Size = Size;
        }


        public override string ToString()
        {
            return $"[Start:{Start} > Size: {Size} > Collapsed: {Collapsed}]";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, Size);
        }

        public override bool Equals([NotNullWhen(true)] object? Object)
        {
            return Object is Region Region && Start == Region.Start && Size == Region.Size;
        }


        public int CompareTo(Region Other)
        {
            return Start.CompareTo(Other.NotNull().Start);
        }
    }
}