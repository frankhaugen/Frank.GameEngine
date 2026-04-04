using FluentAssertions;
using Frank.GameEngine.Assets;

namespace Frank.GameEngine.Tests.Assets;

public class ModelsAssetsTeapotMeshTests
{
    [Test]
    public void GetTeapotMesh_HasManyTriangles_FromObjFaces()
    {
        var mesh = ModelsAssets.GetTeapotMesh();
        mesh.TriangleCount.Should().BeGreaterThan(500);
        mesh.VertexCount.Should().BeGreaterThan(100);
    }

    [Test]
    public void GetTeapotShape_UsesTriangleMesh_NotFanPolygon()
    {
        var shape = ModelsAssets.GetTeapotShape();
        shape.TriangleMesh.Should().NotBeNull();
        shape.TriangleMesh!.TriangleCount.Should().Be(ModelsAssets.GetTeapotMesh().TriangleCount);
    }
}
