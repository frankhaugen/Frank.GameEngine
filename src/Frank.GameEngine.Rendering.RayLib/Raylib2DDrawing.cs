using System.Numerics;
using Frank.GameEngine.Primitives;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Transformations;

namespace Frank.GameEngine.Rendering.RayLib;

internal static class Raylib2DDrawing
{
    public static void DrawScene2D(Scene2D scene, List<GameObject2D> sortedScratch)
    {
        scene.CollectActiveSorted(sortedScratch);
        var cam = ToRaylibCamera(scene.Camera);
        Graphics.BeginMode2D(cam);
        for (var i = 0; i < sortedScratch.Count; i++)
            DrawGameObject2D(sortedScratch[i]);

        Graphics.EndMode2D();
    }

    public static Raylib_CSharp.Camera.Cam2D.Camera2D ToRaylibCamera(Camera2D camera) =>
        new(
            camera.Offset,
            camera.Target,
            camera.RotationDegrees,
            Math.Max(camera.Zoom, 1e-4f));

    private static void DrawGameObject2D(GameObject2D go)
    {
        var sp = go.Sprite;
        var tr = go.Transform;
        var w = sp.Size.X * tr.Scale.X;
        var h = sp.Size.Y * tr.Scale.Y;
        var pivotPx = new Vector2(sp.Origin.X * w, sp.Origin.Y * h);
        var topLeft = tr.Position - pivotPx;
        var rec = new Rectangle(topLeft.X, topLeft.Y, w, h);
        Graphics.DrawRectanglePro(rec, pivotPx, tr.RotationDegrees, ToRlColor(sp.Tint));
    }

    private static Color ToRlColor(Rgba32 c) => new(c.R, c.G, c.B, c.A);
}
