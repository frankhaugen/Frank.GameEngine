namespace Frank.GameEngine;

public class GameLevel
{
    public GameLevel(string name, IEnumerable<IGameObject> gameObjects)
    {
        Name = name;
        GameObjects = gameObjects;
    }

    public string Name { get; }
    public IEnumerable<IGameObject> GameObjects { get; }
}