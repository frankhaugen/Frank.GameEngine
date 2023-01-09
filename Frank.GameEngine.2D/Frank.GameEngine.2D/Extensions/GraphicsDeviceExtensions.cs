using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Frank.GameEngine._2D.Models.BasicShapes;

namespace Frank.GameEngine._2D.Extensions;

public static class GraphicsDeviceExtensions
{
    internal static void Draw(this GraphicsDevice graphicsDevice, Vertices vertices)
    {
        var effect = new BasicEffect(graphicsDevice);
        var viewport = graphicsDevice.Viewport;

        effect.TextureEnabled = true;
        effect.FogEnabled = false;
        effect.LightingEnabled = false;
        effect.VertexColorEnabled = true;
        effect.World = Matrix.Identity;
        effect.View = Matrix.Identity;
        effect.Projection = Matrix.CreateOrthographicOffCenter(0, viewport.Width, 0, viewport.Height, 0, 0);

        foreach (var pass in effect.CurrentTechnique.Passes)
        {
            pass.Apply();

            graphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices.VertexArray, 0, vertices.VertexCount, vertices.Indicies, 0, vertices.IndexCount / 3);
        }
    }

    public static SpriteBatch CreateSpriteBatch(this GraphicsDevice graphicsDevice) => new SpriteBatch(graphicsDevice);
    public static Point GetOrigin(this GraphicsDevice graphicsDevice) => graphicsDevice.Viewport.GetOrigin();
}