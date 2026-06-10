namespace QuickShell
{
    //_Regions
    public sealed partial class Terminal
    {
        public void BeginRegion()
        {
            RegionManager.BeginRegion(CursorY);
        }

        public void EndRegion()
        {
            RegionManager.EndRegion(CursorY);
        }


        public bool AddRegion(Region Region)
        {
            return RegionManager.Add(Region);
        }

        public void ClearRegions()
        {
            RegionManager.Clear();
        }


        public void CollapseAllRegions()
        {
            RegionManager.CollapseAll();
        }

        public void ExpandAllRegions()
        {
            RegionManager.ExpandAll();
        }
    }
}