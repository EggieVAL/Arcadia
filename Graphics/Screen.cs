using Arcadia.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arcadia.Graphics
{
    public sealed class Screen
    {
        public bool IsFullScreen { get; private set; }

        public bool IsBorderless { get; private set; }

        public Screen(GraphicsDeviceManager graphics, GameWindow window)
        {
            _graphics = graphics;
            _window = window;
        }

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
