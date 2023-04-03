using Frank.GameEngine.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Rendering;

public interface IPrimitiveBatch
{
    void Dispose();
    void Begin(PrimitiveType primitiveType);
    void AddPolygon(IPolygon polygon);
    void AddPolygon(IPolygon polygon, Color color);
    void AddVertex(Vertex vertex);
    void AddVertex(VertexColor vertexColor);
    void AddVertex(Vertex vertex, Color color);
    void End();
}