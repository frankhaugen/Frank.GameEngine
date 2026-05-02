namespace Frank.GameEngine.Rendering.RayLib;

/// <summary>
///     Published after the Raylib hosted physics step enqueues geometry for a tick (not core <c>PhysicsEngine</c>).
/// </summary>
public record RayLibPhysicsStepComplete(Tick Tick);
