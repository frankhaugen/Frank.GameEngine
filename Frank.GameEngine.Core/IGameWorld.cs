using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core;

public interface IGameWorld
{
    void AddGameObject(IGameObject gameObject);
    void Update(GameTime gameTime);
    void Render(GameTime gameTime);
    void Dispose();

}