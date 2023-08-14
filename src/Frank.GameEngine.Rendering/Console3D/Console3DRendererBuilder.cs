using System.Text;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Rendering.Console3D;

public class Console3DRendererBuilder
{
    private readonly StringBuilder _builder = new();

    public Console3DRendererBuilder WithShapes(IEnumerable<Shape> shapes)
    {
        foreach (var shape in shapes) WithShape(shape);
        return this;
    }

    public Console3DRendererBuilder WithShape(Shape shape)
    {
        return this;
    }


    public string Build()
    {
        return _builder.ToString();
    }
}