using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core.Input;


public abstract class InputSource : IInputSource
{
    public abstract void Update(GameTime gameTime);
}