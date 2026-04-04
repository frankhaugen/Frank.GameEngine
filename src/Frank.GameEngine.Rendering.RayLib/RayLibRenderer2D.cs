using Frank.GameEngine.Primitives;
using Frank.GameEngine.Rendering;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Windowing;

namespace Frank.GameEngine.Rendering.RayLib;

/// <summary>
///     Raylib orthographic 2D renderer (standalone window). For HUD on top of 3D, use <see cref="RayLibRenderer.Underlay2D" />
///     / <see cref="RayLibRenderer.Overlay2D" />.
/// </summary>
public sealed class RayLibRenderer2D : IRenderer2D
{
    public bool ShouldClose => Window.ShouldClose();

    public float FrameDeltaSeconds => Raylib_CSharp.Time.GetFrameTime();

    public RayLibRenderer2D(int width, int height, string title)
    {
        Window.Init(width, height, title);
        Input.SetExitKey(KeyboardKey.Escape);
    }

    public void Render(Scene2D scene)
    {
        scene.Camera.ViewportWidth = Math.Max(1, Window.GetScreenWidth());
        scene.Camera.ViewportHeight = Math.Max(1, Window.GetScreenHeight());

        Graphics.BeginDrawing();
        Graphics.ClearBackground(ToRlColor(scene.BackgroundColor));
        Raylib2DDrawing.DrawScene2D(scene);
        Graphics.EndDrawing();
    }

    private static Color ToRlColor(Rgba32 c) => new(c.R, c.G, c.B, c.A);
}
