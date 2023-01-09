using Frank.GameEngine.Core._2D.GameObjects;
using Frank.GameEngine.Core._2D.Physics;
using Frank.GameEngine.Core._2D.Rendering;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core._2D.World;

public class GameWorld : IGameWorld
{
	private readonly IRenderer _renderer;
	private readonly IPhysicsEngine _physics;
	private readonly IGameObjectList _gameObjects = new GameObjectList();
	// private readonly InputHandler _inputHandler = new InputHandler();

	public GameWorld(IRenderer renderer, IPhysicsEngine physics)
	{
		_renderer = renderer;
		_physics = physics;
	}

	public void AddGameObject(IGameObject gameObject)
	{
		_gameObjects.Add(gameObject);
	}

	public void Update(GameTime gameTime)
	{
		// _inputHandler.Update(gameTime);
		foreach (var gameObject in _gameObjects.GameObjects.Where(x => x.PhysicsEnebled))
		{
			_physics.Update(gameObject, gameTime.ElapsedGameTime);
		}
	}

	public void Render(GameTime gameTime)
	{
		foreach (var gameObject in _gameObjects.GameObjects.Where(x => !float.IsNaN(x.Position.X) && !float.IsNaN(x.Position.Y)))
		{
			_renderer.Render(gameObject);
		}
	}

	public void Dispose()
	{
		_renderer.Dispose();
	}
}