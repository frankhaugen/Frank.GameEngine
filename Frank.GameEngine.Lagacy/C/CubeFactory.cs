using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine;

public static class CubeFactory
{
    public static VertexPositionColor[] CreateCube(float size, Color color)
    {
        var vertices = new List<VertexPositionColor>();

        var halfSize = size / 2;

        var frontTopLeft = new Vector3(-halfSize, halfSize, halfSize);
        var frontTopRight = new Vector3(halfSize, halfSize, halfSize);
        var frontBottomLeft = new Vector3(-halfSize, -halfSize, halfSize);
        var frontBottomRight = new Vector3(halfSize, -halfSize, halfSize);

        var backTopLeft = new Vector3(-halfSize, halfSize, -halfSize);
        var backTopRight = new Vector3(halfSize, halfSize, -halfSize);
        var backBottomLeft = new Vector3(-halfSize, -halfSize, -halfSize);
        var backBottomRight = new Vector3(halfSize, -halfSize, -halfSize);

        vertices.Add(new VertexPositionColor(frontTopLeft, color));
        vertices.Add(new VertexPositionColor(frontTopRight, color));
        vertices.Add(new VertexPositionColor(frontBottomLeft, color));
        vertices.Add(new VertexPositionColor(frontBottomRight, color));

        vertices.Add(new VertexPositionColor(backTopLeft, color));
        vertices.Add(new VertexPositionColor(backTopRight, color));
        vertices.Add(new VertexPositionColor(backBottomLeft, color));
        vertices.Add(new VertexPositionColor(backBottomRight, color));

        vertices.Add(new VertexPositionColor(backTopLeft, color));
        vertices.Add(new VertexPositionColor(backBottomLeft, color));
        vertices.Add(new VertexPositionColor(frontTopLeft, color));
        vertices.Add(new VertexPositionColor(frontBottomLeft, color));

        vertices.Add(new VertexPositionColor(backTopRight, color));
        vertices.Add(new VertexPositionColor(backBottomRight, color));
        vertices.Add(new VertexPositionColor(frontTopRight, color));
        vertices.Add(new VertexPositionColor(frontBottomRight, color));

        vertices.Add(new VertexPositionColor(backTopLeft, color));
        vertices.Add(new VertexPositionColor(backTopRight, color));
        vertices.Add(new VertexPositionColor(frontTopLeft, color));
        vertices.Add(new VertexPositionColor(frontTopRight, color));

        vertices.Add(new VertexPositionColor(backBottomLeft, color));
        vertices.Add(new VertexPositionColor(backBottomRight, color));
        vertices.Add(new VertexPositionColor(frontBottomLeft, color));
        vertices.Add(new VertexPositionColor(frontBottomRight, color));

        return vertices.ToArray();
    }
}