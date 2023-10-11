using Microsoft.Xna.Framework.Input;

namespace Arcadia.Input
{
    /// <summary>
    /// The <see cref="KeyListener"/> class is a singleton class that listens to keyboard inputs. You can check whether a key is clicked or pressed.
    /// </summary>
    public sealed class KeyListener
    {
        /// <summary>
        /// The single instance of this class.
        /// </summary>
        public static readonly KeyListener Instance = new KeyListener();

        /// <summary>
        /// Ideally, the state of the keyboard right now; Depends if the listener is being updated in real-time.
        /// </summary>
        /// <seealso cref="Update"/>
        public KeyboardState CurrentState { get; private set; }

        /// <summary>
        /// The previous state of the keyboard.
        /// </summary>
        public KeyboardState PreviousState { get; private set; }

        /// <summary>
        /// Is the given <paramref name="key"/> being clicked by the user?
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><c>true</c> if the key was up previously, but is now down; otherwise <c>false</c>.</returns>
        public static bool IsKeyClicked(Keys key)
        {
            return Instance.CurrentState.IsKeyDown(key) && Instance.PreviousState.IsKeyUp(key);
        }

        /// <summary>
        /// Is the given <paramref name="key"/> being pressed by the user?
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><c>true</c> if the key is down; otherwise <c>false</c>.</returns>
        public static bool IsKeyPressed(Keys key)
        {
            return Instance.CurrentState.IsKeyDown(key);
        }

        /// <summary>
        /// Updates the keyboard state. Ideally, this should be called multiple times a frame to get real-time results.
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
