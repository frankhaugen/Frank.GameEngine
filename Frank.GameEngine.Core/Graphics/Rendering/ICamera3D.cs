using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core.Graphics.Rendering;

public interface ICamera3D
{
    Vector3 Position { get; set; }
    Vector3 Target { get; set; }
    Vector3 Up { get; set; }
}