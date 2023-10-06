using Arcadia.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arcadia.GameObjects
{
    public abstract class RenderableObject : GameObject
    {
        public Texture2D Texture { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Rectangle Bounds => new Rectangle((int) MathF.Round(X), (int) MathF.Round(Y), Width, Height);

        public RenderableObject(Texture2D texture, Rectangle bounds)
        {
            Texture = texture;
            X = bounds.X;
            Y = bounds.Y;
            Width = bounds.Width;
            Height = bounds.Height;
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(GameTime gameTime)
        {
            if (Texture is not null)
            {
                SpriteManager.Draw(Texture, Bounds, Color.White);
            }
        }
    }
}
