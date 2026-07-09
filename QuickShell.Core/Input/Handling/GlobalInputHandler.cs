using QuickShell.Runtime;
using Zion;

namespace QuickShell
{
    public sealed class GlobalInputHandler
    {
        private readonly Shell Shell;

        public GlobalInputHandler(Shell Shell)
        {
            this.Shell = Shell.NotNull();
        }


        public void OnKeyDown(KeyInfo KeyInfo)
        {
            Shell.InputHandler.OnKeyDown(KeyInfo);
        }

        public void OnMouseDown(MouseState State)
        {
            Shell.InputHandler.OnMouseDown(State);
        }

        public void OnMouseUp(MouseState State)
        {
            Shell.InputHandler.OnMouseUp(State);
        }
    }
}