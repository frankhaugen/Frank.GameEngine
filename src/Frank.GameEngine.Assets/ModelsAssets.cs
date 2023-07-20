using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Assets;

public class ModelsAssets
{
    public static Polygon GetTeapot()
    {
        var assembly = typeof(ModelsAssets).Assembly;
        var resourceName = assembly.GetManifestResourceName("teapot.obj");
        var bytes = assembly.GetResource(resourceName!);
        var polygon = ObjParser.GetPolygon(bytes);
        return polygon;
    }
}