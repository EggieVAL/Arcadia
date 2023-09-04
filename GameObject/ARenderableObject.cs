using Arcadia.GameWorld;
using Arcadia.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arcadia.GameObject
{
    /// <summary>
    ///     The <c>ARenderableObject</c> is an abstraction of a renderable game
    ///     object. A renderable object has some texture and bounds.
    /// </summary>
    public abstract class ARenderableObject : AGameObject
    {
        /// <summary>
        ///     The texture of a game object.
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        ///     The x-coordinate of a game object in units.
        /// </summary>
        public float UnitX { get; set; }

        /// <summary>
        ///     The y-coordinate of a game object in units.
        /// </summary>
        public float UnitY { get; set; }

        /// <summary>
        ///     The width of a game object in units.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        ///     The height of a game object in units.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        ///     The bounds of a game object (i.e. x- and y-coordinate, width,
        ///     and height).
        /// </summary>
        public Rectangle Bounds => new Rectangle((int) MathF.Round(UnitX),
            (int) MathF.Round(UnitY), Width, Height);

        /// <summary>
        ///     The position of a game object in units.
        /// </summary>
        public Vector2 UnitPos => new Vector2(UnitX, UnitY);

        /// <summary>
        ///     The x-coordinate of a game object in the grid world.
        /// </summary>
        public int X => Grid.GetPosX(UnitX);

        /// <summary>
        ///     The y-coordinate of a game object in the grid world.
        /// </summary>
        public int Y => Grid.GetPosY(UnitY);

        /// <summary>
        ///     The position of a game object in the grid world.
        /// </summary>
        public Vector2 Pos => new Vector2(X, Y);

        /// <summary>
        ///     Constructs a game object with some texture and bounds.
        /// </summary>
        /// <param name="texture">The texture of a game object.</param>
        /// <param name="bounds">The bounds of a game object.</param>
        public ARenderableObject(Texture2D texture, Rectangle bounds)
        {
            Texture = texture;
            UnitX = bounds.X;
            UnitY = bounds.Y;
            Width = bounds.Width;
            Height = bounds.Height;
        }

        /// <summary>
        ///     A standard <c>Draw</c> method for renderable game objects.
        ///     It draws the texture of the game object at its specified
        ///     bounds.
        /// </summary>
        /// <param name="gt">The time state of the game.</param>
        /// <param name="manager">The sprite manager.</param>
        public virtual void Draw(GameTime gt, SpriteManager manager)
        {
            if (Texture is not null)
            {
                manager.Draw(Texture, Bounds, Color.White);
            }
        }
    }
}
