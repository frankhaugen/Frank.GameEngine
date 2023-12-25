using Frank.GameEngine.Primitives;
using Raylib_cs;

namespace Frank.GameEngine.Rendering.RayLib;

public class RayLibRenderer : IRenderer
{
    public RayLibRenderer(int width, float aspectRatio, string title)
    {
        var height = (int) (width / aspectRatio);
        Raylib.InitWindow(width, height, title);
        // Raylib.SetTargetFPS(60);
        Raylib.SetExitKey(KeyboardKey.KEY_ESCAPE);
    }

    public void Render(Scene scene)
    {
        Raylib.ClearBackground(new Color(scene.BackgroundColor.R, scene.BackgroundColor.G, scene.BackgroundColor.B,
            scene.BackgroundColor.A));

        Raylib.BeginDrawing();
        var shapes = scene.GameObjects.Select(x => x.Shape).ToArray();
        
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