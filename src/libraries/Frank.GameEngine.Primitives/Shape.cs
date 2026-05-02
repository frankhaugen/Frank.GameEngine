namespace Frank.GameEngine.Primitives;

/// <summary>
///     Represents a shape. A shape is polygon with a color.
/// </summary>
public class Shape
{
    /// <summary>
    ///     A polygon is a shape of vertices.
    /// </summary>
    public Polygon Polygon { get; set; } = new();

    /// <summary>
    ///     Optional indexed triangle mesh (OBJ/FBX/glTF). When set, renderers draw this instead of fan-triangles from
    ///     <see cref="Polygon" />.
    /// </summary>
    public TriangleMesh? TriangleMesh { get; set; }

    /// <summary>
    ///     The color of the shape.
    /// </summary>
    public Rgba32 Color { get; set; } = Rgba32.White;

    /// <summary>
    ///     Gets a string representation of the shape.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{Polygon}";
    }
}