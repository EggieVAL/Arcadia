
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
        ///     Whether the key was clicked.
        /// </summary>
        /// <param name="key">A key.</param>
        /// <returns>
        ///     Returns <c>true</c> if key was originally up and is now
        ///     down, then return true; otherwise <c>false</c>.
        /// </returns>
        public bool IsKeyClicked(Keys key)
        {
            return _curr.IsKeyDown(key) && _prev.IsKeyUp(key);
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
            return _curr.IsKeyDown(key);
        }

        /// <summary>
        ///     Updates the previous and current keyboard state.
        /// </summary>
        public void Update()
        {
            _prev = _curr;
            _curr = Keyboard.GetState();
        }

        private KeyManager()
        {
            _prev = Keyboard.GetState();
            _curr = _prev;
        }

        private KeyboardState _curr;
        private KeyboardState _prev;
    }
}
