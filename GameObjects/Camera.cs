using Arcadia.GameWorld;
using Arcadia.Graphics;
using Arcadia.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Arcadia.GameObjects
{
    public sealed class Camera : RenderableObject
    {
        public RenderableObject Target { get; private set; }

        public Matrix Projection { get; private set; }

        public Matrix View { get; private set; }

        public Matrix World { get; private set; }

        public float AspectRatio { get; set; }

        public float VFOV { get; set; }

        public float HFOV { get; set; }

        public float Z
        {
            get => _z;
            set => _z = MathHelper.Clamp(value, MinimumZ, MaximumZ);
        }

        public float MinimumZ { get; set; }

        public float MaximumZ { get; set; }

        public float ZoomRate { get; set; }

        public Camera(Scene scene, float x, float y) : base(null, new Rectangle((int) x, (int) y, 0, 0))
        {
            AspectRatio = (float) scene.Width / scene.Height;
            VFOV = MathHelper.PiOver2;
            HFOV = 2 * MathF.Atan(MathF.Tan(0.5f * VFOV) * AspectRatio);

            MinimumZ = GetZFromWidth(60 * Grid.Size);
            MaximumZ = GetZFromHeight(scene.Height);
            Z = MaximumZ;
            ZoomRate = 0.03f;

            Target = null;
            UpdateMatrices();
        }

        public Camera(Scene scene) : this(scene, 0, 0) { }

        public override void Update(GameTime gameTime)
        {
            if (Target is not null)
            {
                X = Target.X + Target.Width / 2;
                Y = Target.Y + Target.Height / 2;
            }

            if (KeyListener.IsKeyPressed(Keys.OemPlus))
            {
                Z -= ZoomRate * MathF.Pow(2, 0.01f * Z) * gameTime.ElapsedGameTime.Milliseconds;
            }
            if (KeyListener.IsKeyPressed(Keys.OemMinus))
            {
                Z += ZoomRate * MathF.Pow(2, 0.01f * Z) * gameTime.ElapsedGameTime.Milliseconds;
            }
        }

        public void UpdateMatrices()
        {
            Projection = Matrix.CreatePerspectiveFieldOfView(VFOV, AspectRatio, 1, MaximumZ);
            View = Matrix.CreateLookAt(new Vector3(0, 0, Z), Vector3.Zero, Vector3.Up);
            World = Matrix.CreateWorld(new Vector3(-X, -Y, 0), Vector3.Forward, Vector3.Up);
        }

        public void Follow(RenderableObject target)
        {
            Target = target;
        }

        public void GetExtents(out float width, out float height)
        {
            height = Z * MathF.Tan(0.5f * VFOV) * 2f;
            width = height * AspectRatio;
        }
        
        public void GetExtents(out float left, out float right, out float top, out float bottom)
        {
            GetExtents(out float width, out float height);
            left = X - width * 0.5f;
            right = left + width;
            top = Y - height * 0.5f;
            bottom = top + height;
        }

        public float GetZFromWidth(float width)
        {
            return 0.5f * width / MathF.Tan(0.5f * HFOV);
        }

        public float GetZFromHeight(float height)
        {
            return 0.5f * height / MathF.Tan(0.5f * VFOV);
        }

        public void ResetZ()
        {
            Z = MaximumZ;
        }

        private float _z;
    }
}
