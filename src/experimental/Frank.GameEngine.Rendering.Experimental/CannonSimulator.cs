using System.Numerics;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Physics;

public class CannonSimulator
{
    private readonly Vector3 _gravity;
    private readonly Vector3 _initialVelocity;

    public CannonSimulator(double gramsOfGunpowder, float mass, Vector3 launchDirection, Vector3 gravity)
    {
        _gravity = gravity;
        var initialForce =
            gramsOfGunpowder *
            Constants.PhysicsConstants.GunpowderEnergyPerGramInJoules; // Convert grams to joules (energy)
        _initialVelocity = ForceHelper.GetInitialVelocity(initialForce, mass, launchDirection);
    }

    public Vector3 GetPosition(TimeSpan time)
    {
        var t = (float)time.TotalSeconds;
        // We need to update all three components x, y and z
        var x = _initialVelocity.X * t;
        var y = _initialVelocity.Y * t - 0.5f * _gravity.Y * t * t;
        var z = _initialVelocity.Z * t - 0.5f * _gravity.Z * t * t;

        return new Vector3(x, y, z);
    }
}