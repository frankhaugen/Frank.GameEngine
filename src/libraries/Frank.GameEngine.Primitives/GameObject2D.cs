namespace Frank.GameEngine.Primitives;

/// <summary>
///     2D entity: lower <see cref="ZOrder" /> draws first (behind higher values).
/// </summary>
public sealed class GameObject2D
{
    public Guid Id { get; } = Guid.NewGuid();

    public string? Name { get; set; }

    public bool IsActive { get; set; } = true;

    public int ZOrder { get; set; }

    public Transform2D Transform { get; set; } = new();

    public Sprite2D Sprite { get; set; } = new();
}
