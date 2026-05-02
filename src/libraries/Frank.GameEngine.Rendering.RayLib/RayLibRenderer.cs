using System.Numerics;
using Frank.GameEngine.Primitives;
using Raylib_CSharp.Camera.Cam3D;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Windowing;

namespace Frank.GameEngine.Rendering.RayLib;

public class RayLibRenderer : IRenderer
{
    private Camera3D _camera;

    private readonly List<GameObject2D> _sorted2DScratch = new();

    /// <summary>Optional 2D layer drawn after clear, before 3D (e.g. parallax or tile backdrop).</summary>
    public Scene2D? Underlay2D { get; set; }

    /// <summary>Optional 2D layer drawn after 3D (HUD, crosshair).</summary>
    public Scene2D? Overlay2D { get; set; }

    /// <summary>True when the user closed the window or hit the exit key.</summary>
    public bool ShouldClose => Window.ShouldClose();

    /// <summary>Raylib frame delta in seconds (call from your main loop each frame).</summary>
    public float FrameDeltaSeconds => Raylib_CSharp.Time.GetFrameTime();

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
        // Host-owned camera (FPS / scene graph). Free mode would fight scene.Camera each frame.
        _camera.Update(CameraMode.Custom);
        _camera.Projection = CameraProjection.Perspective;
        
        Graphics.BeginDrawing();
        Graphics.ClearBackground(new Color(scene.BackgroundColor.R, scene.BackgroundColor.G, scene.BackgroundColor.B, scene.BackgroundColor.A));

        if (Underlay2D != null)
        {
            SyncViewportDimensions(Underlay2D.Camera);
            Raylib2DDrawing.DrawScene2D(Underlay2D, _sorted2DScratch);
        }

        Graphics.BeginMode3D(_camera);

        foreach (var gameObject in scene.GameObjects)
        {
            if (!gameObject.IsActive)
                continue;

            var shape = gameObject.GetTransformedShape();
            var color = new Color(shape.Color.R, shape.Color.G, shape.Color.B, shape.Color.A);

            if (shape.TriangleMesh != null)
            {
                DrawTriangleMesh(shape.TriangleMesh, color);
                continue;
            }

            var faces = shape.Polygon.FacesSpan;
            for (var fi = 0; fi < faces.Length; fi++)
            {
                var face = faces[fi];
                Graphics.DrawTriangle3D(face.A, face.B, face.C, color);
            }

            var edges = shape.Polygon.EdgesSpan;
            for (var ei = 0; ei < edges.Length; ei++)
            {
                var edge = edges[ei];
                Graphics.DrawLine3D(edge.A, edge.B, color);
            }
        }

        Graphics.EndMode3D();

        if (Overlay2D != null)
        {
            CenterScreenSpaceCamera(Overlay2D.Camera);
            Raylib2DDrawing.DrawScene2D(Overlay2D, _sorted2DScratch);
        }

        Graphics.EndDrawing();
    }

    private static void SyncViewportDimensions(Camera2D camera)
    {
        camera.ViewportWidth = Math.Max(1, Window.GetScreenWidth());
        camera.ViewportHeight = Math.Max(1, Window.GetScreenHeight());
    }

    /// <summary>Pins <see cref="Camera2D.Target" /> to the window center (typical HUD).</summary>
    private static void CenterScreenSpaceCamera(Camera2D camera)
    {
        SyncViewportDimensions(camera);
        camera.Offset = new Vector2(camera.ViewportWidth * 0.5f, camera.ViewportHeight * 0.5f);
    }

    public void Render(Scene scene, Action<string> callback)
    {
        Render(scene);
    }

    private static void DrawTriangleMesh(TriangleMesh mesh, Color color)
    {
        var verts = mesh.Vertices;
        var ix = mesh.Indices;
        for (var i = 0; i < ix.Length; i += 3)
        {
            var a = verts[ix[i]];
            var b = verts[ix[i + 1]];
            var c = verts[ix[i + 2]];
            Graphics.DrawTriangle3D(a, b, c, color);
        }
    }
}