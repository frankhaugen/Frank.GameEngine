using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Assets;

public static class ObjParser
{
    public static Polygon GetPolygon(Memory<byte> bytes)
    {
        var objPolygon = ObjHelper.ParsePolygon(bytes);
        var polygon = new Polygon(objPolygon.Vertices.ToArray());
        return polygon;
    }
}