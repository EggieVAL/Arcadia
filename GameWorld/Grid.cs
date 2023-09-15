using Arcadia.GameObject.Tiles;

namespace Arcadia.GameWorld
{
    public sealed class Grid
    {
        public const int Size = 16;

        public static int GetPosition(float coordinate)
        {
            return (int) (coordinate / Size);
        }

        public static int[] GetPosition(float x, float y)
        {
            int positionX = (int) (x / Size);
            int positionY = (int) (y / Size);
            return new int[] { positionX, positionY };
        }

        public static int ConvertToUnits(int tileCoordinate)
        {
            return tileCoordinate * Size;
        }

        public static int[] ConvertToUnits(int gridX, int gridY)
        {
            int positionX = gridX * Size;
            int positionY = gridY * Size;
            return new int[] { positionX, positionY };
        }

        public static bool InBounds(int[,] grid, int x, int y)
        {
            return (x >= 0 && x < grid.GetLength(0))
                && (y >= 0 && y < grid.GetLength(1));
        }

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

        public int GridWidth => _grid.GetLength(0);

        public int GridHeight => _grid.GetLength(1);

        public float Width => GridWidth * Grid.Size;

        public float Height => GridHeight * Grid.Size;

        public Grid(int gridWidth, int gridHeight)
        {
            _grid = new Tile[gridWidth, gridHeight];
        }

        public bool InBounds(int gridX, int gridY)
        {
            return (gridX >= 0 && gridX < GridWidth)
                && (gridY >= 0 && gridY < GridHeight);
        }

        private Tile[,] _grid;
    }
}
