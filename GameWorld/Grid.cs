using Microsoft.Xna.Framework;
using System;

namespace Arcadia.GameWorld
{
    /// <summary>
    ///     The <c>Grid</c> class is a grid representation of a world. Each cell in
    ///     a grid is some type of tile.
    /// </summary>
    /// <seealso cref="GameObject.Tiles.ATile"/>
    public sealed class Grid
    {
        /// <summary>
        ///     The size of each cell in the grid.
        /// </summary>
        public const int Size = 16;

        /// <summary>
        ///     Finds where some <paramref name="x"/> coordinate is in the grid.
        ///     <para>
        ///         <br>Grid X = <paramref name="x"/> / Width</br>
        ///     </para>
        ///     <para>
        ///         <br>Rounds the calculated grid position to the nearest integer.</br>
        ///     </para>
        /// </summary>
        /// <param name="x">The x-coordinate in units.</param>
        /// <returns>
        ///     Returns where the given <paramref name="x"/> is in the grid.
        /// </returns>
        public static int GetPosX(float x)
        {
            return (int) (x / Size);
        }

        /// <summary>
        ///     Finds where some <paramref name="y"/> coordinate is in the grid.
        ///     <para>
        ///         <br>Grid Y = <paramref name="y"/> / Height</br>
        ///     </para>
        ///     <para>
        ///         <br>Rounds the calculated grid position to the nearest integer.</br>
        ///     </para>
        /// </summary>
        /// <param name="y">The y-coordinate in units.</param>
        /// <returns>
        ///     Returns where the given <paramref name="y"/> is in the grid.
        /// </returns>
        public static int GetPosY(float y)
        {
            return (int) (y / Size);
        }

        /// <summary>
        ///     Finds where some position is in the grid.
        ///     <para>
        ///         <br>Grid X = <paramref name="x"/> / Width</br>
        ///         <br>Grid Y = <paramref name="y"/> / Height</br>
        ///     </para>
        ///     <para>
        ///         <br>Rounds the calculated grid position to the nearest integer.</br>
        ///     </para>
        /// </summary>
        /// <param name="x">The x-coordinate in units.</param>
        /// <param name="y">The y-coordinate in units.</param>
        /// <returns>
        ///     Returns where the given (<paramref name="x"/>, <paramref name="y"/>) is
        ///     in the grid.
        /// </returns>
        public static int[] GetPos(float x, float y)
        {
            int posx = (int) (x / Size);
            int posy = (int) (y / Size);
            return new int[] { posx, posy };
        }

        /// <summary>
        ///     Finds where some position is in the grid.
        ///     <para>
        ///         <br>Grid X = <paramref name="pos"/>[0] / Width</br>
        ///         <br>Grid Y = <paramref name="pos"/>[1] / Height</br>
        ///     </para>
        ///     <para>
        ///         <br>Rounds the calculated grid position to the nearest integer.</br>
        ///     </para>
        /// </summary>
        /// <param name="pos">
        ///     The position; its components are in units.
        ///     <para>
        ///         <br><paramref name="pos"/>[0] = x-coordinate</br>
        ///         <br><paramref name="pos"/>[1] = y-coordinate</br>
        ///     </para>
        /// </param>
        /// <returns>
        ///     Returns where the given position <paramref name="pos"/> is in the grid
        /// </returns>
        public static int[] GetPos(float[] pos)
        {
            int posx = (int) (pos[0] / Size);
            int posy = (int) (pos[1] / Size);
            return new int[] { posx, posy };
        }

        /// <summary>
        ///     Finds where some vector is in the grid.
        ///     <para>
        ///         <br>Grid X = <paramref name="vec"/>.X / Width</br>
        ///         <br>Grid Y = <paramref name="vec"/>.Y / Height</br>
        ///     </para>
        ///     <para>
        ///         <br>Rounds the calculated grid position to the nearest integer.</br>
        ///     </para>
        /// </summary>
        /// <param name="vec"> The vector; its components are in units.</param>
        /// <returns>
        ///     Returns where the given vector <paramref name="vec"/> is in the grid.
        /// </returns>
        public static int[] GetPos(Vector2 vec)
        {
            int posx = (int) (vec.X / Size);
            int posy = (int) (vec.Y / Size);
            return new int[] { posx, posy };
        }

        /// <summary>
        ///     Converts some <paramref name="x"/> coordinate in the grid into its
        ///     respective unit component.
        ///     <para>
        ///         <br>Unit X = <paramref name="x"/> * Width</br>
        ///     </para>
        /// </summary>
        /// <param name="x">The x-coordinate in the grid.</param>
        /// <returns>
        ///     Returns the unit value of the given <paramref name="x"/>.
        /// </returns>
        public static float ConvertToUnitX(int x)
        {
            return x * Size;
        }

