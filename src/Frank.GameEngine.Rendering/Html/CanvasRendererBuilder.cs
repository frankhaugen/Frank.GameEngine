﻿using System.Text;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Rendering.Html;

public class CanvasRendererBuilder
{
    private readonly StringBuilder _builder = new();
    private readonly List<Shape> _shapes = new();

    public CanvasRendererBuilder WithStart()
    {
        _builder.AppendLine("<canvas id=\"canvas\" width=\"800\" height=\"600\"></canvas>");
        _builder.AppendLine("<script>");
        return this;
    }


    public CanvasRendererBuilder WithShapes(IEnumerable<Shape> shapes)
    {
        _shapes.AddRange(shapes);
        return this;
    }

    public string Build()
    {
        _builder.AppendLine("var canvas = document.getElementById(\"canvas\");");
        _builder.AppendLine("var ctx = canvas.getContext(\"2d\");");
        _builder.AppendLine("ctx.fillStyle = \"#FF0000\";");
        _builder.AppendLine("ctx.fillRect(0, 0, 150, 75);");
        _builder.AppendLine("</script>");
        return _builder.ToString();
    }
}