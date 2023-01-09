using Frank.GameEngine.Core.Graphics.Rendering;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core.Extensions;

public static class Camera3DExtensions
{
    public static Matrix GetProjection(this ICamera3D camera, float width, float height, float nearPlaneDistance, float farPlaneDistance)
    {
        return Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, width / height, nearPlaneDistance, farPlaneDistance);
    }

    public static Matrix GetPerspective(this ICamera3D camera, float width, float height, float nearPlaneDistance, float farPlaneDistance)
    {
        return Matrix.CreatePerspective(width, height, nearPlaneDistance, farPlaneDistance);
    }
    
    public static Matrix GetView(this ICamera3D camera)
    {
        return Matrix.CreateLookAt(camera.Position, camera.Target, camera.Up);
    }
}