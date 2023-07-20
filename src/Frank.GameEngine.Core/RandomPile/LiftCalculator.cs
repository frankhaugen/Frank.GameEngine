using Frank.GameEngine.Primitives;
using System.Numerics;

namespace Frank.GameEngine.Core.RandomPile;

public struct LiftCalculator
{
	public static float CalculateLiftCoefficient(Polygon polygon, Fluid fluid, Vector3 velocity)
	{
		var liftCoefficient = 0f;
		if (fluid.Name == FluidName.Vacuum)
			return liftCoefficient;
		liftCoefficient +=
			CalculateLiftCoefficientOfFluid(fluid, velocity.GetVelocity(), polygon.Edges.GetCharacteristicLength());
		liftCoefficient += CalculateLiftCoefficientOfPolygon(polygon, velocity);
		return liftCoefficient;
	}

	public static float CalculateLiftCoefficientOfFluid(Fluid fluid, float velocity, float characteristicLength)
	{
		// Calculate the Reynolds number of the flow
		var reynoldsNumber = CalculateReynoldsNumber(fluid, velocity, characteristicLength);

		// Use the formula for a flat plate in a laminar flow to calculate the lift coefficient
		var liftCoefficient = CalculateLiftCoefficient(reynoldsNumber);

		return liftCoefficient;
	}

	public static float CalculateLiftCoefficientOfPolygon(Polygon polygon, Vector3 velocity) 
		=> polygon.Edges.Sum(t => CalculateLiftCoefficientOfEdge(t, velocity));

	public static float CalculateReynoldsNumber(Fluid fluid, float velocity, float characteristicLength) 
		=> characteristicLength * velocity * fluid.Density / fluid.Viscosity;

	private static float CalculateLiftCoefficient(float reynoldsNumber) 
		=> 24 / reynoldsNumber + 6 / (1 + MathF.Sqrt(reynoldsNumber));

	private static float CalculateLiftCoefficientOfEdge(Edge edge, Vector3 velocity)
	{
		var edgeNormal = edge.GetNormal();
		var edgeTangent = edge.GetTangent();
		var velocityNormal = Vector3.Dot(velocity, edgeNormal);
		var velocityTangent = Vector3.Dot(velocity, edgeTangent);
		var liftCoefficient = 2 * velocityNormal * velocityTangent / (velocityNormal * velocityNormal + velocityTangent * velocityTangent);
		return liftCoefficient;
	}
}