using System.Numerics;

namespace Frank.GameEngine.Primitives;

public static class CameraExtensions
{
    /// <summary>
    /// Gets the projection matrix for the camera. A projection matrix is used to project 3D coordinates onto a 2D plane (the screen).
    /// </summary>
    /// <param name="camera"></param>
    /// <returns></returns>
    public static Matrix4x4 GetProjectionMatrix(this Camera camera)
    {
        var projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(
            ToRadians(camera.FieldOfView),
            camera.AspectRatio,
            camera.NearPlaneDistance,
            camera.FarPlaneDistance);

        return projectionMatrix;
    }
    
    private static float ToRadians(this float degrees) => degrees * (MathF.PI / 180f);
    
    /// <summary>
    /// Gets the view matrix for the camera. A view matrix is used to transform a world's coordinates into a camera's coordinates.
    /// The view matrix is the inverse of the camera's transform matrix, which is the camera's position, rotation, and scale.
    /// </summary>
    /// <param name="camera"></param>
    /// <returns></returns>
    public static Matrix4x4 GetViewMatrix(this Camera camera)
    {
        var viewMatrix = Matrix4x4.CreateLookAt(
            camera.Position,
            camera.Target,
            camera.Up);

        return viewMatrix;
    }
}