namespace QuickShell
{
    public interface IInputHandler
    {
        public virtual bool IsActive { get => true; }

        public virtual void OnKeyDown(KeyboardHandler Handler) { }

        public virtual void OnMouseDown(MouseHandler Handler) { }

        public virtual void OnMouseUp(MouseHandler Handler) { }
    }
}