using Microsoft.Xna.Framework;

namespace Frank.GameEngine;

public class GameObjectUpdater
{
    public void Update(GameObject gameObject, GameTime gameTime)
    {
        if (gameObject.Options.IsPhysical)
        {
            gameObject.Transform.Position += gameObject.PhysicalProperties.Velocity * gameTime.ElapsedGameTime.Milliseconds;
        }
    }
}