using Arcadia.GameObject;
using Arcadia.GameObject.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arcadia.Graphics
{
    /// <summary>
    ///     The <c>SpriteManager</c> class handles sprite rendering.
    /// </summary>
    public sealed class SpriteManager : IDisposable
    {
        /// <summary>
        ///     Constructs a sprite manager for some game.
        /// </summary>
        /// <param name="game">A game.</param>
        public SpriteManager(Game game)
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

        /// <summary>
        ///     Begins a new sprite and text batch with the specified
        ///     render state.
        /// </summary>
        /// <param name="camera">
        ///     The camera where the sprites are being drawn.
        /// </param>
        /// <param name="isTextureFilteringEnabled">
        ///     Whether texture filtering is enabled.
        /// </param>
        public void Begin(Camera camera, bool isTextureFilteringEnabled)
        {
            SamplerState samplerState = (isTextureFilteringEnabled)
                ? SamplerState.LinearClamp : SamplerState.PointClamp;

            if (camera is null)
            {
                Viewport viewport = _game.GraphicsDevice.Viewport;
                _basicEffect.Projection = Matrix.CreateOrthographicOffCenter(
                    0, viewport.Width, 0, viewport.Height, 0f, 1f);
                _basicEffect.View = Matrix.Identity;
                _basicEffect.World = Matrix.Identity;
            }
            else
            {
                camera.UpdateMatrices();
                _basicEffect.Projection = camera.Projection;
                _basicEffect.View = camera.View;
                _basicEffect.World = camera.World;
            }

            _spriteBatch.Begin(blendState: BlendState.AlphaBlend,
                               samplerState: samplerState,
                               rasterizerState: RasterizerState.CullNone,
                               effect: _basicEffect);
        }

        /// <summary>
        ///     Begins a new sprite and text batch with the default
        ///     render state.
        /// </summary>
        public void Begin() => _spriteBatch.Begin();

        /// <summary>
        ///     Flushes all batched text and sprites to the screen.
        /// </summary>
        public void End() => _spriteBatch.End();

        /// <summary>
        ///     Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="destinationRectangle">The drawing bounds on screen.</param>
        /// <param name="color">A color mask.</param>
        public void Draw(Texture2D texture, Rectangle destinationRectangle, Color color)
        {
            _spriteBatch.Draw(texture, destinationRectangle, color);
        }

        /// <summary>
        ///     Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="destinationRectangle">The drawing bounds on screen.</param>
        /// <param name="sourceRectangle">
        ///     An optional region on the texture which will be rendered.
        ///     If null - draws full texture.
        /// </param>
        /// <param name="color">A color mask.</param>
        public void Draw(Texture2D texture, Rectangle destinationRectangle,
                         Rectangle? sourceRectangle, Color color)
        {
            _spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color);
        }

        /// <summary>
        ///     Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="destinationRectangle">The drawing bounds on screen.</param>
        /// <param name="sourceRectangle">
        ///     An optional region on the texture which will be rendered.
        ///     If null - draws full texture.
        /// </param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this sprite.</param>
        /// <param name="origin">Center of rotation. (0, 0) by default.</param>
        /// <param name="effects">Modificators for drawing. Can be combined.</param>
        /// <param name="layerDepth">A depth of the layer of this sprite.</param>
        public void Draw(Texture2D texture, Rectangle destinationRectangle,
                         Rectangle? sourceRectangle, Color color, float rotation,
                         Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            _spriteBatch.Draw(texture, destinationRectangle, sourceRectangle,
                color, rotation, origin, effects, layerDepth);
        }

        /// <summary>
        ///     Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="color">A color mask.</param>
        public void Draw(Texture2D texture, Vector2 position, Color color)
        {
            _spriteBatch.Draw(texture, position, color);
        }

        /// <summary>
        ///     Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="sourceRectangle">
        ///     An optional region on the texture which will be rendered.
        ///     If null - draws full texture.
        /// </param>
        /// <param name="color">A color mask.</param>
        public void Draw(Texture2D texture, Vector2 position,
                         Rectangle? sourceRectangle, Color color)
        {
            _spriteBatch.Draw(texture, position, sourceRectangle, color);
        }

        /// <summary>
        ///     Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="sourceRectangle">
        ///     An optional region on the texture which will be rendered.
        ///     If null - draws full texture.
        /// </param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this sprite.</param>
        /// <param name="origin">Center of the rotation. (0, 0) by default.</param>
        /// <param name="scale">A scaling of this sprite.</param>
        /// <param name="effects">Modificators for drawing. Can be combined.</param>
        /// <param name="layerDepth">A depth of the layer of this sprite.</param>
        public void Draw(Texture2D texture, Vector2 position,
                         Rectangle? sourceRectangle, Color color,
                         float rotation, Vector2 origin, Vector2 scale,
                         SpriteEffects effects, float layerDepth)
        {
            _spriteBatch.Draw(texture, position, sourceRectangle, color,
                rotation, origin, scale, effects, layerDepth);
        }

        /// <summary>
        ///     Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="sourceRectangle">
        ///     An optional region on the texture which will be rendered.
        ///     If null - draws full texture.
        /// </param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this sprite.</param>
        /// <param name="origin">Center of rotation. (0, 0) by default.</param>
        /// <param name="scale">A scaling of this sprite.</param>
        /// <param name="effects">Modificators for drawing. Can be combined.</param>
        /// <param name="layerDepth">A depth of the layer of this sprite.</param>
        public void Draw(Texture2D texture, Vector2 position,
                         Rectangle? sourceRectangle, Color color,
                         float rotation, Vector2 origin, float scale,
                         SpriteEffects effects, float layerDepth)
        {
            _spriteBatch.Draw(texture, position, sourceRectangle, color,
                rotation, origin, scale, effects, layerDepth);
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing,
        ///     releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                _spriteBatch?.Dispose();
                _basicEffect?.Dispose();
                _disposed = true;
            }
        }

        private readonly Game _game;

        private SpriteBatch _spriteBatch;

        private readonly BasicEffect _basicEffect;

        private bool _disposed;
    }
}
