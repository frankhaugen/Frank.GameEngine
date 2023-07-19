using Frank.GameEngine.Assets.Testing.Scenes;
using Frank.GameEngine.Core;
using Frank.GameEngine.Rendering.Console;
using SharpHook.Native;
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
        var engine = new Core.GameEngine();
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
                case { KeyCode: KeyCode.VcW }:
                    playerGameObject.Transform.Position += Vector3.UnitY;
                    break;
                case { KeyCode: KeyCode.VcS }:
                    playerGameObject.Transform.Position -= Vector3.UnitY;
                    break;
                case { KeyCode: KeyCode.VcA }:
                    playerGameObject.Transform.Position -= Vector3.UnitX;
                    break;
                case { KeyCode: KeyCode.VcD }:
                    playerGameObject.Transform.Position += Vector3.UnitX;
                    break;
                
                case { KeyCode: KeyCode.VcSpace }:
                    playerGameObject.Transform.Position = startPosition;
                    break;
                
                case { KeyCode: KeyCode.VcEscape }:
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