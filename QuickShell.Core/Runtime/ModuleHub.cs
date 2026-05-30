namespace QuickShell.Runtime
{
    public abstract class ModuleHub
    {
        private void Initialize(ModuleContext Context)
        {
            //Initialization
            Start(Context);
        }

        protected abstract void Start(ModuleContext Context);

        public virtual bool CanClosed() => true;

        public virtual void OnClosed() { }
    }
}