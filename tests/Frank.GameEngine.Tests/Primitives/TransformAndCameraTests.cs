using System.Numerics;
using FluentAssertions;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Tests.Primitives;

public class TransformAndCameraTests
{
    [Test]
    public void Translate_AddsToPosition()
    {
        var t = new Transform { Position = new Vector3(1f, 2f, 3f) };
        t.Translate(new Vector3(4f, 5f, 6f));
        t.Position.Should().Be(new Vector3(5f, 7f, 9f));
    }

    [Test]
    public void MoveTo_ReplacesPosition()
    {
        var t = new Transform { Position = Vector3.One };
        t.MoveTo(Vector3.Zero);
        t.Position.Should().Be(Vector3.Zero);
    }

    [Test]
    public void ScaleBy_MultipliesScale()
    {
        var t = new Transform { Scale = 2f };
        t.ScaleBy(3f);
        t.Scale.Should().Be(6f);
    }

    [Test]
    public void Camera_GetViewMatrix_IsNotDefault()
    {
        var camera = new Camera
        {
            Position = new Vector3(0f, 0f, 5f),
            Target = Vector3.Zero
        };

        var view = camera.GetViewMatrix();

        view.Should().NotBe(default(Matrix4x4));
    }

    [Test]
    public void Camera_GetProjectionMatrix_IsNotDefault()
    {
        var camera = new Camera { FieldOfView = 60f, AspectRatio = 16f / 9f };

        var proj = camera.GetProjectionMatrix();

        proj.Should().NotBe(default(Matrix4x4));
    }

    [Test]
    public void Camera_MoveForward_TranslatesPositionAndTarget()
    {
        var camera = new Camera
        {
            Position = new Vector3(0f, 0f, 10f),
            Target = Vector3.Zero
        };
        var beforeTarget = camera.Target;

        camera.MoveForward(1f);

        camera.Position.Z.Should().BeLessThan(10f);
        (camera.Target - beforeTarget).Length().Should().BeGreaterThan(0f);
    }
}
