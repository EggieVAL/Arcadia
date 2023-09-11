using Microsoft.Xna.Framework.Input;

namespace Arcadia.Input
{
    /// <summary>
    /// The <c>KeyListener</c> is a singleton class that handles key inputs.
    /// </summary>
    public sealed class KeyListener
    {
        /// <summary>
        /// The single instance of this class.
        /// </summary>
        public static readonly KeyListener Instance = new KeyListener();

        /// <summary>
        /// Ideally, what the mouse state is right now.
        /// </summary>
        /// <see cref="Update"/>
        public KeyboardState CurrentState { get; private set; }

        /// <summary>
        /// Ideally, what the mouse state was a frame before.
        /// </summary>
        /// <see cref="Update"/>
        public KeyboardState PreviousState { get; private set; }

        /// <summary>
        /// Whether the key is clicked.
        /// </summary>
        /// <param name="key">A key.</param>
        /// <returns>
        /// Returns true if the key is pressed, but was not pressed
        /// a frame before.
        /// </returns>
        public static bool IsKeyClicked(Keys key)
        {
            return Instance.CurrentState.IsKeyDown(key) &&
                   Instance.PreviousState.IsKeyUp(key);
        }

        /// <summary>
        /// Whether the key is pressed.
        /// </summary>
        /// <param name="key">A key.</param>
        /// <returns>Returns true if the key is pressed.</returns>
        public static bool IsKeyPressed(Keys key)
        {
            return Instance.CurrentState.IsKeyDown(key);
        }

        /// <summary>
        /// Updates the previous key state to the current key state.
        /// Updates the current key state to the key state right now.
        /// The update method should be called multiple times a second
        /// for accurate results.
        /// </summary>
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
