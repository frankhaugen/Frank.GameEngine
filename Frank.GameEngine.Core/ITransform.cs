using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core;

public interface ITransform
{
    Vector3 Position { get; set; }
    Quaternion Rotation { get; set; }
    Vector3 Scale { get; set; }
}