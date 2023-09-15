using Microsoft.Xna.Framework.Input;

namespace Arcadia.Input
{
    public sealed class KeyListener
    {
        public static readonly KeyListener Instance = new KeyListener();

        public KeyboardState CurrentState { get; private set; }

        public KeyboardState PreviousState { get; private set; }

        public static bool IsKeyClicked(Keys key)
        {
            return Instance.CurrentState.IsKeyDown(key) && Instance.PreviousState.IsKeyUp(key);
        }

        public static bool IsKeyPressed(Keys key)
        {
            return Instance.CurrentState.IsKeyDown(key);
        }

        public static void Update()
        {
            Instance.PreviousState = Instance.CurrentState;
            Instance.CurrentState = Keyboard.GetState();
        }

        private KeyListener()
        {
            PreviousState = Keyboard.GetState();
            CurrentState = PreviousState;
        }
    }
}
