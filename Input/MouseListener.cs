using Microsoft.Xna.Framework.Input;

namespace Arcadia.Input
{
    public sealed class MouseListener
    {
        public static readonly MouseListener Instance = new MouseListener();

        public MouseState CurrentState { get; private set; }

        public MouseState PreviousState { get; private set; }

        public int PositionX => CurrentState.X;

        public int PositionY => CurrentState.Y;

        public static bool IsLeftButtonClicked()
        {
            return Instance.CurrentState.LeftButton == ButtonState.Pressed &&
                   Instance.PreviousState.LeftButton == ButtonState.Released;
        }

        public static bool IsLeftButtonPressed()
        {
            return Instance.CurrentState.LeftButton == ButtonState.Pressed;
        }

        public static bool IsMiddleButtonClicked()
        {
            return Instance.CurrentState.MiddleButton == ButtonState.Pressed &&
                   Instance.PreviousState.MiddleButton == ButtonState.Released;
        }

        public static bool IsMiddleButtonPressed()
        {
            return Instance.CurrentState.MiddleButton == ButtonState.Pressed;
        }

        public static bool IsRightButtonClicked()
        {
            return Instance.CurrentState.RightButton == ButtonState.Pressed &&
                   Instance.PreviousState.RightButton == ButtonState.Released;
        }

        public static bool IsRightButtonDown()
        {
            return Instance.CurrentState.RightButton == ButtonState.Pressed;
        }

        public static bool IsScrollingUp()
        {
            return Instance.CurrentState.ScrollWheelValue >
                   Instance.PreviousState.ScrollWheelValue;
        }

        public static bool IsScrollingDown()
        {
            return Instance.CurrentState.ScrollWheelValue <
                   Instance.PreviousState.ScrollWheelValue;
        }

        public static void Update()
        {
            Instance.PreviousState = Instance.CurrentState;
            Instance.CurrentState = Mouse.GetState();
        }

        private MouseListener()
        {
            PreviousState = Mouse.GetState();
            CurrentState = PreviousState;
        }
    }
}
