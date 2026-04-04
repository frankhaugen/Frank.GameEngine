using System.Numerics;
using Assimp;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Assets;

/// <summary>
///     Loads triangle meshes from FBX, OBJ, glTF, blend, and other formats supported by Assimp (native runtime required).
/// </summary>
public static class SceneMeshImporter
{
    private const PostProcessSteps DefaultFlags =
        PostProcessSteps.Triangulate
        | PostProcessSteps.JoinIdenticalVertices
        | PostProcessSteps.ImproveCacheLocality;

    public static TriangleMesh ImportFile(string path)
    {
        using var ctx = new AssimpContext();
        var scene = ctx.ImportFile(path, DefaultFlags);
        return MergeSceneMeshes(scene);
    }

    /// <summary>
    ///     Import from a stream; <paramref name="formatHintExtension" /> must include the dot (e.g. <c>.fbx</c>, <c>.obj</c>).
    /// </summary>
    public static TriangleMesh ImportFromStream(Stream stream, string formatHintExtension)
    {
        ArgumentNullException.ThrowIfNull(stream);
        var ext = formatHintExtension.StartsWith('.') ? formatHintExtension : "." + formatHintExtension;

        using var ctx = new AssimpContext();
        var scene = ctx.ImportFileFromStream(stream, DefaultFlags, ext);
        return MergeSceneMeshes(scene);
    }

    private static TriangleMesh MergeSceneMeshes(Assimp.Scene scene)
    {
        var vertices = new List<Vector3>();
        var indices = new List<int>();

        foreach (var mesh in scene.Meshes)
        {
            var baseIndex = vertices.Count;
            foreach (var v in mesh.Vertices)
                vertices.Add(new Vector3(v.X, v.Y, v.Z));

            for (var fi = 0; fi < mesh.FaceCount; fi++)
            {
                var face = mesh.Faces[fi];
                if (face.IndexCount != 3)
                    continue;

                indices.Add(baseIndex + face.Indices[0]);
                indices.Add(baseIndex + face.Indices[1]);
                indices.Add(baseIndex + face.Indices[2]);
            }
        }

        return new TriangleMesh(vertices, indices);
    }
}
