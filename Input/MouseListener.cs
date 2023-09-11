using Arcadia.GameObject;
using Arcadia.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Arcadia.Input
{
    /// <summary>
    /// The <c>MouseListener</c> is a singleton class that handles mouse inputs.
    /// </summary>
    public sealed class MouseListener
    {
        /// <summary>
        /// The single instance of this class.
        /// </summary>
        public static readonly MouseListener Instance = new MouseListener();

        /// <summary>
        /// Ideally, what the mouse state is right now.
        /// </summary>
        /// <see cref="Update"/>
        public MouseState CurrentState { get; private set; }

        /// <summary>
        /// Ideally, what the mouse state was a frame before.
        /// </summary>
        /// <see cref="Update"/>
        public MouseState PreviousState { get; private set; }

        /// <summary>
        /// Ideally, what the x-position of the mouse is right now.
        /// </summary>
        /// <see cref="Update"/>
        public int PositionX => CurrentState.X;

        /// <summary>
        /// Ideally, what the y-position of the mouse is right now.
        /// </summary>
        /// <see cref="Update"/>
        public int PositionY => CurrentState.Y;

        /// <summary>
        /// Whether the left mouse button (LMB) is clicked.
        /// </summary>
        /// <returns>Returns true if LMB is clicked.</returns>
        public static bool IsLeftButtonClicked()
        {
            return Instance.CurrentState.LeftButton == ButtonState.Pressed &&
                   Instance.PreviousState.LeftButton == ButtonState.Released;
        }

        /// <summary>
        /// Whether the left mouse button (LMB) is pressed.
        /// </summary>
        /// <returns>Returns true if LMB is pressed.</returns>
        public static bool IsLeftButtonPressed()
        {
            return Instance.CurrentState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Whether the middle mouse button (MMB) is clicked.
        /// </summary>
        /// <returns>Returns true if MMB is clicked.</returns>
        public static bool IsMiddleButtonClicked()
        {
            return Instance.CurrentState.MiddleButton == ButtonState.Pressed &&
                   Instance.PreviousState.MiddleButton == ButtonState.Released;
        }

        /// <summary>
        /// Whether the middle mouse button (MMB) is pressed.
        /// </summary>
        /// <returns>Returns true if MMB is pressed.</returns>
        public static bool IsMiddleButtonPressed()
        {
            return Instance.CurrentState.MiddleButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Whether the right mouse button (RMB) is clicked.
        /// </summary>
        /// <returns>Returns true if RMB is clicked.</returns>
        public static bool IsRightButtonClicked()
        {
            return Instance.CurrentState.RightButton == ButtonState.Pressed &&
                   Instance.PreviousState.RightButton == ButtonState.Released;
        }

        /// <summary>
        /// Whether the right mouse button (RMB) is pressed.
        /// </summary>
        /// <returns>Returns true if RMB is pressed.</returns>
        public static bool IsRightButtonDown()
        {
            return Instance.CurrentState.RightButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Whether the scroll wheel is scrolling up.
        /// </summary>
        /// <returns>Returns true if the scroll wheel is scrolling up.</returns>
        public static bool IsScrollingUp()
        {
            return Instance.CurrentState.ScrollWheelValue >
                   Instance.PreviousState.ScrollWheelValue;
        }

        /// <summary>
        /// Whether the scroll wheel is scrolling down.
        /// </summary>
        /// <returns>Returns true if the scroll wheel is scrolling down.</returns>
        public static bool IsScrollingDown()
        {
            return Instance.CurrentState.ScrollWheelValue <
                   Instance.PreviousState.ScrollWheelValue;
        }

        /// <summary>
        /// Updates the previous mouse state to the current mouse state.
        /// Updates the current mouse state to the mouse state right now.
        /// The update method should be called multiple times a second for
        /// accurate results.
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
