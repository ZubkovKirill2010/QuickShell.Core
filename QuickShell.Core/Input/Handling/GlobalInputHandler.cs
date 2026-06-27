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
            Shell.CurrentSession.Input.KeyDown(KeyInfo);
        }

        public void OnMouseDown(MouseState State)
        {
            Shell.CurrentSession.Input.MouseDown(State);
        }

        public void OnMouseUp(MouseState State)
        {
            Shell.CurrentSession.Input.MouseUp(State);
        }
    }
}