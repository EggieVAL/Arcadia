using Arcadia.GameWorld;
using Microsoft.Xna.Framework;

namespace Arcadia.GameObject
{
    /// <summary>
    ///     The <c>AGameObject</c> class is an abstraction of a game object. A game
    ///     object comprises of many things such as tiles, characters, and items.
    /// </summary>
    public abstract class AGameObject
    {
        /// <summary>
        ///     The x-coordinate of a game object in units.
        /// </summary>
        public float UnitX
        {
            get => _unitx;
            set => _unitx = value;
        }

        /// <summary>
        ///     The y-coordinate of a game object in units.
        /// </summary>
        public float UnitY
        {
            get => _unity;
            set => _unity = value;
        }

        /// <summary>
        ///     The position of a game object in units.
        /// </summary>
        public Vector2 UnitPos
        {
            get => new Vector2(UnitX, UnitY);
        }

        /// <summary>
        ///     The x-coordinate of a game object in the grid world.
        /// </summary>
        public int X
        {
            get => Grid.GetPosX(UnitX);
        }

        /// <summary>
        ///     The y-coordinate of a game object in the grid world.
        /// </summary>
        public int Y
        {
            get => Grid.GetPosY(UnitY);
        }

        /// <summary>
        ///     The position of a game object in the grid world.
        /// </summary>
        public Vector2 Pos
        {
            get => new Vector2(X, Y);
        }

        /// <summary>
        ///     Constructs a game object at some (x, y) position.
        /// </summary>
        /// <param name="unitx">The x-coordinate in units.</param>
        /// <param name="unity">The y-coordinate in units.</param>
        public AGameObject(float unitx, float unity)
        {
            UnitX = unitx;
            UnitY = unity;
        }

        /// <summary>
        ///     The <c>Update</c> method is called multiple times a second, updating
        ///     the state of a character.
        /// </summary>
        /// <param name="gt">The time state of the game.</param>
        public abstract void Update(GameTime gt);

        private float _unitx;
        private float _unity;
    }
}
