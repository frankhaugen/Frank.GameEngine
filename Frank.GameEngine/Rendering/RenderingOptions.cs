using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Rendering;

public class RenderingOptions
{
    public Matrix World { get; set; }
    public Matrix View { get; set; }
    public Matrix Projection { get; set; }

    public static RenderingOptions Default(GraphicsDevice graphicsDevice)
    {
        return new RenderingOptions
        {
            World = Matrix.Identity,
            View = Matrix.CreateLookAt(new Vector3(0, 0, 10), Vector3.Zero, Vector3.Up),
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, graphicsDevice.Viewport.AspectRatio, 1.0f, 100.0f)
        };
    }
}