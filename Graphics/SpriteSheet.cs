using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcadia.Graphics
{
    /// <summary>
    /// The <see cref="SpriteSheet"/> is a representation of a sprite sheet. A sprite sheet contains many sprites in one texture. This improves
    /// performance by allowing the CPU to load many sprites at once rather than loading hundreds of sprites individually.
    /// </summary>
    public sealed class SpriteSheet
    {
        /// <summary>
        /// The sprite at the given <paramref name="row"/> and <paramref name="column"/>.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns>The bounds of the sprite.</returns>
        public Rectangle? this[int row, int column] => GetSprite(row, column);

        /// <summary>
        /// A list of sprites at the given <paramref name="row"/>.
        /// </summary>
        /// <param name="row">Thr row.</param>
        /// <returns>A list of bounds of sprites.</returns>
        public Rectangle[] this[int row] => GetRowOfSprites(row);

        /// <summary>
        /// The number of rows in a sprite sheet.
        /// </summary>
        public int NumberOfRows => _sprites.Length;

        /// <summary>
        /// The number of columns in a sprite sheet.
        /// </summary>
        public int NumberOfColumns => _sprites[0].Length;

        /// <summary>
        /// The atlas (sprite sheet).
        /// </summary>
        public Texture2D Atlas { get; init; }

        /// <summary>
        /// Constructs a sprite sheet.
        /// </summary>
        /// <param name="atlas">The atlas or sprite sheet in a texture.</param>
        /// <param name="spriteWidth">The width of a sprite in the atlas.</param>
        /// <param name="spriteHeight">The height of a sprite in the atlas.</param>
        public SpriteSheet(Texture2D atlas, int spriteWidth, int spriteHeight) : this(atlas, spriteWidth, spriteHeight, 0, 0) { }

        /// <summary>
        /// Constructs a sprite sheet.
        /// </summary>
        /// <param name="atlas">The atlas or sprite sheet in a texture.</param>
        /// <param name="spriteWidth">The width of a sprite in the atlas.</param>
        /// <param name="spriteHeight">The height of a sprite in the atlas.</param>
        /// <param name="gap">The horizontal and vertical gap between each sprite.</param>
        public SpriteSheet(Texture2D atlas, int spriteWidth, int spriteHeight, int gap) : this(atlas, spriteWidth, spriteHeight, gap, gap) { }

        /// <summary>
        /// Constructs a sprite sheet.
        /// </summary>
        /// <param name="atlas">The atlas or sprite sheet in a texture.</param>
        /// <param name="spriteWidth">The width of a sprite in the atlas.</param>
        /// <param name="spriteHeight">The height of a sprite in the atlas.</param>
        /// <param name="horizontalGap">The horizontal gap between each sprite.</param>
        /// <param name="verticalGap">The vertical gap between each sprite.</param>
        public SpriteSheet(Texture2D atlas, int spriteWidth, int spriteHeight, int horizontalGap, int verticalGap)
        {
            Atlas = atlas;
            _sprites = new Rectangle[MaxNumberOfSprites(Atlas.Height, spriteHeight, verticalGap)][];

            FindSprites(spriteWidth, spriteHeight, horizontalGap, verticalGap);
        }

        /// <summary>
        /// Gets the sprite at the given <paramref name="row"/> and <paramref name="column"/>.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns>The bounds of the sprite.</returns>
        public Rectangle? GetSprite(int row, int column)
        {
            if (row >= NumberOfRows || column >= NumberOfColumns)
            {
                return null;
            }
            return _sprites[row][column];
        }

        /// <summary>
        /// Gets a list of sprites at the given <paramref name="row"/>.
        /// </summary>
        /// <param name="row">Thr row.</param>
        /// <returns>A list of bounds of sprites.</returns>
        public Rectangle[] GetRowOfSprites(int row)
        {
            if (row >= NumberOfRows)
            {
                return null;
            }
            return _sprites[row];
        }

        private void FindSprites(int spriteWidth, int spriteHeight, int horizontalGap, int verticalGap)
        {
            int numberOfColumns = MaxNumberOfSprites(Atlas.Width, spriteWidth, horizontalGap);
            int offsetX = spriteWidth + horizontalGap;
            int offsetY = spriteHeight + verticalGap;
            int y = 0;

            for (int row = 0; row < NumberOfRows; ++row)
            {
                Rectangle[] rowOfSprites = new Rectangle[numberOfColumns];
                int x = 0;

                for (int column = 0; column < numberOfColumns; ++column)
                {
                    rowOfSprites[column] = new Rectangle(x, y, spriteWidth, spriteHeight);
                    x += offsetX;
                }

                _sprites[row] = rowOfSprites;
                y += offsetY;
            }
        }

        private static int MaxNumberOfSprites(int sheetDimension, int spriteDimension, int gap)
        {
            int offset = spriteDimension + gap;
            int numberOfSprites = 1;

            sheetDimension -= spriteDimension;
            while (sheetDimension > 0)
            {
                sheetDimension -= offset;
                numberOfSprites++;
            }

            return numberOfSprites;
        }

        private readonly Rectangle[][] _sprites;
    }
}
