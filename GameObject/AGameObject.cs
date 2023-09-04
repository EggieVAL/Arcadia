using Arcadia.GameWorld;
using Microsoft.Xna.Framework;
using System;

namespace Arcadia.GameObject
{
    /// <summary>
    ///     The <c>AGameObject</c> class is an abstraction of a game object. A game
    ///     object comprises of many things such as tiles, characters, and items.
    /// </summary>
    public abstract class AGameObject
    {
        /// <summary>
        ///     The <c>Update</c> method is called multiple times a second, updating
        ///     the state of a character.
        /// </summary>
        /// <param name="gt">The time state of the game.</param>
        public abstract void Update(GameTime gt);
    }
}
