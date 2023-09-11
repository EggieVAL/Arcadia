using Arcadia.GameObject.Tiles;
using Arcadia.GameWorld;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Arcadia.GameObject.Characters
{
    /// <summary>
    /// The <c>Character</c> class is an abstraction of a game character. A game character
    /// interacts in the environment they live in, from moving in the world to changing
    /// their surroundings.
    /// </summary>
    public abstract class Character : RenderableObject
    {
        /// <summary>
        /// The world a character is in.
        /// </summary>
        public World World { get; set; }

        /// <summary>
        /// The x-component of a character's velocity.
        /// </summary>
        public float VelocityX { get; set; }

        /// <summary>
        /// The y-component of a character's velocity.
        /// </summary>
        public float VelocityY { get; set; }
        
        /// <summary>
        /// The velocity of a character.
        /// </summary>
        public Vector2 Velocity => new Vector2(VelocityX, VelocityY);

        /// <summary>
        /// Constructs a character with a texture in some bounding box in some world.
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
        /// <param name="gameTime">The time state of the game.</param>
        public override void Update(GameTime gameTime)
        {
            float elapsedTime = gameTime.ElapsedGameTime.Milliseconds;
            float previousX = X;
            float previousY = Y;

            Y += VelocityY * elapsedTime;

            if (IsCollidingBelow(previousY, Y, out List<Tile> tilesCollided))
            {
                handleBottomCollision(tilesCollided);
            }
            else if (IsCollidingAbove(previousY, Y, out tilesCollided))
            {
                handleTopCollision(tilesCollided);
            }

            X += VelocityX * elapsedTime;
            if (IsCollidingLeftSide(previousX, X, out tilesCollided))
            {
                handleLeftCollision(tilesCollided);
            }
            else if (IsCollidingRightSide(previousX, X, out tilesCollided))
            {
                handleRightCollision(tilesCollided);
            }
        }
        
        /// <summary>
        /// Determines whether a character will collide with one or more tiles in a frame.
        /// Specifically, the method checks the tiles above the character.
        /// </summary>
        /// <param name="currentY">Current y-coordinate in units.</param>
        /// <param name="nextY">Next y-coordinate in units.</param>
        /// <param name="tilesCollided">
        /// A list of tiles a character collided; specifically tiles above the character.
        /// </param>
        /// <returns>Returns true if a character collides with at least one tile.</returns>
        public bool IsCollidingAbove(float currentY, float nextY, out List<Tile> tilesCollided)
        {
            tilesCollided = new List<Tile>();
            if (VelocityY >= 0)
            {
                return false;
            }

            int lowerTileX = Grid.GetTilePosition(X);
            int upperTileX = Grid.GetTilePosition(X + Width - 0.01f);
            int lowerTileY = Grid.GetTilePosition(currentY);
            int upperTileY = MathHelper.Clamp(Grid.GetTilePosition(nextY), 0, (int) World.Height);

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
        /// Determines whether a character will collide with one or more tiles in a frame.
        /// Specifically, the method checks the tiles below the character.
        /// </summary>
        /// <param name="currentY">Current y-coordinate in units.</param>
        /// <param name="nextY">Next y-coordinate in units.</param>
        /// <param name="tilesCollided">
        /// A list of tiles a character collided; specifically tiles below the character.
        /// </param>
        /// <returns>Returns true if a character collides with at least one tile.</returns>
        public bool IsCollidingBelow(float currentY, float nextY, out List<Tile> tilesCollided)
        {
            tilesCollided = new List<Tile>();
            if (VelocityY <= 0)
            {
                return false;
            }

            currentY += Height;
            nextY += Height;

            int lowerTileX = Grid.GetTilePosition(X);
            int upperTileX = Grid.GetTilePosition(X + Width - 0.01f);
            int lowerTileY = Grid.GetTilePosition(currentY);
            int upperTileY = MathHelper.Clamp(Grid.GetTilePosition(nextY), 0, (int)World.Height);

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

            if (nextY > World.Height)
            {
                Y = World.Height - Height;
                VelocityY = 0;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether a character will collide with one or more tiles in a frame.
        /// Specifically, the method checks the tiles left of the character.
        /// </summary>
        /// <param name="currentX">Current x-coordinate in units.</param>
        /// <param name="nextX">Next x-coordinate in units.</param>
        /// <param name="tilesCollided">
        /// A list of tiles a character collided; specifically tiles left of the character.
        /// </param>
        /// <returns>Returns true if a character collides with at least one tile.</returns>
        public bool IsCollidingLeftSide(float currentX, float nextX, out List<Tile> tilesCollided)
        {
            tilesCollided = new List<Tile>();
            if (VelocityX >= 0)
            {
                return false;
            }

            int lowerTileX = Grid.GetTilePosition(currentX);
            int upperTileX = MathHelper.Clamp(Grid.GetTilePosition(nextX), 0, (int) World.Width);
            int lowerTileY = Grid.GetTilePosition(Y);
            int upperTileY = Grid.GetTilePosition(Y + Height - 0.01f);

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
        /// Determines whether a character will collide with one or more tiles in a frame.
        /// Specifically, the method checks the tiles right of the character.
        /// </summary>
        /// <param name="currentX">Current x-coordinate in units.</param>
        /// <param name="nextX">Next x-coordinate in units.</param>
        /// <param name="tilesCollided">
        /// A list of tiles a character collided; specifically tiles right of the
        /// character.
        /// </param>
        /// <returns>Returns true if a character collides with at least one tile.</returns>
        public bool IsCollidingRightSide(float currentX, float nextX, out List<Tile> tilesCollided)
        {
            tilesCollided = new List<Tile>();
            if (VelocityX <= 0)
            {
                return false;
            }

            currentX += Width;
            nextX += Width;

            int lowerTileX = Grid.GetTilePosition(currentX);
            int upperTileX = MathHelper.Clamp(Grid.GetTilePosition(nextX), 0, (int) World.Width);
            int lowerTileY = Grid.GetTilePosition(Y);
            int upperTileY = Grid.GetTilePosition(Y + Height - 0.01f);

            for (int tileX = lowerTileX; tileX <= upperTileX; ++tileX)
            {
                for (int tileY = lowerTileY; tileY <= upperTileY; ++tileY)
                {
                    Tile tile = World[tileX, tileY];
                    if (tile is not null)
                    {
                        tilesCollided.Add(tile);
                        return true;
                    }
                }
                if (tilesCollided.Count > 0)
                {
                    return true;
                }
            }

            if (nextX > World.Width)
            {
                X = World.Width - Width;
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
            float upperBoundY = lowerBoundY + Grid.Size / 2f;
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
            float upperBoundY = lowerBoundY + Grid.Size / 2f;
            float tileY = lowerBoundY + Grid.Size;

            if (positionY >= lowerBoundY && positionY < upperBoundY)
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
