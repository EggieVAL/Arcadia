using Arcadia.GameWorld;
using Arcadia.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arcadia.GameObject
{
    /// <summary>
    /// The <c>ARenderableObject</c> is an abstraction of a renderable game object. A renderable
    /// object has some texture and bounds.
    /// </summary>
    public abstract class RenderableObject : GameObject
    {
        /// <summary>
        /// The texture of a game object.
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// The x-coordinate of a game object in units.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// The y-coordinate of a game object in units.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// The width of a game object in units.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The height of a game object in units.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The bounds of a game object (i.e. x- and y-coordinate, width, and height).
        /// </summary>
        public Rectangle Bounds => new Rectangle((int) MathF.Round(X),
                                                 (int) MathF.Round(Y),
                                                 Width, Height);

        /// <summary>
        /// The position of a game object in units.
        /// </summary>
        public Vector2 Position => new Vector2(X, Y);

        /// <summary>
        /// The x-coordinate of a game object in the grid space.
        /// </summary>
        public int TileX => Grid.GetTilePosition(X);

        /// <summary>
        /// The y-coordinate of a game object in the grid space.
        /// </summary>
        public int TileY => Grid.GetTilePosition(Y);

        /// <summary>
        /// The position of a game object in the grid space.
        /// </summary>
        public Vector2 TilePosition => new Vector2(TileX, TileY);

        /// <summary>
        /// Constructs a game object with some texture and bounds.
        /// </summary>
        /// <param name="texture">The texture of a game object.</param>
        /// <param name="bounds">The bounds of a game object.</param>
        public RenderableObject(Texture2D texture, Rectangle bounds)
        {
            Texture = texture;
            X = bounds.X;
            Y = bounds.Y;
            Width = bounds.Width;
            Height = bounds.Height;
        }

        /// <summary>
        /// The update method is called multiple times a second, updating the state of a character.
        /// </summary>
        /// <param name="gameTime">The time state of the game.</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// A standard draw method for renderable game objects. It draws the texture of the game
        /// object at its specified bounds.
        /// </summary>
        /// <param name="gameTime">The time state of the game.</param>
        /// <param name="spriteManager">The sprite manager.</param>
        public virtual void Draw(GameTime gameTime)
        {
            if (Texture is not null)
            {
                SpriteManager.Draw(Texture, Bounds, Color.White);
            }
        }
    }
}
