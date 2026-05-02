using System.Numerics;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Assets;

public static class ModelsAssets
{
    /// <summary>
    ///     Loads the embedded teapot as a correct indexed mesh (OBJ face data).
    /// </summary>
    public static TriangleMesh GetTeapotMesh()
    {
        var bytes = AdditionalResources2.Models.teapot;
        return ObjParser.ParseTriangleMesh(bytes.AsMemory());
    }

    /// <summary>
    ///     Teapot as a <see cref="Shape" /> with <see cref="Shape.TriangleMesh" /> set (empty <see cref="Shape.Polygon" />).
    /// </summary>
    public static Shape GetTeapotShape(Rgba32? color = null)
    {
        return new Shape
        {
            Polygon = new Polygon(Array.Empty<Vector3>()),
            TriangleMesh = GetTeapotMesh(),
            Color = color ?? Rgba32.Chartreuse
        };
    }
}
