using System.Numerics;

namespace Frank.GameEngine.Core.Experimental;

public struct Collission
{
	public string GameObject1 { get; set; }
	public string GameObject2 { get; set; }
	public Vector2 Force { get; set; }
}