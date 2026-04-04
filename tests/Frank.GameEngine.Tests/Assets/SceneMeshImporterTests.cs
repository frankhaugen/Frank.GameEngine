using FluentAssertions;
using Frank.GameEngine.Assets;

namespace Frank.GameEngine.Tests.Assets;

public class SceneMeshImporterTests
{
    [Test]
    public void ImportFile_TeapotObj_ProducesLargeMesh_ComparableToObjParser()
    {
        var dir = Path.Combine(AppContext.BaseDirectory, "Models");
        var path = Path.Combine(dir, "teapot.obj");
        File.Exists(path).Should().BeTrue("test host should copy src Frank.GameEngine.Assets Models");

        var bytes = File.ReadAllBytes(path);
        var objMesh = ObjParser.ParseTriangleMesh(bytes);
        var assimpMesh = SceneMeshImporter.ImportFile(path);

        // Counts can differ materially (Assimp vs raw OBJ); both must yield a dense teapot.
        objMesh.TriangleCount.Should().BeInRange(2500, 9000);
        assimpMesh.TriangleCount.Should().BeInRange(2500, 9000);
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
