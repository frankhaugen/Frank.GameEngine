using Frank.GameEngine.Input;
using Frank.GameEngine.Physics;
using Frank.GameEngine.Primitives;
using Frank.GameEngine.Rendering;

namespace Frank.GameEngine.Core;

public class GameEngine
{
    private IRenderer _renderer;

    public bool IsInitialized { get; private set; } = false;

    public SceneManager SceneManager { get; } = new();

    public InputManager InputManager { get; } = new();
    
    public PhysicsEngine PhysicsEngine { get; } = new();

    public Scene? CurrentScene => SceneManager.CurrentScene;

    public void Initialize(IRenderer renderer)
    {
        if (CurrentScene is null)
            throw new Exception("No scene has been set.");

        _renderer = renderer;

        new Thread(() => InputManager.Start()).Start();

        IsInitialized = true;
    }

    public void Update(UpdateArgs args)
    {
        if (CurrentScene is null)
            return;
        
        PhysicsEngine.Update(CurrentScene, args.ElapsedTime);
    }

    public void Draw()
    {
        if (CurrentScene is null)
            return;
        _renderer.Render(CurrentScene);
    }
}