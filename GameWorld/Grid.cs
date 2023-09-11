using Arcadia.GameObject.Tiles;
using Microsoft.Xna.Framework;
using System;

namespace Arcadia.GameWorld
{
    /// <summary>
    /// The <c>Grid</c> class is a grid representation of a world. Each cell in a grid is some type of tile.
    /// </summary>
    /// <seealso cref="GameObject.Tiles.Tile"/>
    public sealed class Grid
    {
        /// <summary>
        /// The size of each cell in the grid.
        /// </summary>
        public const int Size = 16;

        /// <summary>
        /// Finds where some coordinate is in the grid.
        /// <para>
        ///     <br>Rounds the calculated grid position to the nearest integer.</br>
        /// </para>
        /// </summary>
        /// <param name="coordinate">The coordinate in units.</param>
        /// <returns>
        /// Returns where the given <paramref name="coordinate"/> is in the grid.
        /// </returns>
        public static int GetTilePosition(float coordinate)
        {
            return (int) (coordinate / Size);
        }

        /// <summary>
        /// Finds where some position is in the grid.
        /// <para>
        ///     <br>Rounds the calculated grid position to the nearest integer.</br>
        /// </para>
        /// </summary>
        /// <param name="x">The x-coordinate in units.</param>
        /// <param name="y">The y-coordinate in units.</param>
        /// <returns>
        /// Returns where the given (<paramref name="x"/>, <paramref name="y"/>) is
        /// in the grid.
        /// </returns>
        public static int[] GetTilePosition(float x, float y)
        {
            int positionX = (int) (x / Size);
            int positionY = (int) (y / Size);
            return new int[] { positionX, positionY };
        }

        /// <summary>
        /// Finds where some position is in the grid.
        /// <para>
        ///     <br>Rounds the calculated grid position to the nearest integer.</br>
        /// </para>
        /// </summary>
        /// <param name="position">
        /// The position; its components are in units.
        /// <br><paramref name="position"/>[0] = x-coordinate</br>
        /// <br><paramref name="position"/>[1] = y-coordinate</br>
        /// </param>
        /// <returns>
        /// Returns where the given position <paramref name="position"/> is in the grid.
        /// </returns>
        public static int[] GetTilePosition(float[] position)
        {
            int positionX = (int) (position[0] / Size);
            int positionY = (int) (position[1] / Size);
            return new int[] { positionX, positionY };
        }

        /// <summary>
        /// Finds where some vector is in the grid.
        /// <para>
        ///     <br>Rounds the calculated grid position to the nearest integer.</br>
        /// </para>
        /// </summary>
        /// <param name="vector"> The vector; its components are in units.</param>
        /// <returns>
        /// Returns where the given vector <paramref name="vector"/> is in the grid.
        /// </returns>
        public static int[] GetTilePosition(Vector2 vector)
        {
            int positionX = (int) (vector.X / Size);
            int positionY = (int) (vector.Y / Size);
            return new int[] { positionX, positionY };
        }

        /// <summary>
        /// Converts some coordinate in the grid space into its respective unit component.
        /// </summary>
        /// <param name="tileCoordinate">The coordinate in the grid space.</param>
        /// <returns>
        /// Returns the unit value of the given <paramref name="tileCoordinate"/>.
        /// </returns>
        public static float ConvertToUnit(int tileCoordinate)
        {
            return tileCoordinate * Size;
        }

        /// <summary>
        /// Converts some position in the grid space into its respective unit component.
        /// </summary>
        /// <param name="tileX">The x-coordinate in the grid space.</param>
        /// <param name="tileY">The y-coordinate in the grid space.</param>
        /// <returns>
        /// Returns the unit value of the given (<paramref name="tileX"/>, <paramref name="tileY"/>).
        /// </returns>
        public static float[] ConvertToUnits(int tileX, int tileY)
        {
            float positionX = tileX * Size;
            float positionY = tileY * Size;
            return new float[] { positionX, positionY };
        }

        /// <summary>
        /// Converts some position in the grid space into its respective unit component.
        /// </summary>
        /// <param name="tilePosition">
        /// The position in the grid space.
        /// <br><paramref name="tilePosition"/>[0] = x-coordinate</br>
        /// <br><paramref name="tilePosition"/>[1] = y-coordinate</br>
        /// </param>
        /// <returns>
        /// Returns the unit value of the given position <paramref name="tilePosition"/>.
        /// </returns>
        public static float[] ConvertToUnits(int[] tilePosition)
        {
            float positionX = tilePosition[0] * Size;
            float positionY = tilePosition[1] * Size;
            return new float[] { positionX, positionY };
        }

