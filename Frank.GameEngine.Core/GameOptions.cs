namespace Frank.GameEngine.Core;

public class GameOptions
{
    public Resolution Resolution { get; set; }
    public bool Fullscreen { get; set; }
    public bool VSync { get; set; }
    public bool IsFixedTimeStep { get; set; }
    public float TargetFPS { get; set; }
    public bool ShowFps { get; set; }
    public bool ShowDebugInfo { get; set; }
    public bool ShowMouse { get; set; }
}