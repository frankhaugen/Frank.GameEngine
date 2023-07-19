namespace Frank.GameEngine.Core;

public readonly record struct UpdateArgs(TimeSpan ElapsedTime, TimeSpan TotalTime);