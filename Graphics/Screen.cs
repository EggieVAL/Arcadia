using Arcadia.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arcadia.Graphics
{
    /// <summary>
    /// The <c>Screen</c> class manages the screen window of the game.
    /// </summary>
    public sealed class Screen
    {
        /// <summary>
        /// Whether the screen is in full-screen.
        /// </summary>
        public bool IsFullScreen { get; private set; }

        /// <summary>
        /// Whether the screen is in borderless full-screen.
        /// </summary>
        public bool IsBorderless { get; private set; }

        /// <summary>
        /// Constructs a <c>Screen</c> class that's bound to the
        /// game <paramref name="window"/>.
        /// </summary>
        /// <param name="graphics">The graphics device manager.</param>
        /// <param name="window">The game window.</param>
        public Screen(GraphicsDeviceManager graphics, GameWindow window)
        {
            _graphics = graphics;
            _window = window;
        }

        /// <summary>
        /// Updates the screen state of the game according to keyboard input(s).
        /// </summary>
        public void Update()
        {
            if (KeyListener.IsKeyClicked(Keys.F11))
            {
                if (KeyListener.IsKeyPressed(Keys.LeftControl))
                {
                    ToggleBorderless();
                }
                else
                {
                    ToggleFullScreen();
                }
            }
        }

        /// <summary>
        /// Toggles full screen.
        /// </summary>
        public void ToggleFullScreen()
        {
            bool wasFullScreen = IsFullScreen;
            if (IsBorderless)
            {
                IsBorderless = false;
            }
            else
            {
                IsFullScreen = !IsFullScreen;
            }
            ApplyFullScreenChanges(wasFullScreen);
        }

        /// <summary>
        /// Toggles borderless full screen.
        /// </summary>
        public void ToggleBorderless()
        {
            bool wasFullScreen = IsFullScreen;
            IsBorderless = !IsBorderless;
            IsFullScreen = IsBorderless;
            ApplyFullScreenChanges(wasFullScreen);
        }

        private void ApplyFullScreenChanges(bool wasFullScreen)
        {
            if (IsFullScreen)
            {
                if (wasFullScreen)
                {
                    ApplyHardwareMode();
                }
                else
                {
                    ActivateFullScreen();
                }
            }
            else
            {
                DeactivateFullScreen();
            }
        }

        private void ApplyHardwareMode()
        {
            _graphics.HardwareModeSwitch = !IsBorderless;
            _graphics.ApplyChanges();
        }

        private void ActivateFullScreen()
        {
            _width = _window.ClientBounds.Width;
            _height = _window.ClientBounds.Height;

            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.HardwareModeSwitch = !IsBorderless;
            _graphics.IsFullScreen = true;

            _graphics.ApplyChanges();
        }

        private void DeactivateFullScreen()
        {
            _graphics.PreferredBackBufferWidth = _width;
            _graphics.PreferredBackBufferHeight = _height;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
        }

        private GraphicsDeviceManager _graphics;
        private GameWindow _window;

        private int _width;
        private int _height;
    }
}
