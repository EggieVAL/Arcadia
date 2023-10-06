using Arcadia.GameObjects.Tiles;
using Arcadia.GameWorld;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Arcadia.GameObjects.Characters
{
    public abstract class Character : RenderableObject
    {
        public World World { get; set; }

        public float VelocityX { get; set; }

        public float VelocityY { get; set; }

        public bool IsFalling => VelocityY > 0;

        public bool IsRising => VelocityY < 0;

        public bool IsMovingLeft => VelocityX < 0;

        public bool IsMovingRight => VelocityX > 0;

        public bool IsMovingHorizontally => VelocityX != 0;

        public bool IsMovingVertically => VelocityY != 0;

        public Character(Texture2D texture, Rectangle bounds, World world) : base(texture, bounds)
        {
            VelocityX = 0;
            VelocityY = 0;
            World = world;
        }

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

            if (IsCollidingOnTheLeft(currentX, nextX, out tilesCollided))
            {
                handleLeftCollision(tilesCollided);
            }
            else if (IsCollidingOnTheRight(currentX, nextX, out tilesCollided))
            {
                handleRightCollision(tilesCollided);
            }
        }
        
        public bool IsCollidingAbove(float currentY, float nextY, out List<Tile> tilesCollided)
        {
            tilesCollided = new List<Tile>();
            if (IsFalling || !IsMovingVertically)
            {
                return false;
            }

            int lowerGridX = Grid.GetPosition(X);
            int upperGridX = Grid.GetPosition(X + Width - 0.01f);
            int lowerGridY = Grid.GetPosition(currentY);
            int upperGridY = MathHelper.Clamp(Grid.GetPosition(nextY), 0, World.GridHeight);

            for (int gridY = lowerGridY; gridY >= upperGridY; --gridY)
            {
                for (int gridX = lowerGridX; gridX <= upperGridX; ++gridX)
                {
                    Tile tile = World[gridX, gridY];
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

        public bool IsCollidingBelow(float currentY, float nextY, out List<Tile> tilesCollided)
        {
            tilesCollided = new List<Tile>();
            if (IsRising || !IsMovingVertically)
            {
                return false;
            }

            currentY += Height;
            nextY += Height;

            int lowerGridX = Grid.GetPosition(X);
            int upperGridX = Grid.GetPosition(X + Width - 0.01f);
            int lowerGridY = Grid.GetPosition(currentY);
            int upperGridY = MathHelper.Clamp(Grid.GetPosition(nextY), 0, World.GridHeight);

            for (int gridY = lowerGridY; gridY <= upperGridY; ++gridY)
            {
                for (int gridX = lowerGridX; gridX <= upperGridX; ++gridX)
                {
                    Tile tile = World[gridX, gridY];
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

        public bool IsCollidingOnTheLeft(float currentX, float nextX, out List<Tile> tilesCollided)
        {
            tilesCollided = new List<Tile>();
            if (IsMovingRight || !IsMovingHorizontally)
            {
                return false;
            }

            int lowerGridX = Grid.GetPosition(currentX);
            int upperGridX = MathHelper.Clamp(Grid.GetPosition(nextX), 0, World.GridWidth);
            int lowerGridY = Grid.GetPosition(Y);
            int upperGridY = Grid.GetPosition(Y + Height - 0.01f);

            for (int gridX = lowerGridX; gridX >= upperGridX; --gridX)
            {
                for (int gridY = lowerGridY; gridY <= upperGridY; ++gridY)
                {
                    Tile tile = World[gridX, gridY];
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

        public bool IsCollidingOnTheRight(float currentX, float nextX, out List<Tile> tilesCollided)
        {
            tilesCollided = new List<Tile>();
            if (IsMovingLeft || !IsMovingHorizontally)
            {
                return false;
            }

            currentX += Width;
            nextX += Width;

            int lowerGridX = Grid.GetPosition(currentX);
            int upperGridX = MathHelper.Clamp(Grid.GetPosition(nextX), 0, World.GridWidth);
            int lowerGridY = Grid.GetPosition(Y);
            int upperGridY = Grid.GetPosition(Y + Height - 0.01f);

            for (int gridX = lowerGridX; gridX <= upperGridX; ++gridX)
            {
                for (int gridY = lowerGridY; gridY <= upperGridY; ++gridY)
                {
                    Tile tile = World[gridX, gridY];
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
            float upperBoundY = lowerBoundY + Grid.Size * 0.6f;
            float gridY = lowerBoundY + Grid.Size;

            if (positionY >= lowerBoundY && positionY <= upperBoundY)
            {
                Y = lowerBoundY - Height;
                VelocityY = 0;
            }
            else if (VelocityY == 0 && positionY == gridY)
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
            float gridY = lowerBoundY + Grid.Size;

            if (positionY >= lowerBoundY && positionY <= upperBoundY)
            {
                Y = lowerBoundY - Height;
                VelocityY = 0;
            }
            else if (VelocityY == 0 && positionY == gridY)
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
