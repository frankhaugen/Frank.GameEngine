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
        var collissions = new List<Collission>();
        foreach (var gameObject in _gameObjects.GameObjects)
        {
            _physics.Update(gameObject, gameTime.ElapsedGameTime);

            foreach (var other in _gameObjects.GameObjects)
            {
                if (gameObject != other && gameObject.Polygon.Intersects(other.Polygon))
                {
                    collissions.Add(new Collission
                    {
                        GameObject1 = gameObject.Name,
                        GameObject2 = other.Name,
                        // Calculate the force of the collission here
                    });
                }
            }
        }
    }

    public void Render(GameTime gameTime)
    {
        foreach (var gameObject in _gameObjects.GameObjects)
        {
            _renderer.Render(gameObject);
        }
    }


    public void Dispose()
    {
        _renderer.Dispose();
    }
}