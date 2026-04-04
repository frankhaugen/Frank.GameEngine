using System.Text;
using FluentAssertions;
using Frank.GameEngine.Assets;

namespace Frank.GameEngine.Tests.Assets;

public class ObjFaceParsingTests
{
    [Test]
    public void ParseTriangleMesh_QuadBecomesTwoTriangles()
    {
        const string obj = """
            v 0 0 0
            v 1 0 0
            v 1 1 0
            v 0 1 0
            f 1 2 3 4
            """;
        var bytes = Encoding.UTF8.GetBytes(obj);
        var mesh = ObjParser.ParseTriangleMesh(bytes);
        mesh.TriangleCount.Should().Be(2);
        mesh.VertexCount.Should().Be(4);
    }

    [Test]
    public void ParseTriangleMesh_FaceWithSlashes_UsesVertexIndex()
    {
        const string obj = """
            v 0 0 0
            v 2 0 0
            v 0 2 0
            f 1/1/1 2/1/1 3/1/1
            """;
        var mesh = ObjParser.ParseTriangleMesh(Encoding.UTF8.GetBytes(obj));
        mesh.TriangleCount.Should().Be(1);
    }
}
