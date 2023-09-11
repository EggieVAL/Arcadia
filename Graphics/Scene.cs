using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arcadia.Graphics
{
    /// <summary>
    /// The <c>Scene</c> class is a representation of a scene in a world. Specifically, it
    /// captures all the game objects that are renderable in a specific location.
    /// </summary>
    public sealed class Scene : IDisposable
    {
        /// <summary>
        /// The minimum resolution the scene will capture.
        /// </summary>
        public const int MinimumResolution = 64;

        /// <summary>
        /// The maximum resolution the scene will capture.
        /// </summary>
        public const int MaximumResolution = 4096;

        /// <summary>
        /// The render target width.
        /// </summary>
        public int Width => _renderTarget.Width;

        /// <summary>
        /// The render target height.
        /// </summary>
        public int Height => _renderTarget.Height;

        /// <summary>
        /// Constructs a scene in some resolution for some game.
        /// </summary>
        /// <param name="game">A game the scene will capture.</param>
        /// <param name="width">The resolution width.</param>
        /// <param name="height">The resolution height.</param>
        public Scene(Game game, int width, int height)
        {
            width = MathHelper.Clamp(width, MinimumResolution, MaximumResolution);
            height = MathHelper.Clamp(height, MinimumResolution, MaximumResolution);

            _game = game;
            _renderTarget = new RenderTarget2D(game.GraphicsDevice, width, height);
            _disposed = false;
        }

        /// <summary>
        /// Constructs a scene for some game using the resolution of the current display
        /// monitor.
        /// </summary>
        /// <param name="game">A game the scene will capture.</param>
        public Scene(Game game) : this(game, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                                             GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)
        { }

        /// <summary>
        /// Enables render targeting.
        /// </summary>
        public void EnableRenderTargeting()
        {
            _game.GraphicsDevice.SetRenderTarget(_renderTarget);
        }

        /// <summary>
        /// Disables render targeting.
        /// </summary>
        public void DisableRenderTargeting()
        {
            _game.GraphicsDevice.SetRenderTarget(null);
        }

        /// <summary>
        /// Displays all the objects within the render target.
        /// </summary>
        /// <param name="manager">The sprite manager.</param>
        /// <param name="isTextureFilteringEnabled">
        /// Whether texture filtering is enabled.
        /// </param>
        public void Display(bool isTextureFilteringEnabled)
        {
            Rectangle target = GetRenderTargetDestination();
            SpriteManager.Begin(null, isTextureFilteringEnabled);
            SpriteManager.Draw(_renderTarget, target, Color.White);
            SpriteManager.End();
        }

        /// <summary>
        /// Gets the render target destination based on the application's and scene's aspect ratios.
        /// </summary>
        /// <returns>Returns the render target destination.</returns>
        public Rectangle GetRenderTargetDestination()
        {
            Rectangle backBuffer = _game.GraphicsDevice.PresentationParameters.Bounds;
            int appWidth = backBuffer.Width;
            int appHeight = backBuffer.Height;
            int sceneWidth = _renderTarget.Width;
            int sceneHeight = _renderTarget.Height;

            float appAspectRatio = (float) appWidth / appHeight;
            float sceneAspectRatio = (float) sceneWidth / sceneHeight;

            float x = 0f;
            float y = 0f;
            float width = appWidth;
            float height = appHeight;

            if (appAspectRatio > sceneAspectRatio)
            {
                width = appHeight * sceneAspectRatio;
                x = (appWidth - width) / 2f;
            }
            else if (appAspectRatio < sceneAspectRatio)
            {
                height = width / sceneAspectRatio;
                y = (appHeight - height) / 2f;
            }
            return new Rectangle((int) x, (int) y, (int) width, (int) height);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
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
