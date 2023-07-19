using Frank.GameEngine.Primitives;
using System.Numerics;

namespace Frank.GameEngine.Core.RandomPile;

/// <summary>
/// THis class is responsible for calculating the aerodynamic forces acting on an object.
/// </summary>
public struct AerodynamicsCalculator
{
	public static float CalculateCoefficientOfDrag(Polygon polygon, Fluid fluid, Vector3 velocity)
	{
		var dragCoefficient = 0f;
		if (fluid.Name == FluidName.Vacuum)
			return dragCoefficient;
		dragCoefficient += CalculateDragCoefficientOfFluid(fluid, velocity.GetVelocity(), polygon.Edges.GetCharacteristicLength());
		dragCoefficient += CalculateDragCoefficientOfPolygon(polygon, velocity);
		return dragCoefficient;
	}

	public static float CalculateDragCoefficientOfFluid(Fluid fluid, float velocity, float characteristicLength)
	{
		// Calculate the Reynolds number of the flow
		var reynoldsNumber = CalculateReynoldsNumber(fluid, velocity, characteristicLength);

		// Use the formula for a flat plate in a laminar flow to calculate the drag coefficient
		var dragCoefficient = CalculateDragCoefficient(reynoldsNumber);

		return dragCoefficient;
	}


	public static float CalculateDragCoefficientOfPolygon(Polygon polygon, Vector3 velocity)
	{
		var dragCoefficient = 0f;
		for (var i = 0; i < polygon.Count(); i++)
		{
			var j = (i + 1) % polygon.Count();
			var dragCoefficientOfEdge = CalculateDragCoefficientOfEdge(polygon[i] - polygon[j], velocity);
			dragCoefficient += dragCoefficientOfEdge;
		}

		return dragCoefficient;
	}

	public static float CalculateReynoldsNumber(Fluid fluid, float velocity, float characteristicLength) => characteristicLength * velocity * fluid.Density / fluid.Viscosity;

	private static float CalculateDragCoefficient(float reynoldsNumber) => 24 / reynoldsNumber + 6 / (1 + MathF.Sqrt(reynoldsNumber));

	private static float CalculateDragCoefficientOfEdge(Vector3 edge, Vector3 velocity)
		=> CalculateDragCoefficientOfEdge(edge.Length(), velocity.Length());

	private static float CalculateDragCoefficientOfEdge(float areaOfEdge, float velocity)
		=> 0.5f * areaOfEdge * velocity * velocity;
}
