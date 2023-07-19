using Frank.GameEngine.Lagacy.A.OldCore.Graphics.Rendering;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy.A.OldCore.Extensions;

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
    
    public static void SetTarget(this ICamera3D camera, IGameObject target) => camera.Target = target.Transform.Position;
    public static void SetTarget(this ICamera3D camera, Vector3 target) => camera.Target = target;

    public static Matrix GetView(this ICamera3D camera)
    {
        return Matrix.CreateLookAt(camera.Position, camera.Target, camera.Up);
    }
}