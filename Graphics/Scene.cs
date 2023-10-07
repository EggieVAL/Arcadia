using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arcadia.Graphics
{
    /// <summary>
    /// The <see cref="Scene"/> class is a representation of a scene.
    /// </summary>
    public sealed class Scene : IDisposable
    {
        /// <summary>
        /// The minimum resolution a scene can be.
        /// </summary>
        public const int MinimumResolution = 64;

        /// <summary>
        /// The maximum resolution a scene can be.
        /// </summary>
        public const int MaximumResolution = 4096;

        /// <summary>
        /// The width of the scene.
        /// </summary>
        public int Width => _renderTarget.Width;

        /// <summary>
        /// The height of the scene.
        /// </summary>
        public int Height => _renderTarget.Height;

        /// <summary>
        /// Constructs a scene of some width and height for a game.
        /// </summary>
        /// <param name="game">The game a scene will capture.</param>
        /// <param name="width">The width of a scene.</param>
        /// <param name="height">The height of a scene.</param>
        public Scene(Game game, int width, int height)
        {
            width = MathHelper.Clamp(width, MinimumResolution, MaximumResolution);
            height = MathHelper.Clamp(height, MinimumResolution, MaximumResolution);

            _game = game;
            _renderTarget = new RenderTarget2D(game.GraphicsDevice, width, height);
            _disposed = false;
        }

        /// <summary>
        /// Constructs a scene, using the current display monitor's resolution, for a game.
        /// </summary>
        /// <param name="game">The game a scene will capture.</param>
        public Scene(Game game) : this(game, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height) { }

        /// <summary>
        /// Enables render targeting for a scene.
        /// </summary>
        public void EnableRenderTargeting()
        {
            _game.GraphicsDevice.SetRenderTarget(_renderTarget);
        }

        /// <summary>
        /// Disables render targeting for a scene.
        /// </summary>
        public void DisableRenderTargeting()
        {
            _game.GraphicsDevice.SetRenderTarget(null);
        }

        /// <summary>
        /// Captures everything in a scene and displays it.
        /// </summary>
        /// <param name="isTextureFilteringEnabled"></param>
        public void Display(bool isTextureFilteringEnabled)
        {
            Rectangle target = GetRenderTargetDestination();
            SpriteManager.Begin(null, isTextureFilteringEnabled);
            SpriteManager.Draw(_renderTarget, target, Color.White);
            SpriteManager.End();
        }

        /// <summary>
        /// Gets the render target destination of a scene.
        /// </summary>
        /// <returns></returns>
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

        public void Dispose()
        {
            if (!_disposed)
            {
                _renderTarget?.Dispose();
                _disposed = true;
            }
        }

        private readonly Game _game;

        private readonly RenderTarget2D _renderTarget;

        private bool _disposed;
    }
}
