using System.Globalization;
using System.Numerics;
using System.Text;

namespace Frank.GameEngine.Assets;

internal static class ObjHelper
{
    public static ObjPolygon ParsePolygon(Memory<byte> bytes)
    {
#pragma warning disable RS1035
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
#pragma warning restore RS1035
        var polygon = new ObjPolygon();
        var lines = Encoding.UTF8.GetString(bytes.ToArray()).Split('\n');
        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            switch (parts[0])
            {
                case "v":
                    polygon.Vertices.Add(new Vector3(float.Parse(parts[1]), float.Parse(parts[2]),
                        float.Parse(parts[3])));
                    break;
                case "vt":
                    polygon.Uvs.Add(new Vector2(float.Parse(parts[1]), float.Parse(parts[2])));
                    break;
                case "vn":
                    polygon.Normals.Add(
                        new Vector3(float.Parse(parts[1]), float.Parse(parts[2]), float.Parse(parts[3])));
                    break;
                case "f":
                    // polygon.Faces.Add(new Face(float.Parse(parts[1]), parts[2], parts[3]));
                    break;
            }
        }

        return polygon;
    }
}