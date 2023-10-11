using Arcadia.GameObjects.Tiles;
using Arcadia.GameWorld;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Arcadia.GameObjects
{
    public class Projectile : RenderableObject
    {
        float VelocityX;
        float VelocityY;

        World _world;

        float disappearTimer;
        float disappearTime;

        bool isGrounded;

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

        public Projectile(Texture2D[] texture, Rectangle bounds, World world) : base(texture, bounds)
        {
            VelocityX = 0; 
            VelocityY = 0.0015265f * Grid.Size;
            _world = world;
            disappearTime = 10;
            disappearTimer = disappearTime;
            isGrounded = false;
        }
        public Projectile(Texture2D[] texture, Rectangle bounds, int X, int Y, float VelX, float VelY, World world) : base(texture, bounds)
        {
            VelocityX = 0;
            VelocityY = 0.0015265f * Grid.Size;
            _world = world;
            disappearTime = 10;
            disappearTimer = disappearTime;
            Fire(X, Y, VelX, VelY);
        }

        public void Fire(int X, int Y, float VelocityX, float VelocityY)
        {
            this.X = X;
            this.Y = Y;
            this.VelocityX += VelocityX;
            this.VelocityY += VelocityY;
            isGrounded = false;
        }

        public override void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float currentX = X;
            float currentY = Y;
            float nextX = (X += VelocityX * elapsedTime * 1000);
            float nextY = (Y += VelocityY * elapsedTime * 1000);

            VelocityY += 0.0015265f * Grid.Size;

            if (IsCollidingBelow(currentY, nextY, out List<Tile> tilesCollided))
            {
                handleBottomCollision(tilesCollided);
                isGrounded = true;
            }
            else if (IsCollidingAbove(currentY, nextY, out tilesCollided))
            {
                handleTopCollision(tilesCollided);
                isGrounded = true;
            }
            if (IsCollidingToTheLeft(currentX, nextX, out tilesCollided))
            {
                handleLeftCollision(tilesCollided);
                isGrounded = true;
            }
            else if (IsCollidingToTheRight(currentX, nextX, out tilesCollided))
            {
                handleRightCollision(tilesCollided);
                isGrounded = true;
            }

            if (isGrounded)
            {
                disappearTimer -= elapsedTime;
                if(disappearTimer <= 0)
                {
                    _world.Destroy(this);
                }
            }

        }
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
            int upperTileY = MathHelper.Clamp(Grid.GetPosition(nextY), 0, _world.Height);

            for (int tileY = lowerTileY; tileY >= upperTileY; --tileY)
            {
                for (int tileX = lowerTileX; tileX <= upperTileX; ++tileX)
                {
                    Tile tile = _world[tileX, tileY];
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
            int upperTileY = MathHelper.Clamp(Grid.GetPosition(nextY), 0, _world.Height);

            for (int tileY = lowerTileY; tileY <= upperTileY; ++tileY)
            {
                for (int tileX = lowerTileX; tileX <= upperTileX; ++tileX)
                {
                    Tile tile = _world[tileX, tileY];
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

            if (nextY > _world.HeightInUnits)
            {
                Y = _world.HeightInUnits - Height;
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
            int upperTileX = MathHelper.Clamp(Grid.GetPosition(nextX), 0, _world.Width);
            int lowerTileY = Grid.GetPosition(Y);
            int upperTileY = Grid.GetPosition(Y + Height - 0.01f);

            for (int tileX = lowerTileX; tileX >= upperTileX; --tileX)
            {
                for (int tileY = lowerTileY; tileY <= upperTileY; ++tileY)
                {
                    Tile tile = _world[tileX, tileY];
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
            int upperTileX = MathHelper.Clamp(Grid.GetPosition(nextX), 0, _world.Width);
            int lowerTileY = Grid.GetPosition(Y);
            int upperTileY = Grid.GetPosition(Y + Height - 0.01f);

            for (int tileX = lowerTileX; tileX <= upperTileX; ++tileX)
            {
                for (int tileY = lowerTileY; tileY <= upperTileY; ++tileY)
                {
                    Tile tile = _world[tileX, tileY];
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

            if (nextX > _world.WidthInUnits)
            {
                X = _world.WidthInUnits - Width;
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

            VelocityY = 0f;
            VelocityX = 0;
            Y = tilesCollided[0].Y + Grid.Size;
        }

        private void handleBottomCollision(List<Tile> tilesCollided)
        {
            if (tilesCollided.Count == 0)
            {
                return;
            }

            VelocityY = 0;
            VelocityX = 0;
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
