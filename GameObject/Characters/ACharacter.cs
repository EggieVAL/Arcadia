using Arcadia.GameObject.Tiles;
using Arcadia.GameWorld;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Arcadia.GameObject.Characters
{
    /// <summary>
    ///     The <c>ACharacter</c> class is an abstraction of a game character. A game
    ///     character interacts in the environment they live in, from moving in the
    ///     world to changing their surroundings.
    /// </summary>
    public abstract class ACharacter : ARenderableObject
    {
        /// <summary>
        ///     The x-component of a character's velocity.
        /// </summary>
        public float VelX { get; set; }

        /// <summary>
        ///     The y-component of a character's velocity.
        /// </summary>
        public float VelY { get; set; }
        
        /// <summary>
        ///     The velocity of a character.
        /// </summary>
        public Vector2 Vel => new Vector2(VelX, VelY);

        /// <summary>
        ///     Constructs a character with some texture in some bounding box.
        /// </summary>
        /// <param name="texture">The texture of a character.</param>
        /// <param name="bounds">The bounds of a character.</param>
        public ACharacter(Texture2D texture, Rectangle bounds) : base(texture, bounds)
        {
            VelX = 0;
            VelY = 0;
        }

        /// <summary>
        ///     This <c>Update</c> method checks and updates a character's collision
        ///     state.
        /// </summary>
        /// <param name="gt">The time state of the game.</param>
        public override void Update(GameTime gt)
        {
            float elapsedTime = gt.ElapsedGameTime.Milliseconds;
            float prevX = UnitX;
            float prevY = UnitY;

            UnitY += VelY * elapsedTime;
            if (BotCollision(prevY, UnitY, out List<ATile> bot))
            {
                if (bot.Count > 0)
                {
                    VelY = 0;
                    UnitY = bot[0].UnitY - Height;
                }
            }
            else if (TopCollision(prevY, UnitY, out List<ATile> top))
            {
                if (top.Count > 0)
                {
                    VelY = 0.0000000001f;
                    UnitY = top[0].UnitY + Grid.Size;
                }
            }
            else
            {
                // apply gravity
            }

            UnitX += VelX * elapsedTime;
            if (LeftCollision(prevX, UnitX, out List<ATile> left))
            {
                if (left.Count > 0)
                {
                    float cy = UnitY + Height;
                    float ty0 = left[0].UnitY;
                    float ty1 = ty0 + Grid.Size / 2f;
                    float ty = ty0 + Grid.Size;

                    if (cy >= ty0 && cy < ty1)
                    {
                        UnitY = ty0 - Height;
                        VelY = 0;
                    }
                    else if (VelY == 0 && cy == ty)
                    {
                        UnitY -= Grid.Size;
                    }
                    else
                    {
                        UnitX = left[0].UnitX + Grid.Size;
                        VelX = 0;
                    }
                }
            }
            else if (RightCollision(prevX, UnitX, out List<ATile> right))
            {
                if (right.Count > 0)
                {
                    float cy = UnitY + Height;
                    float ty0 = right[0].UnitY;
                    float ty1 = ty0 + Grid.Size / 2f;
                    float ty = ty0 + Grid.Size;

                    if (cy >= ty0 && cy < ty1)
                    {
                        UnitY = ty0 - Height;
                        VelY = 0;
                    }
                    else if (VelY == 0 && cy == ty)
                    {
                        UnitY -= Grid.Size;
                    }
                    else
                    {
                        UnitX = right[0].UnitX - Width;
                        VelX = 0;
                    }
                }
            }
        }
        
        /// <summary>
        ///     Determines whether a character will collide with one or more tiles in
        ///     a frame. Specifically, the method checks the tiles above the character.
        /// </summary>
        /// <param name="currY">Current y-coordinate in units.</param>
        /// <param name="nextY">Next y-coordinate in units.</param>
        /// <param name="top">
        ///     A list of tiles a character collided; specifically tiles above the
        ///     character.
        /// </param>
        /// <returns>
        ///     Returns <c>true</c> if a character collides with at least one tile;
        ///     otherwise <c>false</c>.
        /// </returns>
        public bool TopCollision(float currY, float nextY, out List<ATile> top)
        {
            top = new List<ATile>();
            if (VelY >= 0)
            {
                return false;
            }

            int x0 = (int) (UnitX / Grid.Size);
            int x1 = (int) ((UnitX + Width - 0.01f) / Grid.Size);
            int y0 = (int) (currY / Grid.Size);
            int y1 = (int) (nextY / Grid.Size);

            // clamp y1

            for (int y = y0; y >= y1; --y)
            {
                for (int x = x0; x <= x1; ++x)
                {
                    ATile tile = null;
                    if (tile is not null)
                    {
                        top.Add(tile);
                    }
                }
                if (top.Count > 0)
                {
                    return true;
                }
            }

            if (UnitY < 0)
            {
                UnitY = 0;
                VelY = 0;
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Determines whether a character will collide with one or more tiles in
        ///     a frame. Specifically, the method checks the tiles below the character.
        /// </summary>
        /// <param name="currY">Current y-coordinate in units.</param>
        /// <param name="nextY">Next y-coordinate in units.</param>
        /// <param name="bot">
        ///     A list of tiles a character collided; specifically tiles below the
        ///     character.
        /// </param>
        /// <returns>
        ///     Returns <c>true</c> if a character collides with at least one tile;
        ///     otherwise <c>false</c>.
        /// </returns>
        public bool BotCollision(float currY, float nextY, out List<ATile> bot)
        {
            bot = new List<ATile>();
            if (VelY <= 0)
            {
                return false;
            }

            currY += Height;
            nextY += Height;

            int x0 = (int) (UnitX / Grid.Size);
            int x1 = (int) ((UnitX + Width - 0.01f) / Grid.Size);
            int y0 = (int) (currY / Grid.Size);
            int y1 = (int) (nextY / Grid.Size);

            // clamp y1

            for (int y = y0; y <= y1; ++y)
            {
                for (int x = x0; x <= x1; ++x)
                {
                    ATile tile = null;
                    if (tile is not null)
                    {
                        bot.Add(tile);
                    }
                }
                if (bot.Count > 0)
                {
                    return true;
                }
            }

            // check world boundary
            return false;
        }

        /// <summary>
        ///     Determines whether a character will collide with one or more tiles in
        ///     a frame. Specifically, the method checks the tiles left of the character.
        /// </summary>
        /// <param name="currX">Current x-coordinate in units.</param>
        /// <param name="nextX">Next x-coordinate in units.</param>
        /// <param name="left">
        ///     A list of tiles a character collided; specifically tiles left of the
        ///     character.
        /// </param>
        /// <returns>
        ///     Returns <c>true</c> if a character collides with at least one tile;
        ///     otherwise <c>false</c>.
        /// </returns>
        public bool LeftCollision(float currX, float nextX, out List<ATile> left)
        {
            left = new List<ATile>();
            if (VelX >= 0)
            {
                return false;
            }

            int x0 = (int) (currX / Grid.Size);
            int x1 = (int) (nextX / Grid.Size);
            int y0 = (int) (UnitY / Grid.Size);
            int y1 = (int) ((UnitY + Height - 0.01f) / Grid.Size);

            // clamp x1

            for (int x = x0; x >= x1; --x)
            {
                for (int y = y0; y <= y1; ++y)
                {
                    ATile tile = null;
                    if (tile is not null)
                    {
                        left.Add(tile);
                    }
                }
                if (left.Count > 0)
                {
                    return true;
                }
            }

            if (UnitX < 0)
            {
                UnitX = 0;
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Determines whether a character will collide with one or more tiles in
        ///     a frame. Specifically, the method checks the tiles right of the character.
        /// </summary>
        /// <param name="currX">Current x-coordinate in units.</param>
        /// <param name="nextX">Next x-coordinate in units.</param>
        /// <param name="right">
        ///     A list of tiles a character collided; specifically tiles right of the
        ///     character.
        /// </param>
        /// <returns>
        ///     Returns <c>true</c> if a character collides with at least one tile;
        ///     otherwise <c>false</c>.
        /// </returns>
        public bool RightCollision(float currX, float nextX, out List<ATile> right)
        {
            right = new List<ATile>();
            if (VelX <= 0)
            {
                return false;
            }

            int x0 = (int) (currX / Grid.Size);
            int x1 = (int) (nextX / Grid.Size);
            int y0 = (int) (UnitY / Grid.Size);
            int y1 = (int) ((UnitY + Height - 0.01f) / Grid.Size);

            // clamp x1

            for (int x = x0; x <= x1; ++x)
            {
                for (int y = y0; y <= y1; ++y)
                {
                    ATile tile = null;
                    if (tile is not null)
                    {
                        right.Add(tile);
                        return true;
                    }
                }
                if (right.Count > 0)
                {
                    return true;
                }
            }

            // check world boundary
            return false;
        }
    }
}
