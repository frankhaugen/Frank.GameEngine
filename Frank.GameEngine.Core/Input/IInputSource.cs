using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core.Input;

public interface IInputSource
{
    void Update(GameTime gameTime);
}