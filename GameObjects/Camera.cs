using Arcadia.GameWorld;
using Arcadia.Graphics;
using Arcadia.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Arcadia.GameObjects
{
    /// <summary>
    /// The <see cref="Camera"/> class is a representation of a camera. Any object that is being rendered will be displayed
    /// on the camera view.
    /// </summary>
    public sealed class Camera : GameObject
    {
        /// <summary>
        /// The scene of a camera.
        /// </summary>
        public Scene Scene { get; init; }

        /// <summary>
        /// The target to follow.
        /// </summary>
        public RenderableObject Target { get; private set; }

        /// <summary>
        /// The projection matrix.
        /// </summary>
        public Matrix Projection { get; private set; }

        /// <summary>
        /// The view matrix.
        /// </summary>
        public Matrix View { get; private set; }

        /// <summary>
        /// The world matrix.
        /// </summary>
        public Matrix World { get; private set; }

        /// <summary>
        /// The aspect ratio of the camera view.
        /// </summary>
        public float AspectRatio { get; set; }

        /// <summary>
        /// The vertical field of view.
        /// </summary>
        public float VFOV { get; set; }

        /// <summary>
        /// The horizontal field of view.
        /// </summary>
        public float HFOV { get; set; }

        /// <summary>
        /// The x-component of the center of the camera.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// The y-component of the center of the camera.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// The z-coordinate in units.
        /// </summary>
        public float Z
        {
            get => _z;
            set => _z = MathHelper.Clamp(value, MinimumZ, MaximumZ);
        }

        /// <summary>
        /// The minimum z-coordinate in units.
        /// </summary>
        public float MinimumZ { get; set; }

        /// <summary>
        /// The maximum z-coordinate in units.
        /// </summary>
        public float MaximumZ { get; set; }

        /// <summary>
        /// The zoom rate.
        /// </summary>
        public float ZoomRate { get; set; }

        /// <summary>
        /// Constructs a camera at (<paramref name="x"/>, <paramref name="y"/>).
        /// </summary>
        /// <param name="scene">The scene.</param>
        /// <param name="x">The x-coordinate in units.</param>
        /// <param name="y">The y-coordinate in units.</param>
        public Camera(Scene scene, float x, float y)
        {
            AspectRatio = (float) scene.Width / scene.Height;
            VFOV = MathHelper.PiOver2;
            HFOV = 2 * MathF.Atan(MathF.Tan(0.5f * VFOV) * AspectRatio);

            MinimumZ = GetZFromWidth(60 * Grid.Size);
            MaximumZ = GetZFromHeight(scene.Height);
            X = x;
            Y = y;
            Z = MaximumZ;
            ZoomRate = 0.03f;

            Scene = scene;
            Target = null;
            UpdateMatrices();
        }

        /// <summary>
        /// Constructs a camera at the origin (0, 0).
        /// </summary>
        /// <param name="scene"></param>
        public Camera(Scene scene) : this(scene, 0, 0) { }

        /// <summary>
        /// Updates the z-coordinate of the camera based on keyboard inputs, and updates the x- and y-coordinate of the
        /// camera by following a target.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            if (Target is not null)
            {
                X = Target.X + Target.Width * 0.5f;
                Y = Target.Y + Target.Height * 0.5f;
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

        /// <summary>
        /// Updates the projection, view, and world matrices.
        /// </summary>
        public void UpdateMatrices()
        {
            Projection = Matrix.CreatePerspectiveFieldOfView(VFOV, AspectRatio, 1, MaximumZ);
            View = Matrix.CreateLookAt(new Vector3(0, 0, Z), Vector3.Zero, Vector3.Up);
            World = Matrix.CreateWorld(new Vector3(-X, -Y, 0), Vector3.Forward, Vector3.Up);
        }

        /// <summary>
        /// The camera follows the given target.
        /// </summary>
        /// <param name="target">The target to follow.</param>
        public void Follow(RenderableObject target)
        {
            Target = target;
        }

        /// <summary>
        /// Gets the width and height of the camera.
        /// </summary>
        /// <param name="width">The width of the camera.</param>
        /// <param name="height">The height of the camera.</param>
        public void GetExtents(out float width, out float height)
        {
            height = Z * MathF.Tan(0.5f * VFOV) * 2f;
            width = height * AspectRatio;
        }
        
        /// <summary>
        /// Gets the left, right, top, and bottom position of the camera in units.
        /// </summary>
        /// <param name="left">The left coordinate in units.</param>
        /// <param name="right">The right coordinate in units.</param>
        /// <param name="top">The top coordinate in units.</param>
        /// <param name="bottom">The bottom coordinate in units.</param>
        public void GetExtents(out float left, out float right, out float top, out float bottom)
        {
            GetExtents(out float width, out float height);
            left = X - width * 0.5f;
            right = left + width;
            top = Y - height * 0.5f;
            bottom = top + height;
        }

        /// <summary>
        /// Gets the z-coordinate of a camera to see <paramref name="width"/> units.
        /// </summary>
        /// <param name="width">The width in units.</param>
        /// <returns>Returns the z-coordinate.</returns>
        public float GetZFromWidth(float width)
        {
            return 0.5f * width / MathF.Tan(0.5f * HFOV);
        }

        /// <summary>
        /// Gets the z-coordinate of a camera to see <paramref name="height"/> units.
        /// </summary>
        /// <param name="height">The height in units.</param>
        /// <returns>Returns the z-coordinate.</returns>
        public float GetZFromHeight(float height)
        {
            return 0.5f * height / MathF.Tan(0.5f * VFOV);
        }

        /// <summary>
        /// Resets the z-coordinate of a camera.
        /// </summary>
        public void ResetZ()
        {
            Z = MaximumZ;
        }

        private float _z;
    }
}
