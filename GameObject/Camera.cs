using Arcadia.GameWorld;
using Arcadia.Graphics;
using Arcadia.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Arcadia.GameObject
{
    /// <summary>
    ///     The <c>Camera</c> class is a representation of a camera that
    ///     captures a scene in the world. A camera can follow a renderable
    ///     game object or can be stationed at a specified position.
    /// </summary>
    public sealed class Camera : AGameObject
    {
        /// <summary>
        ///     The target the camera is following.
        /// </summary>
        public ARenderableObject Target
        {
            get => _target;
            private set => _target = value;
        }

        /// <summary>
        ///     The projection matrix.
        /// </summary>
        public Matrix Projection
        {
            get => _projection;
        }

        /// <summary>
        ///     The view matrix.
        /// </summary>
        public Matrix View
        {
            get => _view;
        }

        /// <summary>
        ///     The world matrix.
        /// </summary>
        public Matrix World
        {
            get => _world;
        }

        /// <summary>
        ///     The aspect ratio of the scene.
        /// </summary>
        public float AspectRatio
        {
            get => _aspectRatio;
            set => _aspectRatio = value;
        }

        /// <summary>
        ///     The vertical field of view.
        /// </summary>
        public float VFOV
        {
            get => _vfov;
            set => _vfov = value;
        }

        /// <summary>
        ///     The horizontal field of view.
        /// </summary>
        public float HFOV
        {
            get => _hfov;
            set => _hfov = value;
        }

        /// <summary>
        ///     The z-coordinate of the camera in units.
        /// </summary>
        public float Z
        {
            get => _z;
            set => _z = MathHelper.Clamp(value, MinZ, MaxZ);
        }

        /// <summary>
        ///     The minimum z-coordinate the camera can be at in units.
        /// </summary>
        public float MinZ
        {
            get => _minZ;
            set => _minZ = value;
        }

        /// <summary>
        ///     The maximum z-coordinate the camera can be at in units.
        /// </summary>
        public float MaxZ
        {
            get => _maxZ;
            set => _maxZ = value;
        }

        /// <summary>
        ///     The zoom rate of the camera in terms of time.
        /// </summary>
        public float ZoomRate
        {
            get => _zoomRate;
            set => _zoomRate = value;
        }

        /// <summary>
        ///     Constructs a camera capturing a scene at some position (x, y).
        /// </summary>
        /// <param name="scene">The scene a camera is capturing.</param>
        /// <param name="unitx">The x-coordinate in units.</param>
        /// <param name="unity">The y-coordinate in units.</param>
        public Camera(Scene scene, float unitx, float unity) : base(unitx, unity)
        {
            AspectRatio = (float) scene.Width / scene.Height;
            VFOV = MathHelper.PiOver2;
            HFOV = 2 * MathF.Atan(MathF.Tan(0.5f * VFOV) * AspectRatio);

            MinZ = GetZFromWidth(60 * Grid.Size);
            MaxZ = GetZFromHeight(scene.Height);
            Z = MaxZ;
            ZoomRate = 0.03f;

            Target = null;
            UpdateMatrices();
        }

        /// <summary>
        ///     Constructs a camera capturing a scene at position (0, 0).
        /// </summary>
        /// <param name="scene">The scene a camera is capturing.</param>
        public Camera(Scene scene) : this(scene, 0, 0) { }

        /// <summary>
        ///     This <c>Update</c> method updates the position of the
        ///     camera if it is following a target. The method also
        ///     checks key inputs for zooming-in and -out.
        /// </summary>
        /// <param name="gt">The time state of the game.</param>
        public override void Update(GameTime gt)
        {
            if (Target is not null)
            {
                UnitX = Target.UnitX + Target.Width / 2;
                UnitY = Target.UnitY + Target.Height / 2;
            }

            KeyManager manager = KeyManager.Instance;
            if (manager.IsKeyDown(Keys.OemPlus))
            {
                Z -= ZoomRate * MathF.Pow(2, 0.01f * Z) * gt.ElapsedGameTime.Milliseconds;
            }
            if (manager.IsKeyDown(Keys.OemMinus))
            {
                Z += ZoomRate * MathF.Pow(2, 0.01f * Z) * gt.ElapsedGameTime.Milliseconds;
            }
        }

        /// <summary>
        ///     Updates each matrices (i.e. projection, view, and world)
        ///     when called.
        /// </summary>
        public void UpdateMatrices()
        {
            _projection = Matrix.CreatePerspectiveFieldOfView(
                VFOV, AspectRatio, 1, MaxZ);
            _view = Matrix.CreateLookAt(new Vector3(0, 0, Z),
                Vector3.Zero, Vector3.Up);
            _world = Matrix.CreateWorld(new Vector3(-UnitX, -UnitY, 0),
                Vector3.Forward, Vector3.Up);
        }

        /// <summary>
        ///     Follows a renderable game object.
        /// </summary>
        /// <param name="target">A target the camera is following.</param>
        public void Follow(ARenderableObject target)
        {
            Target = target;
        }

        /// <summary>
        ///     Gets the extents of a scene a camera is capturing.
        /// </summary>
        /// <param name="width">The width of a scene in units.</param>
        /// <param name="height">The height of a scene in units.</param>
        public void GetExtents(out float width, out float height)
        {
            height = Z * MathF.Tan(0.5f * VFOV) * 2f;
            width = height * AspectRatio;
        }
        
        /// <summary>
        ///     Gets the extents of a scene a camera is capturing.
        /// </summary>
        /// <param name="left">The left bound of a scene in units.</param>
        /// <param name="right">THe right bound of a scene in units.</param>
        /// <param name="top">The top bound of a scene in units.</param>
        /// <param name="bot">The bottom bound of a scene in units.</param>
        public void GetExtents(out float left, out float right,
            out float top, out float bot)
        {
            GetExtents(out float width, out float height);
            left = UnitX - width * 0.5f;
            right = left + width;
            top = UnitY - height * 0.5f;
            bot = top + height;
        }

        /// <summary>
        ///     Calculates and returns the z-coordinate needed for the camera to
        ///     see <paramref name="width"/> units.
        /// </summary>
        /// <param name="width">The width in units.</param>
        /// <returns>
        ///     The z-coordinate needed to see <paramref name="width"/> units.
        /// </returns>
        public float GetZFromWidth(float width)
        {
            return 0.5f * width / MathF.Tan(0.5f * HFOV);
        }

        /// <summary>
        ///     Calculates and returns the z-coordinate needed for the camera to
        ///     see <paramref name="height"/> units.
        /// </summary>
        /// <param name="height">The height in units.</param>
        /// <returns>
        ///     The z-coordinate needed to see <paramref name="height"/> units.
        /// </returns>
        public float GetZFromHeight(float height)
        {
            return 0.5f * height / MathF.Tan(0.5f * VFOV);
        }

        /// <summary>
        ///     Resets the z-coordinate of a camera to its maximum z-coordinate.
        /// </summary>
        public void ResetZ()
        {
            Z = MaxZ;
        }

        private ARenderableObject _target;

        private Matrix _projection;
        private Matrix _view;
        private Matrix _world;

        private float _aspectRatio;
        private float _vfov;
        private float _hfov;

        private float _minZ;
        private float _maxZ;
        private float _z;

        private float _zoomRate;
    }
}