        /// <summary>
        ///     Converts some <paramref name="y"/> coordinate in the grid into its
        ///     respective unit component.
        ///     <para>
        ///         <br>Unit Y = <paramref name="y"/> * Width</br>
        ///     </para>
        /// </summary>
        /// <param name="y">The y-coordinate in the grid.</param>
        /// <returns>
        ///     Returns the unit value of the given <paramref name="y"/>.
        /// </returns>
        public static float ConvertToUnitY(int y)
        {
            return y * Size;
        }

        /// <summary>
        ///     Converts some position in the grid into its respective unit component.
        ///     <para>
        ///         <br>Unit X = <paramref name="x"/> * Width</br>
        ///         <br>Unit Y = <paramref name="y"/> * Height</br>
        ///     </para>
        /// </summary>
        /// <param name="x">The x-coordinate in the grid.</param>
        /// <param name="y">The y-coordinate in the grid.</param>
        /// <returns>
        ///     Returns the unit value of the given (<paramref name="x"/>,
        ///     <paramref name="y"/>).
        /// </returns>
        public static float[] ConvertToUnit(int x, int y)
        {
            float posx = x * Size;
            float posy = y * Size;
            return new float[] { posx, posy };
        }

        /// <summary>
        ///     Converts some position in the grid into its respective unit component.
        ///     <para>
        ///         <br>Unit X = <paramref name="pos"/>[0] * Width</br>
        ///         <br>Unit Y = <paramref name="pos"/>[1] * Height</br>
        ///     </para>
        /// </summary>
        /// <param name="pos">
        ///     The position in the grid.
        ///     <para>
        ///         <br><paramref name="pos"/>[0] = x-coordinate</br>
        ///         <br><paramref name="pos"/>[1] = y-coordinate</br>
        ///     </para>
        /// </param>
        /// <returns>
        ///     Returns the unit value of the given position <paramref name="pos"/>.
        /// </returns>
        public static float[] ConvertToUnit(int[] pos)
        {
            float posx = pos[0] * Size;
            float posy = pos[1] * Size;
            return new float[] { posx, posy };
        }

        /// <summary>
        ///     Converts some vector in the grid into its respective unit component.
        ///     <para>
        ///         <br>Unit X = <paramref name="vec"/>.X * Width</br>
        ///         <br>Unit Y = <paramref name="vec"/>.Y * Height</br>
        ///     </para>
        /// </summary>
        /// <param name="vec"> The vector in the grid. </param>
        /// <returns>
        ///     Returns the unit value of the given vector <paramref name="vec"/>.
        /// </returns>
        public static float[] ConvertToUnit(Vector2 vec)
        {
            float posx = vec.X * Size;
            float posy = vec.Y * Size;
            return new float[] { posx, posy };
        }

        /// <summary>
        ///     The integer representation of the tile (<paramref name="x"/>,
        ///     <paramref name="y"/>).
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>
        ///     Returns the integer representation of the tile (<paramref name="x"/>,
        ///     <paramref name="y"/>) in the grid. Returns <c>-1</c> if the position
        ///     is outside the scope of the grid.
        /// </returns>
        public int this[int x, int y]
        {
            get => (InBounds(x, y)) ? _grid[x, y] : -1;
            set
            {
                if (InBounds(x, y))
                {
                    _grid[x, y] = value;
                }
            }
        }

        /// <summary>
        ///     The width of the grid.
        /// </summary>
        public int Width => _grid.GetLength(0);

        /// <summary>
        ///     The height of the grid.
        /// </summary>
        public int Height => _grid.GetLength(1);

        /// <summary>
        ///     The width of the grid in units.
        /// </summary>
        public float UnitWidth => Width * Grid.Size;

        /// <summary>
        ///     The height of the grid in units.
        /// </summary>
        public float UnitHeight => Height * Grid.Size;

        /// <summary>
        ///     Constructs a grid of some <paramref name="width"/> and
        ///     <paramref name="height"/>.
        /// </summary>
        public Grid(int width, int height)
        {
            _grid = new int[width, height];
        }

        /// <summary>
        ///     Whether the position (<paramref name="x"/>, <paramref name="y"/>)
        ///     is in the bounds of the grid.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>
        ///     Returns true if the position (<paramref name="x"/>,
        ///     <paramref name="y"/>) is in the bounds of the gridl otherwise
        ///     false.
        /// </returns>
        public bool InBounds(int x, int y)
        {
            return (x >= 0 && x < Width)
                && (y >= 0 && y < Height);
        }

        /// <summary>
        ///     Whether the position (<paramref name="unitx"/>,
        ///     <paramref name="unity"/>) is in the bounds of the grid.
        /// </summary>
        /// <param name="unitx">The x-coordinate in units.</param>
        /// <param name="unity">The y-coordinate in units.</param>
        /// <returns>
        ///     Returns true if the position (<paramref name="unitx"/>,
        ///     <paramref name="unity"/>) is in the bounds of the grid;
        ///     otherwise false.
        /// </returns>
        public bool InBounds(float unitx, float unity)
        {
            return (unitx >= 0 && unitx < UnitWidth)
                && (unity >= 0 && unity < UnitHeight);
        }

        private int[,] _grid;
    }
}
