namespace Frank.GameEngine.Core._2D.GameObjects;

public class GameObjectList : Dictionary<string, IGameObject>, IGameObjectList
{
	public void Add(IGameObject gameObject) => Add(gameObject.Name, gameObject);
	public void Remove(IGameObject gameObject) => Remove(gameObject.Name);
	public bool Exist(string name) => ContainsKey(name);
	public IGameObject Get(string name) => this[name];
	public IEnumerable<IGameObject> GameObjects => Values;
}