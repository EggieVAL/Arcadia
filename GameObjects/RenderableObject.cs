using Arcadia.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arcadia.GameObjects
{
    /// <summary>
    /// The <see cref="RenderableObject"/> class is an abstraction of a renderable object. A renderable
    /// object is a game object that can be drawn on the screen.
    /// </summary>
    /// <seealso cref="GameObject"/>
    public abstract class RenderableObject : GameObject
    {
        /// <summary>
        /// The texture of a renderable object.
        /// </summary>
        public Texture2D[] Textures { get; set; }

        /// <summary>
        /// The x-coordinate of a renderable object in units.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// The y-coordinate of a renderable object in units.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// The width of a renderable object in units; this is not part of its collider.
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// The height of a renderable object in units; this is not part of its collider.
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// The bounds of a renderable object; this is not its collider.
        /// </summary>
        public Rectangle Bounds => new((int) MathF.Round(X), (int) MathF.Round(Y), (int) Width, (int) Height);

        /// <summary>
        /// Constructs a renderable object with a texture.
        /// </summary>
        /// <param name="texture">The texture of a renderable object.</param>
        /// <param name="bounds">The bounds of a renderable object; this is not its collider.</param>
        public RenderableObject(Texture2D[] texture, Rectangle bounds)
        {
            Textures = texture;
            X = bounds.X;
            Y = bounds.Y;
            Width = bounds.Width;
            Height = bounds.Height;
        }

        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// This is a standard draw method for a renderable object.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public virtual void Draw(GameTime gameTime)
        {
            if (Textures[0] is not null)
            {
                SpriteManager.Draw(Textures[0], Bounds, Color.White);
            }

        }
    }
}
