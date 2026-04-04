using System.Globalization;
using System.Numerics;
using System.Text;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Assets;

internal static class ObjHelper
{
    /// <summary>
    ///     Parses Wavefront OBJ: <c>v</c> vertices and <c>f</c> faces (triangles or quads; quads split into two triangles).
    ///     When no faces exist, returns a <see cref="TriangleMesh" /> with zero indices and vertices only (legacy callers
    ///     can build a <see cref="Polygon" /> from vertices).
    /// </summary>
    public static TriangleMesh ParseTriangleMesh(ReadOnlySpan<byte> bytes)
    {
#pragma warning disable RS1035
        var prev = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
#pragma warning restore RS1035
        try
        {
            var text = Encoding.UTF8.GetString(bytes);
            var vertices = new List<Vector3>();
            var indices = new List<int>();

            foreach (var rawLine in text.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries))
            {
                var line = rawLine.Trim();
                if (line.Length == 0 || line[0] == '#')
                    continue;

                if (line.StartsWith("v ", StringComparison.Ordinal))
                {
                    var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 4)
                        continue;
                    vertices.Add(new Vector3(float.Parse(parts[1]), float.Parse(parts[2]), float.Parse(parts[3])));
                    continue;
                }

                if (!line.StartsWith("f ", StringComparison.Ordinal))
                    continue;

                var faceParts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (faceParts.Length < 4)
                    continue;

                var cornerCount = faceParts.Length - 1;
                var vIndices = new int[cornerCount];
                for (var c = 0; c < cornerCount; c++)
                {
                    var token = faceParts[c + 1];
                    var slash = token.IndexOf('/');
                    var num = slash >= 0 ? token[..slash] : token;
                    vIndices[c] = int.Parse(num, CultureInfo.InvariantCulture) - 1;
                }

                void AddTri(int a, int b, int c)
                {
                    indices.Add(a);
                    indices.Add(b);
                    indices.Add(c);
                }

                switch (cornerCount)
                {
                    case 3:
                        AddTri(vIndices[0], vIndices[1], vIndices[2]);
                        break;
                    case 4:
                        AddTri(vIndices[0], vIndices[1], vIndices[2]);
                        AddTri(vIndices[0], vIndices[2], vIndices[3]);
                        break;
                    default:
                        for (var i = 1; i < cornerCount - 1; i++)
                            AddTri(vIndices[0], vIndices[i], vIndices[i + 1]);
                        break;
                }
            }

            return new TriangleMesh(vertices, indices);
        }
        finally
        {
#pragma warning disable RS1035
            CultureInfo.CurrentCulture = prev;
#pragma warning restore RS1035
        }
    }
}
