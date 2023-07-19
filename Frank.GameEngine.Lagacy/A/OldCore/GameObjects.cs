using System.Collections;

namespace Frank.GameEngine.Lagacy.A.OldCore;

public class GameObjects : IEnumerable<IGameObject>
{
    private readonly List<IGameObject> _gameObjects;

    public GameObjects(params IGameObject[] gameObjects) => _gameObjects = gameObjects.ToList();

    public IEnumerator<IGameObject> GetEnumerator() => _gameObjects.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}