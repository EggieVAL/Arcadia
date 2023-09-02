using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arcadia.Graphics
{
    /// <summary>
    ///     The <c>Scene</c> class is a representation of a scene in a world.
    ///     Specifically, it captures all the game objects that are renderable
    ///     in a specific location.
    /// </summary>
    public sealed class Scene : IDisposable
    {
        /// <summary>
        ///     The minimum resolution the scene will capture.
        /// </summary>
        public const int MinResolution = 64;

        /// <summary>
        ///     The maximum resolution the scene will capture.
        /// </summary>
        public const int MaxResolution = 4096;

        /// <summary>
        ///     The render target width.
        /// </summary>
        public int Width
        {
            get => _renderTarget.Width;
        }

        /// <summary>
        ///     The render target height.
        /// </summary>
        public int Height
        {
            get => _renderTarget.Height;
        }

        /// <summary>
        ///     Constructs a scene in some resolution for some game.
        /// </summary>
        /// <param name="game">A game a scene will capture.</param>
        /// <param name="width">The resolution width.</param>
        /// <param name="height">The resolution height.</param>
        public Scene(Game game, int width, int height)
        {
            width = MathHelper.Clamp(width, MinResolution, MaxResolution);
            height = MathHelper.Clamp(height, MinResolution, MaxResolution);

            _game = game;
            _renderTarget = new RenderTarget2D(game.GraphicsDevice, width, height);
            _disposed = false;
        }

        /// <summary>
        ///     Constructs a scene for some game using the resolution of the
        ///     current display monitor.
        /// </summary>
        /// <param name="game">A game a scene will capture.</param>
        public Scene(Game game) : this(game,
            GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
            GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height) { }

        /// <summary>
        ///     Enables render targeting.
        /// </summary>
        public void EnableRenderTargeting()
        {
            _game.GraphicsDevice.SetRenderTarget(_renderTarget);
        }

        /// <summary>
        ///     Disables render targeting.
        /// </summary>
        public void DisableRenderTargeting()
        {
            _game.GraphicsDevice.SetRenderTarget(null);
        }

        /// <summary>
        ///     Displays all objects within the render target.
        /// </summary>
        /// <param name="manager">The sprite manager.</param>
        /// <param name="isTextureFilteringEnabled">
        ///     Whether texture filtering is enabled.
        /// </param>
        public void Display(SpriteManager manager, bool isTextureFilteringEnabled)
        {
            Rectangle target = GetRenderTargetDestination();
            manager.Begin(null, isTextureFilteringEnabled);
            manager.Draw(_renderTarget, target, Color.White);
            manager.End();
        }

        /// <summary>
        ///     Gets the render target destination based on the application's
        ///     and scene's aspect ratios.
        /// </summary>
        /// <returns></returns>
        public Rectangle GetRenderTargetDestination()
        {
            Rectangle backBuffer = _game.GraphicsDevice.PresentationParameters.Bounds;
            int appWidth = backBuffer.Width;
            int appHeight = backBuffer.Height;
            int sceneWidth = _renderTarget.Width;
            int sceneHeight = _renderTarget.Height;

            float appRatio = (float) appWidth / appHeight;
            float sceneRatio = (float) sceneWidth / sceneHeight;

            float x = 0f;
            float y = 0f;
            float width = appWidth;
            float height = appHeight;

            if (appRatio > sceneRatio)
            {
                width = appHeight * sceneRatio;
                x = (appWidth - width) / 2f;
            }
            else if (appRatio < sceneRatio)
            {
                height = width / sceneRatio;
                y = (appHeight - height) / 2f;
            }
            return new Rectangle((int) x, (int) y, (int) width, (int) height);
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing,
        ///     releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                _renderTarget?.Dispose();
                _disposed = true;
            }
        }

        private readonly Game _game;

        private RenderTarget2D _renderTarget;

        private bool _disposed;
    }
}
