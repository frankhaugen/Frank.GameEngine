namespace Frank.GameEngine.Core;

public struct DragCoefficientCalculator
{
    public float Fd { get; set; } // the drag force
    public float Rho { get; set; } // the density of the fluid
    public float V { get; set; } // the velocity of the object
    public float A { get; set; } // the surface area of the object

    public float CalculateCd()
    {
        return Fd / (0.5f * Rho * V * V * A);
    }
}