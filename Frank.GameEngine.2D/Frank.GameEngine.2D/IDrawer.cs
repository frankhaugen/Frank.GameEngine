using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Shapes;

//using MonoGame.Shapes;

namespace Frank.GameEngine._2D;

public interface IDrawer
{
    void Begin();
    void End();
    void DrawCircle(Vector2 center, float radius, int sides, Color color);
    void DrawPolygon(Vector2 center, Polygon polygon, Color color);
    void DrawString(SpriteFont spriteFont, string text, Vector2 position, Color color);
    void DrawLine(Vector2 origin, IEnumerable<Vector2> vertices, Color color);
    void DrawLine(Vector2 start, Vector2 end, Color color);
}