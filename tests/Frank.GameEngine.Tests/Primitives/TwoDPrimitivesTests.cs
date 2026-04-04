using System.Numerics;
using FluentAssertions;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Tests.Primitives;

public class TwoDPrimitivesTests
{
    [Test]
    public void Camera2D_WorldToScreen_WithIdentityZoom_CentersTarget()
    {
        var cam = new Camera2D
        {
            Target = Vector2.Zero,
            Offset = new Vector2(400f, 300f),
            Zoom = 1f,
            RotationDegrees = 0f,
            ViewportWidth = 800,
            ViewportHeight = 600
        };

        var screen = cam.WorldToScreen(Vector2.Zero);
        screen.X.Should().BeApproximately(400f, 0.01f);
        screen.Y.Should().BeApproximately(300f, 0.01f);
    }

    [Test]
    public void Scene2D_GetActiveSorted_RespectsZOrder()
    {
        var scene = new Scene2D("t", new Camera2D());
        scene.GameObjects.Add(new GameObject2D { ZOrder = 10, Name = "back" });
        scene.GameObjects.Add(new GameObject2D { ZOrder = 1, Name = "front" });
        var order = scene.GetActiveSorted().Select(x => x.Name).ToList();
        order[0].Should().Be("front");
        order[1].Should().Be("back");
    }

    [Test]
    public void GameObject2D_Aabb_ContainsPivot()
    {
        var go = new GameObject2D
        {
            Transform = new Transform2D { Position = new Vector2(100f, 200f) },
            Sprite = new Sprite2D { Size = new Vector2(20f, 20f), Origin = new Vector2(0.5f, 0.5f) }
        };
        var (min, max) = go.GetAxisAlignedBoundingBox();
        min.X.Should().BeLessOrEqualTo(100f);
        max.X.Should().BeGreaterOrEqualTo(100f);
        min.Y.Should().BeLessOrEqualTo(200f);
        max.Y.Should().BeGreaterOrEqualTo(200f);
    }
}
