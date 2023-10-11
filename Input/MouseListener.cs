using Arcadia.GameObjects;
using Arcadia.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Arcadia.Input
{
    /// <summary>
    /// The <see cref="MouseListener"/> class is a singleton class that listens to mouse inputs. You can check whether a mouse button is clicked or pressed.
    /// </summary>
    public sealed class MouseListener
    {
        /// <summary>
        /// The single instance of this class.
        /// </summary>
        public static readonly MouseListener Instance = new MouseListener();

        /// <summary>
        /// Ideally, the state of the mouse right now; Depends if the listener is being updated in real-time.
        /// </summary>
        /// <seealso cref="Update"/>
        public MouseState CurrentState { get; private set; }

        /// <summary>
        /// The previous state of the mouse.
        /// </summary>
        public MouseState PreviousState { get; private set; }

        /// <summary>
        /// The x-coordinate of the current mouse state relative to the screen.
        /// </summary>
        public int PositionX => CurrentState.X;

        /// <summary>
        /// The y-coordinate of the current mouse state relative to the screen.
        /// </summary>
        public int PositionY => CurrentState.Y;

        /// <summary>
        /// Gets the position of the mouse relative to the screen.
        /// </summary>
        /// <param name="x">The x-coordinate of the mouse, in units, relative to the screen.</param>
        /// <param name="y">The y-coordinate of the mouse, in units, relative to the screen.</param>
        public static void PositionRelativeToScreen(out float x, out float y)
        {
            x = Instance.PositionX;
            y = Instance.PositionY;
        }

        /// <summary>
        /// Gets the position of the mouse relative to a <paramref name="scene"/>.
        /// </summary>
        /// <param name="scene">The scene.</param>
        /// <param name="x">The x-coordinate of the mouse, in units, relative to a scene.</param>
        /// <param name="y">The y-coordinate of the mouse, in units, relative to a scene.</param>
        public static void PositionRelativeToScene(Scene scene, out float x, out float y)
        {
            PositionRelativeToScreen(out float xRelativeToScreen, out float yRelativeToScreen);
            Rectangle targetDestination = scene.GetRenderTargetDestination();

            x = xRelativeToScreen - targetDestination.X;
            y = yRelativeToScreen - targetDestination.Y;

            x *= (float) scene.Width / targetDestination.Width;
            y *= (float) scene.Height / targetDestination.Height;
        }

        /// <summary>
        /// Gets the position of the mouse relative to a <paramref name="camera"/>.
        /// </summary>
        /// <param name="camera">The camera.</param>
        /// <param name="x">The x-coordinate of the mouse, in units, relative to a camera.</param>
        /// <param name="y">The y-coordinate of the mouse, in units, relative to a camera.</param>
        public static void PositionRelativeToCamera(Camera camera, out float x, out float y)
        {
            PositionRelativeToScene(camera.Scene, out float xRelativeToScene, out float yRelativeToScene);
            camera.GetExtents(out float left, out _, out float top, out _);
            camera.GetExtents(out float width, out float height);

            float widthRatio = width / camera.Scene.Width;
            float heightRatio = height / camera.Scene.Height;

            x = left + xRelativeToScene * widthRatio;
            y = top + yRelativeToScene * heightRatio;
        }

        /// <summary>
        /// Is the left mouse button being clicked by the user?
        /// </summary>
        /// <returns><c>true</c> if the left mouse button was up previously, but is now down; otherwise <c>false</c>.</returns>
        public static bool IsLeftButtonClicked()
        {
            return Instance.CurrentState.LeftButton == ButtonState.Pressed &&
                   Instance.PreviousState.LeftButton == ButtonState.Released;
        }

        /// <summary>
        /// Is the left mouse button being pressed by the user?
        /// </summary>
        /// <returns><c>true</c> if the left mouse button is down; otherwise <c>false</c>.</returns>
        public static bool IsLeftButtonPressed()
        {
            return Instance.CurrentState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Is the middle mouse button being clicked by the user?
        /// </summary>
        /// <returns><c>true</c> if the middle mouse button was up previously, but is now down; otherwise <c>false</c>.</returns>
        public static bool IsMiddleButtonClicked()
        {
            return Instance.CurrentState.MiddleButton == ButtonState.Pressed &&
                   Instance.PreviousState.MiddleButton == ButtonState.Released;
        }

        /// <summary>
        /// Is the middle mouse button being pressed by the user?
        /// </summary>
        /// <returns><c>true</c> if the middle mouse button is down; otherwise <c>false</c>.</returns>
        public static bool IsMiddleButtonPressed()
        {
            return Instance.CurrentState.MiddleButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Is the right mouse button being clicked by the user?
        /// </summary>
        /// <returns><c>true</c> if the right mouse button was up previously, but is now down; otherwise <c>false</c>.</returns>
        public static bool IsRightButtonClicked()
        {
            return Instance.CurrentState.RightButton == ButtonState.Pressed &&
                   Instance.PreviousState.RightButton == ButtonState.Released;
        }

        /// <summary>
        /// Is the right mouse button being pressed by the user?
        /// </summary>
        /// <returns><c>true</c> if the right mouse button is down; otherwise <c>false</c>.</returns>
        public static bool IsRightButtonDown()
        {
            return Instance.CurrentState.RightButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Is the user scrolling up?
        /// </summary>
        /// <returns><c>true</c> if the user is scrolling up; otherwise <c>false</c>.</returns>
        public static bool IsScrollingUp()
        {
            return Instance.CurrentState.ScrollWheelValue >
                   Instance.PreviousState.ScrollWheelValue;
        }

        /// <summary>
        /// Is the user scrolling down?
        /// </summary>
        /// <returns><c>true</c> if the user is scrolling down; otherwise <c>false</c>.</returns>
        public static bool IsScrollingDown()
        {
            return Instance.CurrentState.ScrollWheelValue <
                   Instance.PreviousState.ScrollWheelValue;
        }

        /// <summary>
        /// Updates the mouse state. Ideally, this should be called multiple times a frame to get real-time results.
        /// </summary>
        public static void Update()
        {
            Instance.PreviousState = Instance.CurrentState;
            Instance.CurrentState = Mouse.GetState();
        }

        private MouseListener()
        {
            PreviousState = Mouse.GetState();
            CurrentState = PreviousState;
        }
    }
}
