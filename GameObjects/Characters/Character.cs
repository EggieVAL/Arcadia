using Arcadia.GameObjects.Tiles;
using Arcadia.GameWorld;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Arcadia.GameObjects.Characters
{
    /// <summary>
    /// The <see cref="Character"/> class is an abstraction of a game character. A character can be a player,
    /// an NPC, a mob, or a critter.
    /// </summary>
    public abstract class Character : RenderableObject
    {
        /// <summary>
        /// The world a character is in.
        /// </summary>
        public World World { get; set; }

        /// <summary>
        /// The x-component of velocity of a character.
        /// </summary>
        public float VelocityX { get; set; }

        /// <summary>
        /// The y-component of velocity of a character.
        /// </summary>
        public float VelocityY { get; set; }

        /// <summary>
        /// Is the character falling?
        /// </summary>
        public bool IsFalling => VelocityY > 0;

        /// <summary>
        /// Is the character rising (moving upwards)?
        /// </summary>
        public bool IsRising => VelocityY < 0;

        /// <summary>
        /// Is the character moving left?
        /// </summary>
        public bool IsMovingLeft => VelocityX < 0;

        /// <summary>
        /// Is the character moving right?
        /// </summary>
        public bool IsMovingRight => VelocityX > 0;

        /// <summary>
        /// Is the character moving horizontally?
        /// </summary>
        public bool IsMovingHorizontally => VelocityX != 0;

        /// <summary>
        /// Is the character moving vertically?
        /// </summary>
        public bool IsMovingVertically => VelocityY != 0;

        /// <summary>
        /// Is the character moving?
        /// </summary>
        public bool IsMoving => IsMovingHorizontally || IsMovingVertically;

        /// <summary>
        /// Constructs a character in a world at the given bounds.
        /// </summary>
        /// <param name="texture">The texture of a character.</param>
        /// <param name="bounds">The bounds of a character.</param>
        /// <param name="world">The world a character is in.</param>
        public Character(Texture2D texture, Rectangle bounds, World world) : base(texture, bounds)
        {
            VelocityX = 0;
            VelocityY = 0;
            World = world;
        }

        /// <summary>
        /// Updates a character's collision state.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            float elapsedTime = gameTime.ElapsedGameTime.Milliseconds;
            float currentX = X;
            float currentY = Y;
            float nextX = (X += VelocityX * elapsedTime);
            float nextY = (Y += VelocityY * elapsedTime);

            if (IsCollidingBelow(currentY, nextY, out List<Tile> tilesCollided))
            {
                handleBottomCollision(tilesCollided);
            }
            else if (IsCollidingAbove(currentY, nextY, out tilesCollided))
            {
                handleTopCollision(tilesCollided);
            }

            if (IsCollidingToTheLeft(currentX, nextX, out tilesCollided))
            {
                handleLeftCollision(tilesCollided);
            }
            else if (IsCollidingToTheRight(currentX, nextX, out tilesCollided))
            {
                handleRightCollision(tilesCollided);
            }
        }
        
        /// <summary>
        /// Is a character colliding with any tiles above it?
        /// </summary>
        /// <param name="currentY">The current y-coordinate of a character.</param>
        /// <param name="nextY">The next y-coordinate of a character after moving.</param>
        /// <param name="tilesCollided">The tiles a character collided with.</param>
        /// <returns>Returns true if a character collides with at least one tile.</returns>
        public bool IsCollidingAbove(float currentY, float nextY, out List<Tile> tilesCollided)
        {
            tilesCollided = new List<Tile>();
            if (IsFalling || !IsMovingVertically)
            {
                return false;
            }

            int lowerTileX = Grid.GetPosition(X);
            int upperTileX = Grid.GetPosition(X + Width - 0.01f);
            int lowerTileY = Grid.GetPosition(currentY);
            int upperTileY = MathHelper.Clamp(Grid.GetPosition(nextY), 0, World.Height);

            for (int tileY = lowerTileY; tileY >= upperTileY; --tileY)
            {
                for (int tileX = lowerTileX; tileX <= upperTileX; ++tileX)
                {
                    Tile tile = World[tileX, tileY];
                    if (tile is not null)
                    {
                        tilesCollided.Add(tile);
                    }
                }
                if (tilesCollided.Count > 0)
                {
                    return true;
                }
            }

            if (nextY < 0)
            {
                Y = 0;
                VelocityY = 0;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Is a character colliding with any tiles below it?
        /// </summary>
        /// <param name="currentY">The current y-coordinate of a character.</param>
        /// <param name="nextY">The next y-coordinate of a character after moving.</param>
        /// <param name="tilesCollided">The tiles a character collided with.</param>
        /// <returns>Returns true if a character collides with at least one tile.</returns>
        public bool IsCollidingBelow(float currentY, float nextY, out List<Tile> tilesCollided)
        {
            tilesCollided = new List<Tile>();
            if (IsRising || !IsMovingVertically)
            {
                return false;
            }

            currentY += Height;
            nextY += Height;

            int lowerTileX = Grid.GetPosition(X);
            int upperTileX = Grid.GetPosition(X + Width - 0.01f);
            int lowerTileY = Grid.GetPosition(currentY);
            int upperTileY = MathHelper.Clamp(Grid.GetPosition(nextY), 0, World.Height);

            for (int tileY = lowerTileY; tileY <= upperTileY; ++tileY)
            {
                for (int tileX = lowerTileX; tileX <= upperTileX; ++tileX)
                {
                    Tile tile = World[tileX, tileY];
                    if (tile is not null)
                    {
                        tilesCollided.Add(tile);
                    }
                }
                if (tilesCollided.Count > 0)
                {
                    return true;
                }
            }

            if (nextY > World.HeightInUnits)
            {
                Y = World.HeightInUnits - Height;
                VelocityY = 0;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Is a character colliding with any tiles to the left of it?
        /// </summary>
        /// <param name="currentX">The current x-coordinate of a character.</param>
        /// <param name="nextX">The next x-coordinate of a character after moving.</param>
        /// <param name="tilesCollided">The tiles a character collided with.</param>
        /// <returns>Returns true if a character collides with at least one tile.</returns>
        public bool IsCollidingToTheLeft(float currentX, float nextX, out List<Tile> tilesCollided)
        {
            tilesCollided = new List<Tile>();
            if (IsMovingRight || !IsMovingHorizontally)
            {
                return false;
            }

            int lowerTileX = Grid.GetPosition(currentX);
            int upperTileX = MathHelper.Clamp(Grid.GetPosition(nextX), 0, World.Width);
            int lowerTileY = Grid.GetPosition(Y);
            int upperTileY = Grid.GetPosition(Y + Height - 0.01f);

            for (int tileX = lowerTileX; tileX >= upperTileX; --tileX)
            {
                for (int tileY = lowerTileY; tileY <= upperTileY; ++tileY)
                {
                    Tile tile = World[tileX, tileY];
                    if (tile is not null)
                    {
                        tilesCollided.Add(tile);
                    }
                }
                if (tilesCollided.Count > 0)
                {
                    return true;
                }
            }

            if (nextX < 0)
            {
                X = 0;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Is a character colliding with any tiles to the right of it?
        /// </summary>
        /// <param name="currentX">The current x-coordinate of a character.</param>
        /// <param name="nextX">The next x-coordinate of a character after moving.</param>
        /// <param name="tilesCollided">The tiles a character collided with.</param>
        /// <returns>Returns true if a character collides with at least one tile.</returns>
        public bool IsCollidingToTheRight(float currentX, float nextX, out List<Tile> tilesCollided)
        {
            tilesCollided = new List<Tile>();
            if (IsMovingLeft || !IsMovingHorizontally)
            {
                return false;
            }

            currentX += Width;
            nextX += Width;

            int lowerTileX = Grid.GetPosition(currentX);
            int upperTileX = MathHelper.Clamp(Grid.GetPosition(nextX), 0, World.Width);
            int lowerTileY = Grid.GetPosition(Y);
            int upperTileY = Grid.GetPosition(Y + Height - 0.01f);

            for (int tileX = lowerTileX; tileX <= upperTileX; ++tileX)
            {
                for (int tileY = lowerTileY; tileY <= upperTileY; ++tileY)
                {
                    Tile tile = World[tileX, tileY];
                    if (tile is not null)
                    {
                        tilesCollided.Add(tile);
                    }
                }
                if (tilesCollided.Count > 0)
                {
                    return true;
                }
            }

            if (nextX > World.WidthInUnits)
            {
                X = World.WidthInUnits - Width;
                return true;
            }
            return false;
        }

        private void handleTopCollision(List<Tile> tilesCollided)
        {
            if (tilesCollided.Count == 0)
            {
                return;
            }

            VelocityY = 0.0000000001f;
            Y = tilesCollided[0].Y + Grid.Size;
        }

        private void handleBottomCollision(List<Tile> tilesCollided)
        {
            if (tilesCollided.Count == 0)
            {
                return;
            }

            VelocityY = 0;
            Y = tilesCollided[0].Y - Height;
        }

        private void handleLeftCollision(List<Tile> tilesCollided)
        {
            if (tilesCollided.Count == 0)
            {
                return;
            }

            float positionY = Y + Height;
            float lowerBoundY = tilesCollided[0].Y;
            float upperBoundY = lowerBoundY + Grid.Size * 0.6f;
            float tileY = lowerBoundY + Grid.Size;

            if (positionY >= lowerBoundY && positionY <= upperBoundY)
            {
                Y = lowerBoundY - Height;
                VelocityY = 0;
            }
            else if (VelocityY == 0 && positionY == tileY)
            {
                Y -= Grid.Size;
            }
            else
            {
                X = tilesCollided[0].X + Grid.Size;
                VelocityX = 0;
            }
        }

        private void handleRightCollision(List<Tile> tilesCollided)
        {
            if (tilesCollided.Count == 0)
            {
                return;
            }

            float positionY = Y + Height;
            float lowerBoundY = tilesCollided[0].Y;
            float upperBoundY = lowerBoundY + Grid.Size * 0.6f;
            float tileY = lowerBoundY + Grid.Size;

            if (positionY >= lowerBoundY && positionY <= upperBoundY)
            {
                Y = lowerBoundY - Height;
                VelocityY = 0;
            }
            else if (VelocityY == 0 && positionY == tileY)
            {
                Y -= Grid.Size;
            }
            else
            {
                X = tilesCollided[0].X - Width;
                VelocityX = 0;
            }
        }
    }
}
