using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arcadia.Graphics
{
    public sealed class Scene : IDisposable
    {
        public const int MinimumResolution = 64;

        public const int MaximumResolution = 4096;

        public int Width => _renderTarget.Width;

        public int Height => _renderTarget.Height;

        public Scene(Game game, int width, int height)
        {
            width = MathHelper.Clamp(width, MinimumResolution, MaximumResolution);
            height = MathHelper.Clamp(height, MinimumResolution, MaximumResolution);

            _game = game;
            _renderTarget = new RenderTarget2D(game.GraphicsDevice, width, height);
            _disposed = false;
        }

        public Scene(Game game) : this(game, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height) { }

        public void EnableRenderTargeting()
        {
            _game.GraphicsDevice.SetRenderTarget(_renderTarget);
        }

        public void DisableRenderTargeting()
        {
            _game.GraphicsDevice.SetRenderTarget(null);
        }

        public void Display(bool isTextureFilteringEnabled)
        {
            Rectangle target = GetRenderTargetDestination();
            SpriteManager.Begin(null, isTextureFilteringEnabled);
            SpriteManager.Draw(_renderTarget, target, Color.White);
            SpriteManager.End();
        }

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
