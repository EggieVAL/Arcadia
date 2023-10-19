using Arcadia.GameObjects.Tiles;

namespace Arcadia.GameWorld
{
    /// <summary>
    /// The <see cref="Grid"/> class is a representation of a grid. A grid is composed of tiles going across (widh) and vertically (height).
    /// </summary>
    public sealed class Grid
    {
        /// <summary>
        /// The width and height of the tile.
        /// </summary>
        public const int Size = 16;

        /// <summary>
        /// Gets the grid coordinate of some unit coordinate.
        /// </summary>
        /// <param name="coordinate">The coordinate in units.</param>
        /// <returns>An integer representation of a grid coordinate</returns>
        public static int GetPosition(float coordinate)
        {
            return (int) (coordinate / Size);
        }

        /// <summary>
        /// Gets the grid coordinates of some unit coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate in units.</param>
        /// <param name="y">The y-coordinate in units.</param>
        /// <returns>An integer array representation of grid coordinates.</returns>
        public static int[] GetPosition(float x, float y)
        {
            int positionX = (int) (x / Size);
            int positionY = (int) (y / Size);
            return new int[] { positionX, positionY };
        }

        /// <summary>
        /// Gets the unit coordinate of some grid coordinate.
        /// </summary>
        /// <param name="tileCoordinate">The tile coordinate.</param>
        /// <returns>An integer representation of a unit coordinate.</returns>
        public static int ConvertToUnits(int tileCoordinate)
        {
            return tileCoordinate * Size;
        }

        /// <summary>
        /// Gets the unit coordinates of some grid coordinates.
        /// </summary>
        /// <param name="tileX">The x-coordinate in the grid space.</param>
        /// <param name="tileY">The y-coordinate in the grid space.</param>
        /// <returns>An integer array representation of unit coordinates.</returns>
        public static int[] ConvertToUnits(int tileX, int tileY)
        {
            int positionX = tileX * Size;
            int positionY = tileY * Size;
            return new int[] { positionX, positionY };
        }

        /// <summary>
        /// Whether the given position is within the bounds of a <paramref name="grid"/>.
        /// </summary>
        /// <param name="grid">The grid.</param>
        /// <param name="tileX">The x-coordinate in the grid space.</param>
        /// <param name="tileY">The y-cooridnate in the grid space.</param>
        /// <returns><c>true</c> if (<paramref name="tileX"/>, <paramref name="tileY"/>) is in the grid; otherwise <c>false</c>.</returns>
        public static bool InBounds(int[,] grid, int tileX, int tileY)
        {
            return (tileX >= 0 && tileX < grid.GetLength(0))
                && (tileY >= 0 && tileY < grid.GetLength(1));
        }

        /// <summary>
        /// The <see cref="Tile"/> at the given tile coordinates.
        /// </summary>
        /// <param name="tileX">The x-coordiante in the grid space.</param>
        /// <param name="tileY">The y-coordinate in the grid space.</param>
        /// <returns>The <see cref="Tile"/> at the given grid coordinates.</returns>
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
        /// The width of the grid in terms of tiles.
        /// </summary>
        public int Width => _grid.GetLength(0);

        /// <summary>
        /// The height of the grid in terms of tiles.
        /// </summary>
        public int Height => _grid.GetLength(1);

        /// <summary>
        /// The width of the grid in units.
        /// </summary>
        public float WidthInUnits => Width * Grid.Size;

        /// <summary>
        /// The height of the grid in units.
        /// </summary>
        public float HeightInUnits => Height * Grid.Size;

        /// <summary>
        /// Constructs a grid of some width and height.
        /// </summary>
        /// <param name="width">The width of the grid in terms of tiles.</param>
        /// <param name="height">The height of the grid in terms of tiles.</param>
        public Grid(int width, int height)
        {
            _grid = new Tile[width, height];
        }

        /// <summary>
        /// Whether the given position is within the bounds of this grid.
        /// </summary>
        /// <param name="tileX">The x-coordinate in a grid.</param>
        /// <param name="tileY">The y-coordinate in a grid.</param>
        /// <returns></returns>
        public bool InBounds(int tileX, int tileY)
        {
            return (tileX >= 0 && tileX < Width) && (tileY >= 0 && tileY < Height);
        }

        private readonly Tile[,] _grid;
    }
}
