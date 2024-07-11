using System.Numerics;
using Frank.GameEngine.Primitives;
using Raylib_CSharp.Camera.Cam3D;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering;

namespace Frank.GameEngine.Rendering.RayLib;

public class RayLibRenderer : IRenderer
{
    private Camera3D _camera;
    
    public RayLibRenderer(int width, float aspectRatio, string title)
    {
        var height = (int) (width / aspectRatio);
        Raylib_CSharp.Windowing.Window.Init(width, height, title);
        Input.SetExitKey(KeyboardKey.Escape);
        _camera = new Camera3D();
    }

    public void Render(Scene scene)
    {
        // System.Console.WriteLine("Rendering scene...");
        _camera.Position = scene.Camera.Position;
        _camera.Target = scene.Camera.Target;
        _camera.Up = scene.Camera.Up;
        _camera.FovY = scene.Camera.FieldOfView;
        _camera.Update(CameraMode.Free);
        _camera.Projection = CameraProjection.Perspective;
        
        Graphics.BeginDrawing();
        Graphics.BeginMode3D(_camera);
        Graphics.ClearBackground(new Color(scene.BackgroundColor.R, scene.BackgroundColor.G, scene.BackgroundColor.B, scene.BackgroundColor.A));
        // Graphics.DrawGrid(10, 100);
        // Graphics.DrawText("Basic Window!", 10, 10, 20, Color.White);

        var shapes = scene.GameObjects.Select(x => x.Shape).ToArray();
        
        foreach (var shape in shapes)
        {
            var color = shape.Color;
            var edges = shape.Polygon.Edges.ToArray();
            var faces = shape.Polygon.Faces.ToArray();
            
            foreach (var face in faces) Graphics.DrawTriangle3D(face.A, face.B, face.C, new Color(color.R, color.G, color.B, color.A));

            foreach (var edge in edges) Graphics.DrawLine3D(edge.A, edge.B, new Color(color.R, color.G, color.B, color.A));
        }

        Graphics.EndMode3D();
        Graphics.EndDrawing();
    }

    public void Render(Scene scene, Action<string> callback)
    {
        Render(scene);
    }
}