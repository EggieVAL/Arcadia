using Microsoft.Xna.Framework.Input;

namespace Arcadia.Input
{
    public sealed class GamePadListener
    {
        public static GamePadListener P1 = new GamePadListener();

        public GamePadState CurrentState { get; private set; }

        public GamePadState PreviousState { get; private set; }

        public static bool IsButtonClicked(Buttons button)
        {
            return Instance.CurrentState.IsButtonDown(button) && Instance.PreviousState.IsButtonUp(button);
        }

        public static bool IsButtonPressed(Buttons button)
        {
            return Instance.CurrentState.IsButtonDown(button);
        }

        private GamePadListener()
        {
            PreviousState = GamePad.GetState();
            CurrentState = PreviousState;
        }

    }
}
