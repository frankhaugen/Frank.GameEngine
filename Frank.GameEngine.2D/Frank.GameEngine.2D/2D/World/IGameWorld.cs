using Frank.GameEngine.Core._2D.GameObjects;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core._2D.World;

public interface IGameWorld
{
	void AddGameObject(IGameObject gameObject);
	void Update(GameTime gameTime);
	void Render(GameTime gameTime);
	void Dispose();
}