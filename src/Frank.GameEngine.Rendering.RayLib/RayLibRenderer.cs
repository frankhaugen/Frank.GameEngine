using Frank.GameEngine.Primitives;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Windowing;

namespace Frank.GameEngine.Rendering.RayLib;

public class RayLibRenderer : IRenderer
{
    public RayLibRenderer(int width, float aspectRatio, string title)
    {
        var height = (int) (width / aspectRatio);
        Window.Init(width, height, title);
        Input.SetExitKey(KeyboardKey.Escape);
    }

    public void Render(Scene scene)
    {
        Graphics.ClearBackground(new Color(scene.BackgroundColor.R, scene.BackgroundColor.G, scene.BackgroundColor.B,
            scene.BackgroundColor.A));

        Graphics.BeginDrawing();
        var shapes = scene.GameObjects.Select(x => x.Shape).ToArray();
        
        foreach (var shape in shapes)
        {
            var color = shape.Color;
            var edges = shape.Polygon.Edges.ToArray();

            foreach (var edge in edges)
                Graphics.DrawLine3D(edge.A, edge.B, new Color(color.R, color.G, color.B, color.A));
        }

        Graphics.EndDrawing();
    }

    public void Render(Scene scene, Action<string> callback)
    {
        Render(scene);
    }
}