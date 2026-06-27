namespace QuickShell
{
    public sealed class InputManager
    {
        private readonly Stack<IInputHandler> Handlers;
        private readonly IEnumerable<IInputHandler> ActiveHandlers;

        public int HandlerCount => Handlers.Count;

        public InputManager()
        {
            Handlers = new Stack<IInputHandler>();
            ActiveHandlers = Handlers.Where(static Item => Item.IsActive);
        }


        public void KeyDown(KeyInfo KeyInfo)
        {
            foreach (IInputHandler Handler in ActiveHandlers)
            {
                KeyboardHandler KeyboardHandler = new KeyboardHandler(KeyInfo);
                Handler.OnKeyDown(KeyboardHandler);

                if (KeyboardHandler.Handled)
                {
                    break;
                }
            }
        }

        public void MouseDown(MouseState State)
        {
            foreach (IInputHandler Handler in ActiveHandlers)
            {
                MouseHandler MouseHandler = new MouseHandler(State);
                Handler.OnMouseDown(MouseHandler);

                if (MouseHandler.Handled)
                {
                    break;
                }
            }
        }

        public void MouseUp(MouseState State)
        {
            foreach (IInputHandler Handler in ActiveHandlers)
            {
                MouseHandler MouseHandler = new MouseHandler(State);
                Handler.OnMouseUp(MouseHandler);

                if (MouseHandler.Handled)
                {
                    break;
                }
            }
        }


        public void PushHandler(IInputHandler Handler)
        {
            Handlers.Push(Handler);
        }

        public void PopHandler()
        {
            if (HandlerCount != 0)
            {
                Handlers.Pop();
            }            
        }
    }
}