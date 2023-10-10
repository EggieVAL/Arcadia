using Microsoft.Xna.Framework;

namespace Arcadia.GameObjects
{
    /// <summary>
    /// The <see cref="GameObject"/> interface is a skeleton of a game object. A game object only updates its
    /// state multiple times a frame.
    /// </summary>
    public interface GameObject
    {
        /// <summary>
        /// The update method is called multiple times a frame. This is where all calculations will be held,
        /// and this is where a game object's state is updated.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        void Update(GameTime gameTime);
    }
}
