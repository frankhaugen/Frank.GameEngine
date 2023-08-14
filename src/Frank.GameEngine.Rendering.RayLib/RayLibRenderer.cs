using Frank.GameEngine.Primitives;
using Raylib_cs;

namespace Frank.GameEngine.Rendering.RayLib;

public class RayLibRenderer : IRenderer
{
    public RayLibRenderer()
    {
        Raylib.InitWindow(1980, 1080, "Pong");
        // Raylib.SetTargetFPS(60);
        Raylib.SetExitKey(KeyboardKey.KEY_ESCAPE);
    }

    public void Render(Scene scene)
    {
        var shapes = scene.GetTransformedShapes().ToArray();

        Raylib.ClearBackground(new Color(scene.BackgroundColor.R, scene.BackgroundColor.G, scene.BackgroundColor.B,
            scene.BackgroundColor.A));

        Raylib.BeginDrawing();
        foreach (var shape in shapes)
        {
            var color = shape.Color;
            var edges = shape.Polygon.Edges.ToArray();

            foreach (var edge in edges)
                Raylib.DrawLine3D(edge.A, edge.B, new Color(color.R, color.G, color.B, color.A));
        }

        Raylib.EndDrawing();
    }

    public void Render(Scene scene, Action<string> callback)
    {
        Render(scene);
    }
}