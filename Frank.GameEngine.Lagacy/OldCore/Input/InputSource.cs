using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy.OldCore.Input;


public abstract class InputSource : IInputSource
{
    public abstract void Update(GameTime gameTime);
}