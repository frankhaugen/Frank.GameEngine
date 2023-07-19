using System.Collections;
using System.Collections.Concurrent;

namespace Frank.GameEngine.Collections;

public class GameObjects : IEnumerable<GameObject>
{
    private readonly ConcurrentDictionary<Guid, GameObject> _gameObjects = new();

    public void Add(GameObject gameObject)
    {
        _gameObjects.TryAdd(gameObject.Id, gameObject);
    }

    public IEnumerator<GameObject> GetEnumerator()
    {
        return _gameObjects.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}