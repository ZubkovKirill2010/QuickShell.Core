namespace QuickShell
{
    public abstract class Session
    {
        public readonly InputManager Input = new();
        public readonly Terminal Terminal = new();

        public readonly StatusBarBinding StatusBarBinding = new();
        public StatusBar StatusBar
        {
            get => StatusBarBinding.Visualizer.Value;
            set
            {
                value ??= NullStatusBarVisualizer.Instance;
                StatusBarBinding.Visualizer.Value = value;
            }
        }


        public string Title
        {
            get;
            set
            {
                value ??= "Title";
                field = value;
                TitleChanged?.Invoke(value);
            }
        } = "Title";

        public event Action<string>? TitleChanged;


        public abstract void Main(in ModuleArguments Arguments);

        public virtual bool CanClose() => true;

        public virtual void OnClosed() { }


        public void Close()
        {
            if (CanClose())
            {
                CloseForced();
            }
        }

        public void CloseForced()
        {
            OnClosed();
            //TODO
        }
    }
}