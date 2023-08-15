using System.Numerics;
using Frank.GameEngine.Primitives;
using Frank.GameEngine.Rendering.Console;
using Frank.Libraries.Extensions;
using Xunit.Abstractions;

namespace Frank.GameEngine.Tests.Primitives;

public class PolygonTest
{
    private readonly ITestOutputHelper _outputHelper;

    public PolygonTest(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public void TestPolygonIntersection2D()
    {
        // Instantiate your 2d polygons here. Z axis is 0 for all points

        var polygon1 = PolygonFactory.CreateRectangle(4, 4, new Vector3(-1, 0, 0));
        var polygon2 = PolygonFactory.CreateRectangle(4, 4, new Vector3(1, 1, 0));

        var intersections = polygon1.GetIntersectionPoints(polygon2).ToList();
        
        var drawerString = new ConsoleDrawer(10, 10, 5, 5)
            .WithPoints(polygon1, new CharColor('X', ConsoleColor.DarkMagenta))
            .WithPoints(polygon2, new CharColor('Y', ConsoleColor.DarkGreen))
            .ToString();
        
        _outputHelper.WriteLine(drawerString);
        _outputHelper.WriteLine(intersections.ToString("F"));

        Assert.NotEmpty(intersections);
    }

    [Fact]
    public void TestPolygonIntersection3D()
    {
        // Instantiate your 3d polygons here.

        var polygon1 = PolygonFactory.CreateCube(10);
        polygon1.Translate(new Vector3(5, 0, 0));
        var polygon2 = PolygonFactory.CreateCube(10);
        polygon2.Translate(new Vector3(0, 0, 0));

        var intersections = polygon1.GetIntersectionPoints(polygon2).ToList();

        _outputHelper.WriteLine(PrimitiveSerializers.SerializeVector3s(intersections, "F"));
        Assert.NotEmpty(intersections);
    }
}