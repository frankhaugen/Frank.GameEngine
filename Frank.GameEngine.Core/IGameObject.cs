namespace Frank.GameEngine.Core;

public interface IGameObject
{
    public string Name { get; set; }
    public ITransform Transform { get; set; }
    public IShape Shape { get; set; }
    public GameObjectOptions Options { get; set; }
}