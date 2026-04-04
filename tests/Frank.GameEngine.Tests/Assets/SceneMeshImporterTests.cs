using FluentAssertions;
using Frank.GameEngine.Assets;

namespace Frank.GameEngine.Tests.Assets;

public class SceneMeshImporterTests
{
    [Test]
    public void ImportFile_TeapotObj_MatchesNativeObjTriangleCount()
    {
        var dir = Path.Combine(AppContext.BaseDirectory, "Models");
        var path = Path.Combine(dir, "teapot.obj");
        File.Exists(path).Should().BeTrue("test host should copy src Frank.GameEngine.Assets Models");

        var assimpMesh = SceneMeshImporter.ImportFile(path);
        var objMesh = ObjParser.ParseTriangleMesh(File.ReadAllBytes(path));

        assimpMesh.TriangleCount.Should().Be(objMesh.TriangleCount);
        assimpMesh.VertexCount.Should().Be(objMesh.VertexCount);
    }

    [Test]
    public void ImportFileFromStream_Obj_Works()
    {
        var dir = Path.Combine(AppContext.BaseDirectory, "Models");
        var path = Path.Combine(dir, "teapot.obj");
        using var stream = File.OpenRead(path);
        var mesh = SceneMeshImporter.ImportFromStream(stream, ".obj");
        mesh.TriangleCount.Should().BeGreaterThan(500);
    }
}
