using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Arcadia.Graphics
{
    public sealed class SpriteSheet
    {
        public Rectangle? this[int row, int col] => GetSprite(row, col);

        public List<Rectangle> this[int row] => GetRowOfSprites(row);

        public Texture2D Atlas { get; init; }

        public SpriteSheet(Texture2D atlas, int spriteWidth, int spriteHeight, int gap) : this(atlas, spriteWidth, spriteHeight, gap, gap) { }

        public SpriteSheet(Texture2D atlas, int spriteWidth, int spriteHeight, int horizontalGap, int verticalGap)
        {
            Atlas = atlas;
            _sprites = new List<List<Rectangle>>();

            FindSourceRectangles(spriteWidth, spriteHeight, horizontalGap, verticalGap);
        }

        public List<Rectangle> GetRowOfSprites(int row)
        {
            if (row >= _sprites.Count)
            {
                return null;
            }
            return _sprites[row];
        }

        public Rectangle? GetSprite(int row, int col)
        {
            if (row >= _sprites.Count)
            {
                return null;
            }
            if (col >= _sprites[row].Count)
            {
                return null;
            }
            return _sprites[row][col];
        }

        private void FindSourceRectangles(int spriteWidth, int spriteHeight, int horizontalGap, int verticalGap)
        {
            for (int y = 0; y < Atlas.Height; y += spriteHeight + verticalGap)
            {
                List<Rectangle> rowOfSourceRectangles = new();
                for (int x = 0; x < Atlas.Width; x += spriteWidth + horizontalGap)
                {
                    Rectangle rectangle = new(x, y, spriteWidth, spriteHeight);
                    rowOfSourceRectangles.Add(rectangle);
                }
                _sprites.Add(rowOfSourceRectangles);
            }
        }

        private readonly List<List<Rectangle>> _sprites;
    }
}
