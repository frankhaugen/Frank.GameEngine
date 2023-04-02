using System.Collections;

namespace Frank.GameEngine.Lagacy.OldCore.Physics;

public class PhysicalForces : IEnumerable<IPhysicalForce>
{
    private readonly List<IPhysicalForce> _forces;
    public PhysicalForces(params IPhysicalForce[] forces) => _forces = forces.ToList();
    public IEnumerator<IPhysicalForce> GetEnumerator() => _forces.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}