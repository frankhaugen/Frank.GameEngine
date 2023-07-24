using Frank.GameEngine.Assets.Testing.Scenes;
using Frank.GameEngine.Audio;
using Frank.GameEngine.Audio.Console;
using Frank.GameEngine.Core;
using Frank.GameEngine.Input;
using Frank.GameEngine.Physics;
using Frank.GameEngine.Rendering.Console;
using System.Numerics;

namespace Frank.GameEngine.Tests.Application;

public class ConsoleRendererWorker : BackgroundService
{
    private readonly ILogger<ConsoleRendererWorker> _logger;

    public ConsoleRendererWorker(ILogger<ConsoleRendererWorker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var renderer = new ConsoleRenderer(128, 4);
        var tune = new Tune()
        {
            new Note(Tone.B, Duration.QUARTER),
            new Note(Tone.A, Duration.QUARTER),
            new Note(Tone.GbelowC, Duration.QUARTER),
            new Note(Tone.A, Duration.QUARTER),
            new Note(Tone.B, Duration.QUARTER),
            new Note(Tone.B, Duration.QUARTER),
            new Note(Tone.B, Duration.HALF),
            new Note(Tone.A, Duration.QUARTER),
            new Note(Tone.A, Duration.QUARTER),
            new Note(Tone.A, Duration.HALF),
            new Note(Tone.B, Duration.QUARTER),
            new Note(Tone.D, Duration.QUARTER),
            new Note(Tone.D, Duration.HALF)
        };

        var engine = new Core.GameEngine(new PhysicsEngine(new NullCollisionHandler()), AudioPlayerFactory.CreateConsoleAudioPlayer(tune));
        var testScene = new BasicTestScene();
        var startPosition = new Vector3(15, 5, 0);

        var playerGameObject = testScene.GameObjects.First();
        playerGameObject.Transform.Position = startPosition;
        
        // engine.PhysicsEngine.Forces.Add(new GravityForce());
        engine.SceneManager.GameScenes.Add(testScene);
        engine.SceneManager.SelectScene(testScene.Id);
        
        engine.InputManager.OnKeyboardKeyPress(data =>
        {   
            switch (data)
            {
                case { KeyboardKey: KeyboardKey.W }:
                    playerGameObject.Transform.Position += Vector3.UnitY;
                    break;
                case { KeyboardKey: KeyboardKey.S }:
                    playerGameObject.Transform.Position -= Vector3.UnitY;
                    break;
                case { KeyboardKey: KeyboardKey.A }:
                    playerGameObject.Transform.Position -= Vector3.UnitX;
                    break;
                case { KeyboardKey: KeyboardKey.D }:
                    playerGameObject.Transform.Position += Vector3.UnitX;
                    break;
                
                case { KeyboardKey: KeyboardKey.Space }:
                    playerGameObject.Transform.Position = startPosition;
                    break;
                
                case { KeyboardKey: KeyboardKey.Escape }:
                    Environment.Exit(0);
                    break;
            }
        });
        
        
        engine.Initialize(renderer);
        
        var simulator = new Simulator(deltaTime =>
        {
            engine.Update(new UpdateArgs(deltaTime, TimeSpan.Zero));
            engine.Draw();
            
            Console.WriteLine($"PlayerPosition: {playerGameObject.Transform.Position}");
            Console.WriteLine($"PlayerVelocity: {playerGameObject.Rigidbody.Velocity}");
        })
        {
            SimulationSpeed = 5f,
            TimeIncrement = TimeSpan.FromMilliseconds(33.33),
            MaxRunningTime = TimeSpan.FromMinutes(5)
        };
        
        simulator.Start();
        
        Environment.Exit(0);
    }
}