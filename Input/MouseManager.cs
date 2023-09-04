using Arcadia.GameObject;
using Arcadia.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Arcadia.Input
{
    /// <summary>
    ///     The <c>MouseManager</c> is a singleton class that handles
    ///     mouse inputs.
    /// </summary>
    public sealed class MouseManager
    {
        /// <summary>
        ///     An instance of a <c>MouseManager</c>.
        /// </summary>
        public static readonly MouseManager Instance = new MouseManager();

        /// <summary>
        ///     The previous mouse state.
        /// </summary>
        public MouseState PreviousState { get; private set; }

        /// <summary>
        ///     The current mouse state.
        /// </summary>
        public MouseState CurrentState { get; private set; }

        /// <summary>
        ///     The current x-position of the mouse.
        /// </summary>
        public int PosX => CurrentState.X;
        
        /// <summary>
        ///     The current y-position of the mouse.
        /// </summary>
        public int PosY => CurrentState.Y;

        /// <summary>
        ///     Whether the left mouse button was clicked.
        /// </summary>
        /// <returns>Returns <c>true</c> if LMB was clicked.</returns>
        public bool IsLeftButtonClicked()
        {
            return CurrentState.LeftButton == ButtonState.Pressed &&
                   PreviousState.LeftButton == ButtonState.Released;
        }

        /// <summary>
        ///     Whether the left mouse button is down.
        /// </summary>
        /// <returns>Returns <c>true</c> if LMB is down.</returns>
        public bool IsLeftButtonDown()
        {
            return CurrentState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        ///     Whether the middle mouse button was clicked.
        /// </summary>
        /// <returns>Returns <c>true</c> if MMB was clicked.</returns>
        public bool IsMiddleButtonClicked()
        {
            return CurrentState.MiddleButton == ButtonState.Pressed &&
                   PreviousState.MiddleButton == ButtonState.Released;
        }

        /// <summary>
        ///     Whether the middle mouse button is down.
        /// </summary>
        /// <returns>Returns <c>true</c> if MMB is down.</returns>
        public bool IsMiddleButtonDown()
        {
            return CurrentState.MiddleButton == ButtonState.Pressed;
        }

        /// <summary>
        ///     Whether the right mouse button was clicked.
        /// </summary>
        /// <returns>Returns <c>true</c> if RMB was clicked.</returns>
        public bool IsRightButtonClicked()
        {
            return CurrentState.RightButton == ButtonState.Pressed &&
                   PreviousState.RightButton == ButtonState.Released;
        }

        /// <summary>
        ///     Whether the right mouse button is down.
        /// </summary>
        /// <returns>Returns <c>true</c> if RMB is down.</returns>
        public bool IsRightButtonDown()
        {
            return CurrentState.RightButton == ButtonState.Pressed;
        }

        /// <summary>
        ///     Updates the previous and current mouse state.
        /// </summary>
        public void Update()
        {
            PreviousState = CurrentState;
            CurrentState = Mouse.GetState();
        }

        private MouseManager()
        {
            PreviousState = Mouse.GetState();
            CurrentState = PreviousState;
        }
    }
}
