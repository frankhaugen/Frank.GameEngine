using System.Linq;
using System.Numerics;
using FluentAssertions;
using Frank.GameEngine.Primitives;
using Frank.GameEngine.Rendering.Console;
using TUnit.Core;

namespace Frank.GameEngine.Tests.Primitives;

public class PolygonTest
{
    [Test]
    public void TestPolygonIntersection2D()
    {
        var polygon1 = PolygonFactory.CreateRectangle(4, 4, new Vector3(-1, 0, 0));
        var polygon2 = PolygonFactory.CreateRectangle(4, 4, new Vector3(1, 1, 0));

        var intersections = polygon1.GetIntersectionPoints(polygon2).ToList();

        var drawerString = new ConsoleDrawer(10, 10, 5, 5)
            .WithPoints(polygon1, new CharColor('X', ConsoleColor.DarkMagenta))
            .WithPoints(polygon2, new CharColor('Y', ConsoleColor.DarkGreen))
            .ToString();

        TestContext.Current!.Output.WriteLine(drawerString);
        TestContext.Current.Output.WriteLine(string.Join(Environment.NewLine, intersections.Select(v => v.ToString("F"))));

        intersections.Should().NotBeEmpty();
    }

    [Test]
    public void TestPolygonIntersection3D()
    {
        var polygon1 = PolygonFactory.CreateCube(10);
        polygon1.Translate(new Vector3(5, 0, 0));
        var polygon2 = PolygonFactory.CreateCube(10);
        polygon2.Translate(new Vector3(0, 0, 0));

        var intersections = polygon1.GetIntersectionPoints(polygon2).ToList();

        TestContext.Current!.Output.WriteLine(PrimitiveSerializers.SerializeVector3s(intersections, "F"));
        intersections.Should().NotBeEmpty();
    }
}
