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
        public const int Size = 32;

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
            return (int) Math.Round(x / Size);
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
            return (int) Math.Round(y / Size);
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
            int posx = (int) Math.Round(x / Size);
            int posy = (int) Math.Round(x / Size);
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
            int posx = (int) Math.Round(pos[0] / Size);
            int posy = (int) Math.Round(pos[1] / Size);
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
            int posx = (int)Math.Round(vec.X / Size);
            int posy = (int)Math.Round(vec.Y / Size);
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
    }
}
