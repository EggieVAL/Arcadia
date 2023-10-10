using Arcadia.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arcadia.Graphics
{
    /// <summary>
    /// The <see cref="Screen"/> class manages screen-related activities. This class mainly tracks keyboard inputs requesting a screen mode
    /// change, such as windowed, fullscreen, borderless fullscreen mode.
    /// </summary>
    public sealed class Screen
    {
        /// <summary>
        /// Is the screen in fullscreen mode?
        /// </summary>
        public bool IsFullScreen { get; private set; }

        /// <summary>
        /// Is the screen in fullscreen borderless mode?
        /// </summary>
        public bool IsBorderless { get; private set; }

        /// <summary>
        /// Constructs a screen that manages the application's screen.
        /// </summary>
        /// <param name="graphics">The graphics device manager.</param>
        /// <param name="window">The game window.</param>
        public Screen(GraphicsDeviceManager graphics, GameWindow window)
        {
            _graphics = graphics;
            _window = window;
        }
        
        /// <summary>
        /// Updates the screen mode (i.e. windowed, fullscreen, borderless fullscreen) based on keyboard inputs.
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
        /// Toggles fullscreen.
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
        /// Toggles borderless fullscreen.
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
