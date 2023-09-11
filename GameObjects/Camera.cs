using Arcadia.GameWorld;
using Arcadia.Graphics;
using Arcadia.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Arcadia.GameObject
{
    /// <summary>
    /// The <c>Camera</c> class is a representation of a camera that captures a scene in the world.
    /// A camera can follow a renderable game object or can be stationed
    /// at a specified position.
    /// </summary>
    public sealed class Camera : RenderableObject
    {
        /// <summary>
        /// The target the camera is following.
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
        /// The aspect ratio of the scene.
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
        /// The z-coordinate of the camera in units.
        /// </summary>
        public float Z
        {
            get => _z;
            set => _z = MathHelper.Clamp(value, MinimumZ, MaximumZ);
        }

        /// <summary>
        /// The minimum z-coordinate the camera can be at in units.
        /// </summary>
        public float MinimumZ { get; set; }

        /// <summary>
        /// The maximum z-coordinate the camera can be at in units.
        /// </summary>
        public float MaximumZ { get; set; }

        /// <summary>
        /// The zoom rate of the camera.
        /// </summary>
        public float ZoomRate { get; set; }

        /// <summary>
        /// Constructs a camera capturing a scene at some position (x, y).
        /// </summary>
        /// <param name="scene">The scene a camera is capturing.</param>
        /// <param name="x">The x-coordinate in units.</param>
        /// <param name="y">The y-coordinate in units.</param>
        public Camera(Scene scene, float x, float y) : base(null,
            new Rectangle((int) x, (int) y, 0, 0))
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

        /// <summary>
        /// Constructs a camera capturing a scene at position (0, 0).
        /// </summary>
        /// <param name="scene">The scene a camera is capturing.</param>
        public Camera(Scene scene) : this(scene, 0, 0) { }

        /// <summary>
        /// Updates the position of the camera according to key input(s) and if the camera is following
        /// a target.
        /// </summary>
        /// <param name="gameTime">The time state of the game.</param>
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

        /// <summary>
        /// Updates each matrices (i.e. projection, view, and world) when called.
        /// </summary>
        public void UpdateMatrices()
        {
            Projection = Matrix.CreatePerspectiveFieldOfView(VFOV, AspectRatio, 1, MaximumZ);
            View = Matrix.CreateLookAt(new Vector3(0, 0, Z), Vector3.Zero, Vector3.Up);
            World = Matrix.CreateWorld(new Vector3(-X, -Y, 0), Vector3.Forward, Vector3.Up);
        }

        /// <summary>
        /// Follows a renderable game object.
        /// </summary>
        /// <param name="target">A target the camera is following.</param>
        public void Follow(RenderableObject target)
        {
            Target = target;
        }

        /// <summary>
        /// Gets the extents of a scene a camera is capturing.
        /// </summary>
        /// <param name="width">The width of a scene in units.</param>
        /// <param name="height">The height of a scene in units.</param>
        public void GetExtents(out float width, out float height)
        {
            height = Z * MathF.Tan(0.5f * VFOV) * 2f;
            width = height * AspectRatio;
        }
        
        /// <summary>
        /// Gets the extents of a scene a camera is capturing.
        /// </summary>
        /// <param name="left">The left bound of a scene in units.</param>
        /// <param name="right">THe right bound of a scene in units.</param>
        /// <param name="top">The top bound of a scene in units.</param>
        /// <param name="bottom">The bottom bound of a scene in units.</param>
        public void GetExtents(out float left, out float right, out float top, out float bottom)
        {
            GetExtents(out float width, out float height);
            left = X - width * 0.5f;
            right = left + width;
            top = Y - height * 0.5f;
            bottom = top + height;
        }

        /// <summary>
        /// Calculates the z-coordinate needed for the camera to see <paramref name="width"/>.
        /// </summary>
        /// <param name="width">The width in units.</param>
        /// <returns>
        /// The z-coordinate needed to see <paramref name="width"/>.
        /// </returns>
        public float GetZFromWidth(float width)
        {
            return 0.5f * width / MathF.Tan(0.5f * HFOV);
        }

        /// <summary>
        /// Calculates the z-coordinate needed for the camera to see <paramref name="height"/>.
        /// </summary>
        /// <param name="height">The height in units.</param>
        /// <returns>
        /// The z-coordinate needed to see <paramref name="height"/>.
        /// </returns>
        public float GetZFromHeight(float height)
        {
            return 0.5f * height / MathF.Tan(0.5f * VFOV);
        }

        /// <summary>
        /// Resets the z-coordinate of a camera to its maximum z-coordinate.
        /// </summary>
        public void ResetZ()
        {
            Z = MaximumZ;
        }

        private float _z;
    }
}
