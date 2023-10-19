using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcadia.Graphics
{
    public sealed class SpriteSheet
    {
        public Rectangle? this[int row, int column] => GetSprite(row, column);

        public Rectangle[] this[int row] => GetRowOfSprites(row);

        public int NumberOfRows => _sprites.Length;

        public int NumberOfColumns => _sprites[0].Length;

        public Texture2D Atlas { get; init; }

        public SpriteSheet(Texture2D atlas, int spriteWidth, int spriteHeight) : this(atlas, spriteWidth, spriteHeight, 0, 0) { }

        public SpriteSheet(Texture2D atlas, int spriteWidth, int spriteHeight, int gap) : this(atlas, spriteWidth, spriteHeight, gap, gap) { }

        public SpriteSheet(Texture2D atlas, int spriteWidth, int spriteHeight, int horizontalGap, int verticalGap)
        {
            Atlas = atlas;
            _sprites = new Rectangle[MaxNumberOfSprites(Atlas.Height, spriteHeight, verticalGap)][];

            FindSprites(spriteWidth, spriteHeight, horizontalGap, verticalGap);
        }

        public Rectangle? GetSprite(int row, int column)
        {
            if (row >= NumberOfRows || column >= NumberOfColumns)
            {
                return null;
            }
            return _sprites[row][column];
        }

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
