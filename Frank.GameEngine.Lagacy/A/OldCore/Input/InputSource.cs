using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy.A.OldCore.Input;


public abstract class InputSource : IInputSource
{
    public abstract void Update(GameTime gameTime);
}