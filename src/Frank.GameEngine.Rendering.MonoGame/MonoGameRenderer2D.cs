using Frank.GameEngine.Primitives;
using Frank.GameEngine.Rendering;
using Frank.GameEngine.Rendering.MonoGame.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Rendering.MonoGame;

/// <summary>
///     SpriteBatch orthographic 2D using <see cref="Camera2DExtensions.GetWorldToScreenMatrix" />.
/// </summary>
public sealed class MonoGameRenderer2D : IRenderer2D
{
    private readonly IGraphicsDeviceContext _ctx;
    private Texture2D? _pixel;

    public MonoGameRenderer2D(IGraphicsDeviceContext ctx)
    {
        _ctx = ctx;
    }

    private Texture2D Pixel =>
        _pixel ??= new Texture2D(_ctx.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);

    public void Render(Scene2D scene)
    {
        if (!Pixel.TrySetData([Microsoft.Xna.Framework.Color.White]))
        {
        }

        var device = _ctx.GraphicsDevice;
        device.Clear(ToXnaColor(scene.BackgroundColor));

        var view = scene.Camera.GetWorldToScreenMatrix().ToXnaMatrix();
        using var batch = new SpriteBatch(device);
        batch.Begin(
            transformMatrix: view,
            blendState: BlendState.AlphaBlend,
            samplerState: SamplerState.PointClamp,
            depthStencilState: DepthStencilState.None,
            rasterizerState: RasterizerState.CullNone);

        foreach (var go in scene.GetActiveSorted())
        {
            var sp = go.Sprite;
            var tr = go.Transform;
            var w = sp.Size.X * tr.Scale.X;
            var h = sp.Size.Y * tr.Scale.Y;
            var layer = go.ZOrder * 0.0001f;
            batch.Draw(
                Pixel,
                tr.Position,
                new Microsoft.Xna.Framework.Rectangle(0, 0, 1, 1),
                sp.Tint.ToXnaColor(),
                MathHelper.ToRadians(tr.RotationDegrees),
                new Vector2(sp.Origin.X, sp.Origin.Y),
                new Vector2(w, h),
                SpriteEffects.None,
                layer);
        }

        batch.End();
    }

    private static Microsoft.Xna.Framework.Color ToXnaColor(Rgba32 c) =>
        new(c.R, c.G, c.B, c.A);
}
