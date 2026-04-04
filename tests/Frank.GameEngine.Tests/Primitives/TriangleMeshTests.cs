using System.Numerics;
using FluentAssertions;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Tests.Primitives;

public class TriangleMeshTests
{
    [Test]
    public void Constructor_RejectsInvalidIndex()
    {
        var verts = new[] { Vector3.Zero, Vector3.UnitX, Vector3.UnitY };
        var bad = new[] { 0, 1, 3 };
        var act = () => _ = new TriangleMesh(verts, bad);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void Translate_ShiftsBoundingBox()
    {
        var mesh = new TriangleMesh(
            [Vector3.Zero, Vector3.UnitX, Vector3.UnitY],
            [0, 1, 2]);
        var moved = mesh.Translate(new Vector3(10f, 0f, 0f));
        var (min, max) = moved.GetAxisAlignedBoundingBox();
        min.X.Should().BeApproximately(10f, 0.001f);
        max.X.Should().BeApproximately(11f, 0.001f);
    }

    [Test]
    public void GetFaces_YieldsOnePerTriangle()
    {
        var mesh = new TriangleMesh(
            [Vector3.Zero, Vector3.UnitX, Vector3.UnitY, Vector3.UnitZ],
            [0, 1, 2, 0, 2, 3]);
        mesh.GetFaces().Should().HaveCount(2);
    }

    [Test]
    public void Shape_WithMesh_TransformsVertices()
    {
        var shape = new Shape
        {
            Polygon = new Polygon(Array.Empty<Vector3>()),
            TriangleMesh = new TriangleMesh([Vector3.UnitZ], [0, 0, 0]),
            Color = Rgba32.White
        };
        var t = new Transform { Position = new Vector3(0f, 5f, 0f) };
        var world = shape.GetTransformedShape(t);
        world.TriangleMesh.Should().NotBeNull();
        world.TriangleMesh!.Vertices[0].Y.Should().BeApproximately(6f, 0.001f);
    }
}
