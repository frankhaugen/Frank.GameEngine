namespace Frank.GameEngine.Lagacy.A.OldCore.Physics;

public struct Fluid
{
    public FluidName Name { get; set; }
    public float Density { get; set; }
    public float Viscosity { get; set; }

    public Fluid(FluidName name)
    {
        Name = name;
        Density = GetDensity(name);
        Viscosity = GetViscosity(name);
    }

    private static readonly Dictionary<FluidName, float> Densities = new Dictionary<FluidName, float>
    {
        { FluidName.Air, 1.225f },
        { FluidName.Water, 1000f },
        { FluidName.CarbonDioxide, 1.977f },
        { FluidName.Helium, 0.178f },
        { FluidName.Hydrogen, 0.08988f },
        { FluidName.Nitrogen, 1.251f },
        { FluidName.Oxygen, 1.429f },
        { FluidName.Propane, 1.879f },
        { FluidName.Steam, 0.5961f },
        { FluidName.Vacuum, 0f }
    };

    private static readonly Dictionary<FluidName, float> Viscosities = new Dictionary<FluidName, float>
    {
        { FluidName.Air, 1.81e-5f },
        { FluidName.Water, 1.004e-3f },
        { FluidName.CarbonDioxide, 1.529e-5f },
        { FluidName.Helium, 1.66e-5f },
        { FluidName.Hydrogen, 1.33e-5f },
        { FluidName.Nitrogen, 1.8e-5f },
        { FluidName.Oxygen, 1.46e-5f },
        { FluidName.Propane, 2.00e-5f },
        { FluidName.Steam, 1.004e-3f },
        { FluidName.Vacuum, 1.81e-5f }
    };

    public static float GetDensity(FluidName fluid) => Densities[fluid];
    public static float GetViscosity(FluidName fluid) => Densities[fluid];
}