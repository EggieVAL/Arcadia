using Arcadia.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arcadia.Graphics
{
    /// <summary>
    /// The <see cref="SpriteManager"/> is a singleton class that manages sprites.
    /// </summary>
    public sealed class SpriteManager : IDisposable
    {
        /// <summary>
        /// The single instance of this class.
        /// </summary>
        public static SpriteManager Instance { get; private set; }

        /// <summary>
        /// Creates a single instance of this class if it's not already created.
        /// </summary>
        /// <param name="game"></param>
        public static void CreateInstance(Game game)
        {
            Instance ??= new SpriteManager(game);
        }

        public static void Begin(Camera camera, bool isTextureFilteringEnabled)
        {
            SamplerState samplerState = (isTextureFilteringEnabled)
                ? SamplerState.LinearClamp : SamplerState.PointClamp;

            if (camera is null)
            {
                Viewport viewport = Instance._game.GraphicsDevice.Viewport;
                Instance._basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0, viewport.Width, 0, viewport.Height, 0f, 1f);
                Instance._basicEffect.View = Matrix.Identity;
                Instance._basicEffect.World = Matrix.Identity;
            }
            else
            {
                camera.UpdateMatrices();
                Instance._basicEffect.Projection = camera.Projection;
                Instance._basicEffect.View = camera.View;
                Instance._basicEffect.World = camera.World;
            }

            Instance._spriteBatch.Begin(blendState: BlendState.AlphaBlend, samplerState: samplerState, rasterizerState: RasterizerState.CullNone, effect: Instance._basicEffect);
        }

        public static void Begin() => Instance._spriteBatch.Begin();

        public static void End() => Instance._spriteBatch.End();

        public static void Draw(Texture2D texture, Rectangle destinationRectangle, Color color)
        {
            Instance._spriteBatch.Draw(texture, destinationRectangle, color);
        }

        public static void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color)
        {
            Instance._spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color);
        }

        public static void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            Instance._spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color, rotation, origin, effects, layerDepth);
        }

        public static void Draw(Texture2D texture, Vector2 position, Color color)
        {
            Instance._spriteBatch.Draw(texture, position, color);
        }

        public static void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color)
        {
            Instance._spriteBatch.Draw(texture, position, sourceRectangle, color);
        }

        public static void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            Instance._spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
        }

        public static void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            Instance._spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _spriteBatch?.Dispose();
                _basicEffect?.Dispose();
                _disposed = true;
            }
        }

        private SpriteManager(Game game)
        {
            _game = game;
            _spriteBatch = new SpriteBatch(game.GraphicsDevice);
            _basicEffect = new BasicEffect(game.GraphicsDevice)
            {
                FogEnabled = false,
                LightingEnabled = false,
                TextureEnabled = true,
                VertexColorEnabled = false,
                Projection = Matrix.Identity,
                View = Matrix.Identity,
                World = Matrix.Identity
            };
            _disposed = false;
        }

        private readonly Game _game;

        private readonly SpriteBatch _spriteBatch;

        private readonly BasicEffect _basicEffect;

        private bool _disposed;
    }
}
