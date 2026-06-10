namespace QuickShell
{
    public abstract class ModuleHub
    {
        public abstract void Main(in ModuleArguments Arguments);

        public virtual bool CanClose() => true;

        public virtual void OnClosed() { }
    }
}