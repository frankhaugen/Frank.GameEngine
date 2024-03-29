﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Rendering.MonoGame.Extensions;

public static partial class GraphicsDeviceExtensions
{
    public static Texture2D CreateWhitePixel(this GraphicsDevice graphicsDevice)
    {
        var texture = new Texture2D(graphicsDevice, 1, 1);
        texture.SetData(new[] { Color.White });
        return texture;
    }

    public static Texture2D GenerateTexture2D(this GraphicsDevice graphicsDevice, Color color)
    {
        return graphicsDevice.GenerateTexture2D(graphicsDevice.GenerateRenderTarget2D());
    }

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

    public static RenderTarget2D GenerateRenderTarget2D(this GraphicsDevice graphicsDevice)
    {
        return new RenderTarget2D(graphicsDevice, 128, 128, false, SurfaceFormat.Color, DepthFormat.Depth24);
    }
}