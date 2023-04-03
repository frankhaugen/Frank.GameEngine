using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Types;

public class Transform
{
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
    public Vector3 Scale { get; set; }
}