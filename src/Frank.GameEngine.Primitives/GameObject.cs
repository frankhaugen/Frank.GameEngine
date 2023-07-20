using System.Numerics;

namespace Frank.GameEngine.Primitives;

public class GameObject
{
    public Guid Id { get; } = Guid.NewGuid();

    public string? Name { get; set; }

    public bool IsActive { get; set; } = true;

    public Transform Transform { get; set; } = new();

    public Shape Shape { get; set; } = new();
    
    public Rigidbody Rigidbody { get; set; } = new();

    public override string ToString() => $"{Name} - {Shape}";

    public void MoveUp(float speed)
    {
        Transform.Translate(new Vector3(Transform.Position.X, Transform.Position.Y - speed, 0));
    }

    public void MoveDown(float speed)
    {
        Transform.Translate(new Vector3(Transform.Position.X, Transform.Position.Y + speed, 0));
    }
}