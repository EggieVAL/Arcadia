using Arcadia.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Arcadia.Graphics
{
    /// <summary>
    ///     The <c>Screen</c> class manages the screen window of the game.
    /// </summary>
    public sealed class Screen
    {
        /// <summary>
        ///     Constructs a <c>Screen</c> class that's bound to the
        ///     GameWindow <paramref name="window"/>.
        /// </summary>
        /// <param name="graphics">The graphics device manager.</param>
        /// <param name="window">The game window.</param>
        public Screen(GraphicsDeviceManager graphics, GameWindow window)
        {
            _graphics = graphics;
            _window = window;
        }

        /// <summary>
        ///     This <c>Update</c> method updates the screen state of the
        ///     game according to keyboard input(s).
        /// </summary>
        public void Update()
        {
            KeyManager manager = KeyManager.Instance;
            if (manager.IsKeyClicked(Keys.F11))
            {
                if (manager.IsKeyDown(Keys.LeftControl))
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
        ///     Toggles full screen.
        /// </summary>
        public void ToggleFullScreen()
        {
            bool wasFullScreen = _fullscreen;
            if (_borderless)
            {
                _borderless = false;
            }
            else
            {
                _fullscreen = !_fullscreen;
            }
            ApplyFullScreenChanges(wasFullScreen);
        }

        /// <summary>
        ///     Toggles borderless full screen.
        /// </summary>
        public void ToggleBorderless()
        {
            bool wasFullScreen = _fullscreen;
            _borderless = !_borderless;
            _fullscreen = _borderless;
            ApplyFullScreenChanges(wasFullScreen);
        }

        private void ApplyFullScreenChanges(bool wasFullScreen)
        {
            if (_fullscreen)
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
            _graphics.HardwareModeSwitch = !_borderless;
            _graphics.ApplyChanges();
        }

        private void ActivateFullScreen()
        {
            _width = _window.ClientBounds.Width;
            _height = _window.ClientBounds.Height;

            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.HardwareModeSwitch = !_borderless;
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

        private bool _fullscreen;
        private bool _borderless;

        private int _width;
        private int _height;
    }
}
