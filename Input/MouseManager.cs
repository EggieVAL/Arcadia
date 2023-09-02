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
        ///     Whether the left mouse button was clicked.
        /// </summary>
        /// <returns>Returns <c>true</c> if LMB was clicked.</returns>
        public bool IsLeftButtonClicked()
        {
            return _curr.LeftButton == ButtonState.Pressed &&
                   _prev.LeftButton == ButtonState.Released;
        }

        /// <summary>
        ///     Whether the left mouse button is down.
        /// </summary>
        /// <returns>Returns <c>true</c> if LMB is down.</returns>
        public bool IsLeftButtonDown()
        {
            return _curr.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        ///     Whether the middle mouse button was clicked.
        /// </summary>
        /// <returns>Returns <c>true</c> if MMB was clicked.</returns>
        public bool IsMiddleButtonClicked()
        {
            return _curr.MiddleButton == ButtonState.Pressed &&
                   _prev.MiddleButton == ButtonState.Released;
        }

        /// <summary>
        ///     Whether the middle mouse button is down.
        /// </summary>
        /// <returns>Returns <c>true</c> if MMB is down.</returns>
        public bool IsMiddleButtonDown()
        {
            return _curr.MiddleButton == ButtonState.Pressed;
        }

        /// <summary>
        ///     Whether the right mouse button was clicked.
        /// </summary>
        /// <returns>Returns <c>true</c> if RMB was clicked.</returns>
        public bool IsRightButtonClicked()
        {
            return _curr.RightButton == ButtonState.Pressed &&
                   _prev.RightButton == ButtonState.Released;
        }

        /// <summary>
        ///     Whether the right mouse button is down.
        /// </summary>
        /// <returns>Returns <c>true</c> if RMB is down.</returns>
        public bool IsRightButtonDown()
        {
            return _curr.RightButton == ButtonState.Pressed;
        }

        private MouseManager()
        {
            _prev = Mouse.GetState();
            _curr = _prev;
        }

        private MouseState _prev;
        private MouseState _curr;
    }
}
