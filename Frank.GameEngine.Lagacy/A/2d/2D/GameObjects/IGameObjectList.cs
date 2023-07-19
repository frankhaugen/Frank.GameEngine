namespace Frank.GameEngine.Lagacy.A._2d._2D.GameObjects;

public interface IGameObjectList
{
	void Add(IGameObject gameObject);
	void Remove(IGameObject gameObject);
	bool Exist(string name);
	IGameObject Get(string name);
	IEnumerable<IGameObject> GameObjects { get; }
	int Count { get; }
	void Clear();
}