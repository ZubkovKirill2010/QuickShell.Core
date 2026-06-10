using System.Security.Cryptography;
using Zion;

namespace QuickShell
{
    public sealed class RegionManager
    {
        private readonly SortedList<Region> Regions   = new SortedList<Region>();
        private readonly Stack<int> UnfinishedRegions = new Stack<int>();


        public void BeginRegion(int Start)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(Start);
            UnfinishedRegions.Push(Start);
        }

        public void EndRegion(int End)
        {
            if (UnfinishedRegions.TryPop(out int Start) && End > Start)
            {
                Regions.Add(new Region(Start, End));
            }
        }


        public bool Add(Region Region)
        {
            if (!Regions.Contains(Region.NotNull()))
            {
                Regions.Add(Region.NotNull());
                return true;
            }
            return false;
        }

        public void Clear()
        {
            Regions.Clear();
            UnfinishedRegions.Clear();
        }


        public void CollapseAll()
        {
            bool Collapsed = false;

            foreach (Region Region in Regions.Where(static Region => !Region.Collapsed))
            {
                Region.Collapsed = true;
                Collapsed = true;
            }

            if (Collapsed)
            {
                //TODO
            }
        }

        public void ExpandAll()
        {
            bool Expanded = false;

            foreach (Region Region in Regions.Where(static Region => Region.Collapsed))
            {
                Region.Collapsed = false;
                Expanded = true;
            }

            if (Expanded)
            {
                //TODO
            }
        }
    }
}