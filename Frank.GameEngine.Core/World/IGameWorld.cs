using Frank.GameEngine.Core.GameObjects;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core.World;

public interface IGameWorld
{
	void AddGameObject(IGameObject gameObject);
	void Update(GameTime gameTime);
	void Render(GameTime gameTime);
	void Dispose();
}