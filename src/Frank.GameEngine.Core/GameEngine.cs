using Frank.GameEngine.Audio.Ogg;
using Frank.GameEngine.Input;
using Frank.GameEngine.Physics;
using Frank.GameEngine.Primitives;
using Frank.GameEngine.Rendering;
using Microsoft.Extensions.Logging;

namespace Frank.GameEngine.Core;

public class GameEngine
{
    private IRenderer _renderer;

    public GameEngine(PhysicsEngine physicsEngine, IAudioPlayer audioPlayer)
    {
        PhysicsEngine = physicsEngine;
        AudioPlayer = audioPlayer;
    }

    public ILoggerProvider? LoggerProvider { get; set; }

    public bool IsInitialized { get; private set; }

    public SceneManager SceneManager { get; } = new();

    public InputManager InputManager { get; } = new();

    public PhysicsEngine PhysicsEngine { get; }

    public IAudioPlayer AudioPlayer { get; }

    public Scene? CurrentScene => SceneManager.CurrentScene;

    public void Initialize(IRenderer renderer)
    {
        if (CurrentScene is null)
            throw new Exception("No scene has been set.");

        _renderer = renderer;

        new Thread(() => AudioPlayer.PlayLooping(0)).Start();

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