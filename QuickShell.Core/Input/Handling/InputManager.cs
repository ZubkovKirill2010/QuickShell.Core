namespace QuickShell
{
    public sealed class InputManager
    {
        private static readonly Stack<IInputHandler> Handlers
            = new Stack<IInputHandler>();

        private static readonly IEnumerable<IInputHandler> ActiveHandlers
            = Handlers.Where(Item => Item.IsActive);

        public static int HandlerCount { get; private set; }


        public static void HandleKeyDown(KeyInfo KeyInfo)
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

        public static void HandleMouseDown(MouseState State)
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

        public static void HandleMouseUp(MouseState State)
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


        public static void PushHandler(IInputHandler Handler)
        {
            Handlers.Push(Handler);
            HandlerCount++;
        }

        public static void PopHandler()
        {
            if (HandlerCount == 0)
            {
                return;
            }

            Handlers.Pop();
            HandlerCount--;
        }
    }
}