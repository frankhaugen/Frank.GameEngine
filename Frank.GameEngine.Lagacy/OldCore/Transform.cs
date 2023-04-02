using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy.OldCore;

public class Transform : ITransform
{
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
    public Vector3 Scale { get; set; }

    public Transform()
    {
        Position = Vector3.Zero;
        Rotation = Quaternion.Identity;
        Scale = Vector3.One;
    }
}