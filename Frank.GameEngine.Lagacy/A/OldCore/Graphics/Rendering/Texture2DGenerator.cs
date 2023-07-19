using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Lagacy.A.OldCore.Graphics.Rendering;

public static class Texture2DGenerator
{
    public static Texture2D GenerateTexture2D(this GraphicsDevice graphicsDevice, RenderTarget2D renderTarget)
    {
        // Get the color data from the render target
        var colorData = new Color[renderTarget.Width * renderTarget.Height];
        renderTarget.GetData(colorData);

        // Create a new Texture2D object using the color data
        var texture = new Texture2D(graphicsDevice, renderTarget.Width, renderTarget.Height);
        texture.SetData(colorData);

        return texture;
    }
}