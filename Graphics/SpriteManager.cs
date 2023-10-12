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

        /// <summary>
        /// Begins a new sprite and text batch with the specified render state.
        /// </summary>
        /// <param name="camera">The camera.</param>
        /// <param name="isTextureFilteringEnabled">Is texture filtering enabled?</param>
        public static void Begin(Camera camera, bool isTextureFilteringEnabled)
        {
            SamplerState samplerState = (isTextureFilteringEnabled) ? SamplerState.LinearClamp : SamplerState.PointClamp;

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

        /// <summary>
        /// Begins a new sprite and text batch with the specified render state.
        /// </summary>
        public static void Begin() => Instance._spriteBatch.Begin();

        /// <summary>
        /// Flushes all batched text and sprites to the screen.
        /// </summary>
        /// <remarks>This command should be called after <see cref="Begin()"/> or <see cref="Begin(Camera, bool)"/> and drawing commands.</remarks>
        public static void End() => Instance._spriteBatch.End();

        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="destinationRectangle">The drawing bounds on screen.</param>
        /// <param name="color">A color mask.</param>
        public static void Draw(Texture2D texture, Rectangle destinationRectangle, Color color)
        {
            Instance._spriteBatch.Draw(texture, destinationRectangle, color);
        }

        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="destinationRectangle">The drawing bounds on screen.</param>
        /// <param name="sourceRectangle">An optional region on the texture which will be rendered. If null - draws full texture.</param>
        /// <param name="color">A color mask.</param>
        public static void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color)
        {
            Instance._spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color);
        }

        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="destinationRectangle">The drawing bounds on screen.</param>
        /// <param name="sourceRectangle">An optional region on the texture which will be rendered. If null - draws full texture.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this sprite.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="effects">Modificators for drawing. Can be combined.</param>
        /// <param name="layerDepth">A depth of the layer of this sprite.</param>
        public static void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            Instance._spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color, rotation, origin, effects, layerDepth);
        }

        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="color">A color mask.</param>
        public static void Draw(Texture2D texture, Vector2 position, Color color)
        {
            Instance._spriteBatch.Draw(texture, position, color);
        }

        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="sourceRectangle">An optional region on the texture which will be rendered. If null - draws full texture.</param>
        /// <param name="color">A color mask.</param>
        public static void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color)
        {
            Instance._spriteBatch.Draw(texture, position, sourceRectangle, color);
        }

        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="sourceRectangle">An optional region on the texture which will be rendered. If null - draws full texture.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this sprite.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="scale">A scaling of this sprite.</param>
        /// <param name="effects">Modificators for drawing. Can be combined.</param>
        /// <param name="layerDepth">A depth of the layer of this sprite.</param>
        public static void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            Instance._spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
        }

        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="sourceRectangle">An optional region on the texture which will be rendered. If null - draws full texture.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this sprite.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="scale">A scaling of this sprite.</param>
        /// <param name="effects">Modificators for drawing. Can be combined.</param>
        /// <param name="layerDepth">A depth of the layer of this sprite.</param>
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
