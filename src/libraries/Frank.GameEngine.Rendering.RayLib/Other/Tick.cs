namespace Frank.GameEngine.Rendering.RayLib;

public record Tick(ulong FrameNumber, TimeSpan TotalTime, TimeSpan DeltaTime);