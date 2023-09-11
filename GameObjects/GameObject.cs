using Microsoft.Xna.Framework;

namespace Arcadia.GameObject
{
    /// <summary>
    /// The <c>IGameObject</c> class is a skeleton of a game object. A game object comprises of
    /// many things such as tiles, characters, and items.
    /// </summary>
    public interface GameObject
    {
        /// <summary>
        /// The update method is called multiple times a second, updating the state of a character.
        /// </summary>
        /// <param name="gameTime">The time state of the game.</param>
        void Update(GameTime gameTime);
    }
}
