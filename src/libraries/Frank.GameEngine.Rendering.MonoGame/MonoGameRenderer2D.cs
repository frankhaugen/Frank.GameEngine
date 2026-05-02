using System.Collections.Generic;
using Frank.GameEngine.Primitives;
using Frank.GameEngine.Rendering;
using Frank.GameEngine.Rendering.MonoGame.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaColor = Microsoft.Xna.Framework.Color;
using XnaVector2 = Microsoft.Xna.Framework.Vector2;

namespace Frank.GameEngine.Rendering.MonoGame;

/// <summary>
///     SpriteBatch orthographic 2D using <see cref="Camera2DExtensions.GetWorldToScreenMatrix" />.
/// </summary>
public sealed class MonoGameRenderer2D : IRenderer2D
{
    private readonly IGraphicsDeviceContext _ctx;
    private readonly List<GameObject2D> _sortedScratch = new();
    private Texture2D? _pixel;
    private SpriteBatch? _spriteBatch;

    public MonoGameRenderer2D(IGraphicsDeviceContext ctx)
    {
        _ctx = ctx;
    }

    private Texture2D Pixel
    {
        get
        {
            if (_pixel != null)
                return _pixel;
            _pixel = new Texture2D(_ctx.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            _pixel.SetData([XnaColor.White]);
            return _pixel;
        }
    }

    public void Render(Scene2D scene)
    {
        var device = _ctx.GraphicsDevice;
        device.Clear(scene.BackgroundColor.ToXnaColor());

        var view = scene.Camera.GetWorldToScreenMatrix().ToXnaMatrix();
        _spriteBatch ??= new SpriteBatch(device);
        var batch = _spriteBatch;
        scene.CollectActiveSorted(_sortedScratch);
        batch.Begin(
            transformMatrix: view,
            blendState: BlendState.AlphaBlend,
            samplerState: SamplerState.PointClamp,
            depthStencilState: DepthStencilState.None,
            rasterizerState: RasterizerState.CullNone);

        for (var i = 0; i < _sortedScratch.Count; i++)
        {
            var go = _sortedScratch[i];
            var sp = go.Sprite;
            var tr = go.Transform;
            var w = sp.Size.X * tr.Scale.X;
            var h = sp.Size.Y * tr.Scale.Y;
            var layer = go.ZOrder * 0.0001f;
            batch.Draw(
                Pixel,
                new XnaVector2(tr.Position.X, tr.Position.Y),
                new Microsoft.Xna.Framework.Rectangle(0, 0, 1, 1),
                sp.Tint.ToXnaColor(),
                MathHelper.ToRadians(tr.RotationDegrees),
                new XnaVector2(sp.Origin.X, sp.Origin.Y),
                new XnaVector2(w, h),
                SpriteEffects.None,
                layer);
        }

        batch.End();
    }
}
