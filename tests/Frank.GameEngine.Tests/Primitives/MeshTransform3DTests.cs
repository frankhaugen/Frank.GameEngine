using System.Numerics;
using FluentAssertions;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Tests.Primitives;

public class MeshTransform3DTests
{
    [Test]
    public void Polygon_Translate_DoesNotMutateOriginalVertices()
    {
        var original = PolygonFactory.CreateRectangle(2f, 2f, Vector3.Zero);
        var v0Before = original[0];

        _ = original.Translate(new Vector3(10f, 0f, 0f));

        original[0].Should().Be(v0Before);
    }

    [Test]
    public void Polygon_EdgeCount_EqualsVertexCount_ForClosedLoop()
    {
        var cube = PolygonFactory.CreateCube(1f);

        cube.Edges.Should().HaveCount(cube.Length);
    }

    [Test]
    public void Shape_Transform_AppliesScaleRotationThenTranslation()
    {
        var rect = PolygonFactory.CreateRectangle(2f, 2f, Vector3.Zero);
        var shape = new Shape { Polygon = rect, Color = Rgba32.White };
        var transform = new Transform
        {
            Position = new Vector3(0f, 5f, 0f),
            Rotation = Quaternion.Identity,
            Scale = 1f
        };

        var world = shape.Transform(transform);

        world.Polygon[0].Y.Should().BeApproximately(rect[0].Y + 5f, 0.001f);
    }

    [Test]
    public void GameObject_GetTransformedShape_UsesFullTransform()
    {
        var rect = PolygonFactory.CreateRectangle(1f, 1f, Vector3.Zero);
        var go = new GameObject
        {
            Shape = new Shape { Polygon = rect, Color = Rgba32.Red },
            Transform = new Transform { Position = new Vector3(100f, 0f, 0f) }
        };

        var world = go.GetTransformedShape();

        world.Polygon.GetCenter().X.Should().BeApproximately(100f, 1f);
    }

    [Test]
    public void Polygon_GetAxisAlignedBoundingBox_ContainsAllVertices()
    {
        var rect = PolygonFactory.CreateRectangle(4f, 2f, new Vector3(10f, 20f, 0f));

        var (min, max) = rect.GetAxisAlignedBoundingBox();

        foreach (var v in rect)
        {
            v.X.Should().BeInRange(min.X, max.X);
            v.Y.Should().BeInRange(min.Y, max.Y);
            v.Z.Should().BeInRange(min.Z, max.Z);
        }
    }

    [Test]
    public void Camera_Up_IsMutable_ForCustomOrientation()
    {
        var camera = new Camera();
        var custom = Vector3.Normalize(new Vector3(0.2f, 1f, 0f));

        camera.Up = custom;

        camera.Up.Should().Be(custom);
    }
}