        /// <summary>
        /// Converts some vector in the grid space into its respective unit component.
        /// </summary>
        /// <param name="tileVector"> The vector in the grid space. </param>
        /// <returns>
        /// Returns the unit value of the given vector <paramref name="tileVector"/>.
        /// </returns>
        public static float[] ConvertToUnits(Vector2 tileVector)
        {
            float positionX = tileVector.X * Size;
            float positionY = tileVector.Y * Size;
            return new float[] { positionX, positionY };
        }

        /// <summary>
        /// The tile in the grid at (<paramref name="tileX"/>, <paramref name="tileY"/>).
        /// </summary>
        /// <param name="tileX">The x-coordinate in the grid space.</param>
        /// <param name="tileY">The y-coordinate in the grid space.</param>
        /// <returns>
        /// Returns the tile in the grid at (<paramref name="tileX"/>, <paramref name="tileY"/>).
        /// Returns <c>null</c> if the position is outside the scope of the grid.
        /// </returns>
        public Tile this[int tileX, int tileY]
        {
            get => InBounds(tileX, tileY) ? _grid[tileX, tileY] : null;
            set
            {
                if (InBounds(tileX, tileY))
                {
                    _grid[tileX, tileY] = value;
                }
            }
        }

        /// <summary>
        /// The tile in the grid at (<paramref name="x"/>, <paramref name="y"/>).
        /// </summary>
        /// <param name="x">The x-coordinate in units.</param>
        /// <param name="y">The y-coordinate in units.</param>
        /// <returns>
        /// Returns the tile in the grid at (<paramref name="x"/>, <paramref name="y"/>).
        /// Returns <c>null</c> if the position is outside the scope of the grid.
        /// </returns>
        public Tile this[float x, float y]
        {
            get => InBounds(x, y) ? _grid[GetTilePosition(x), GetTilePosition(y)] : null;
            set
            {
                if (InBounds(x, y))
                {
                    _grid[GetTilePosition(x), GetTilePosition(y)] = value;
                }
            }
        }

        /// <summary>
        /// The tile width of the grid.
        /// </summary>
        public int TileWidth => _grid.GetLength(0);

        /// <summary>
        /// The tile height of the grid.
        /// </summary>
        public int TileHeight => _grid.GetLength(1);

        /// <summary>
        /// The width of the grid in units.
        /// </summary>
        public float Width => TileWidth * Grid.Size;

        /// <summary>
        /// The height of the grid in units.
        /// </summary>
        public float Height => TileHeight * Grid.Size;

        /// <summary>
        /// Constructs a grid of some <paramref name="tileWidth"/> and <paramref name="tileHeight"/>.
        /// </summary>
        /// <param name="tileWidth">The tile width of a grid.</param>
        /// <param name="tileHeight">The tile height of a grid.</param>
        public Grid(int tileWidth, int tileHeight)
        {
            _grid = new Tile[tileWidth, tileHeight];
        }

        /// <summary>
        /// Constructs a grid of some <paramref name="width"/> and <paramref name="height"/>.
        /// </summary>
        /// <param name="width">The width of a grid in units.</param>
        /// <param name="height">The height of a grid in units.</param>
        public Grid(float width, float height)
        {
            _grid = new Tile[(int)(width / Grid.Size), (int)(height / Grid.Size)];
        }

        /// <summary>
        /// Whether the position (<paramref name="tileX"/>, <paramref name="tileY"/>) is in the bounds
        /// of the grid.
        /// </summary>
        /// <param name="tileX">The tile x-coordinate.</param>
        /// <param name="tileY">The tile y-coordinate.</param>
        /// <returns>
        /// Returns true if the position (<paramref name="tileX"/>, <paramref name="tileY"/>) is in
        /// the bounds of the grid.
        /// </returns>
        public bool InBounds(int tileX, int tileY)
        {
            return (tileX >= 0 && tileX < TileWidth)
                && (tileY >= 0 && tileY < TileHeight);
        }

        /// <summary>
        /// Whether the position (<paramref name="x"/>, <paramref name="y"/>) is in
        /// the bounds of the grid.
        /// </summary>
        /// <param name="x">The x-coordinate in units.</param>
        /// <param name="y">The y-coordinate in units.</param>
        /// <returns>
        /// Returns true if the position (<paramref name="x"/>, <paramref name="y"/>)
        /// is in the bounds of the grid.
        /// </returns>
        public bool InBounds(float x, float y)
        {
            return (x >= 0 && x < Width)
                && (y >= 0 && y < Height);
        }

        private Tile[,] _grid;
    }
}
