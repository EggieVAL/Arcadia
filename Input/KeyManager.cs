
using Microsoft.Xna.Framework.Input;

namespace Arcadia.Input
{
    /// <summary>
    ///     The <c>KeyManager</c> is a singleton class that handles key inputs.
    /// </summary>
    public sealed class KeyManager
    {
        /// <summary>
        ///     An instance of a <c>KeyManager</c>.
        /// </summary>
        public static readonly KeyManager Instance = new KeyManager();

        /// <summary>
        ///     The current keyboard state.
        /// </summary>
        public KeyboardState CurrentState { get; private set; }

        /// <summary>
        ///     The previous keyboard state.
        /// </summary>
        public KeyboardState PreviousState { get; private set; }

        /// <summary>
        ///     Whether the key was clicked.
        /// </summary>
        /// <param name="key">A key.</param>
        /// <returns>
        ///     Returns <c>true</c> if key was originally up and is now
        ///     down, then return true; otherwise <c>false</c>.
        /// </returns>
        public bool IsKeyClicked(Keys key)
        {
            return CurrentState.IsKeyDown(key) && PreviousState.IsKeyUp(key);
        }

        /// <summary>
        ///     Whether the key is held down.
        /// </summary>
        /// <param name="key">A key.</param>
        /// <returns>
        ///     Returns <c>true</c> if key is held down; otherwise
        ///     <c>false</c>.
        /// </returns>
        public bool IsKeyDown(Keys key)
        {
            return CurrentState.IsKeyDown(key);
        }

        /// <summary>
        ///     Updates the previous and current keyboard state.
        /// </summary>
        public void Update()
        {
            PreviousState = CurrentState;
            CurrentState = Keyboard.GetState();
        }

        private KeyManager()
        {
            PreviousState = Keyboard.GetState();
            CurrentState = PreviousState;
        }
    }
}
