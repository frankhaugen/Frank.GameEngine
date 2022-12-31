using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core;


/// <summary>
/// THis class is responsible for calculating the aerodynamic forces acting on an object.
/// </summary>
public struct AerodynamicsCalculator
{
    public float SurfaceArea { get; set; }
    public static float CalculateCoefficientOfDrag(Polygon polygon, Fluid fluid, Vector2 velocity)
    {
        var dragCoefficient = 0f;
        var dragCoefficientOfFluid = CalculateDragCoefficientOfFluid(fluid, GetMagnitude(velocity), polygon.GetCharacteristicLength());
        var dragCoefficientOfPolygon = CalculateDragCoefficientOfPolygon(polygon, velocity);
        dragCoefficient += dragCoefficientOfFluid;
        dragCoefficient += dragCoefficientOfPolygon;
        return dragCoefficient;
    }

    private static float GetMagnitude(Vector2 vector)
    {
        // Use the Pythagorean theorem to calculate the magnitude of the vector
        return MathF.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
    }

    private static float CalculateDragCoefficientOfFluid(Fluid fluid, float velocity, float characteristicLength)
    {
        // Calculate the Reynolds number of the flow
        float reynoldsNumber = characteristicLength * velocity * fluid.Density / fluid.Viscosity;

        // Use the formula for a flat plate in a laminar flow to calculate the drag coefficient
        float dragCoefficient = (24 / reynoldsNumber) + (6 / (1 + MathF.Sqrt(reynoldsNumber)));

        return dragCoefficient;
    }

    private static float CalculateDragCoefficientOfPolygon(Polygon polygon, Vector2 velocity)
    {
        var dragCoefficient = 0f;
        for (var i = 0; i < polygon.Vertices.Count(); i++)
        {
            var j = (i + 1) % polygon.Vertices.Count();
            var dragCoefficientOfEdge = CalculateDragCoefficientOfEdge(polygon.Vertices[i], polygon.Vertices[j], velocity);
            dragCoefficient += dragCoefficientOfEdge;
        }
        return dragCoefficient;
    }

    private static float CalculateDragCoefficientOfEdge(Vector2 edge1, Vector2 edge2, Vector2 velocity)
    {
        var edge = edge2 - edge1;
        var areaOfEdge = edge.Length();
        var dragCoefficientOfEdge = CalculateDragCoefficientOfEdge(areaOfEdge, velocity);
        return dragCoefficientOfEdge;
    }

    private static float CalculateDragCoefficientOfEdge(float areaOfEdge, Vector2 velocity)
    {
        var velocityOfEdge = velocity.Length();
        var dragCoefficientOfEdge = 0.5f * areaOfEdge * velocityOfEdge * velocityOfEdge;
        return dragCoefficientOfEdge;
    }
}