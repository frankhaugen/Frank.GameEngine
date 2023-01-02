using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core;

public class GameWorld : IGameWorld
{
    private readonly IRenderer _renderer;
    private readonly IPhysics _physics;
    private readonly IGameObjectList _gameObjects = new GameObjectList();

    public GameWorld(IRenderer renderer, IPhysics physics)
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