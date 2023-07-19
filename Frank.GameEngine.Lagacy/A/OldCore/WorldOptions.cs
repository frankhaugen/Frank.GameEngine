using Frank.GameEngine.Lagacy.A.OldCore.Constants;
using Frank.GameEngine.Lagacy.A.OldCore.Physics;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy.A.OldCore;

public class WorldOptions
{
    public Vector3 Gravity { get; set; }
    public Fluid Atmosphere { get; set; }

    public WorldOptions()
    {
        // Set default values for gravity and atmosphere
        Gravity = new Vector3(0, -PhysicalConstants.GravitationalAcceleration, 0);
        Atmosphere = new Fluid(FluidName.Air);
    }
}