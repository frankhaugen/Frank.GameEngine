using Frank.GameEngine.Primitives;
using System.Drawing;

namespace Frank.GameEngine.Rendering.Svg;

public class SvgRenderer : IRenderer
{
    private readonly SvgRendererOptions _options;

    public SvgRenderer(SvgRendererOptions options) => _options = options;

    public void Render(Scene scene) => throw new NotImplementedException("This method is not implemented. Use the other overload.");

    public void Render(Scene scene, Action<string> callback)
    {
        var svg = new SvgBuilder(_options, scene.Size)
            .WithGridLines()
            .WithBackground(scene.BackgroundColor)
            .WithShapes(scene.GetTransformedShapes())
            .WithLegend()
            .Build();
        callback(svg);
    }
}