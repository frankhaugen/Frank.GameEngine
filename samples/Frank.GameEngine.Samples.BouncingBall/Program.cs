using System.Numerics;
using BouncingBall;
using Frank.GameEngine.Audio.Console;
using Frank.GameEngine.Audio.Midi;
using Frank.GameEngine.Core;
using Frank.GameEngine.Input;
using Frank.GameEngine.Physics;
using Frank.GameEngine.Physics.Forces;
using Frank.GameEngine.Primitives;
using Frank.GameEngine.Rendering.RayLib;
using PhysicsEngine = Frank.GameEngine.Physics.PhysicsEngine;

Console.WriteLine("Hello, World!");

// var renderer = new RayLibRenderer();

var camera = new Camera()
{
    Position = new Vector3(0, -100, 500),
    AspectRatio = GameConstants.AspectRatio,
    FieldOfView = Constants.MathConstants.PiOver4,
    NearPlaneDistance = 1f,
    FarPlaneDistance = 1_000f,
    Target = new Vector3(0, 0, 0)
};

var renderer = new RayLibRenderer(1080, camera.AspectRatio, "Bouncing Ball");
// var renderer = new ConsoleRenderer(System.Console.BufferWidth, camera.AspectRatio);

var physics = new PhysicsEngine(new NullCollisionHandler());
// physics.Forces.Add(new GravityForce());

var engine = new GameEngine(physics, new ConsoleAudioPlayer(new TuneLibrary()));

var scene = new MainScene("Main", camera);
// scene.GameObjects.Add(new Floor());
scene.GameObjects.Add(new Ball());

engine.SceneManager.GameScenes.Add(scene);
engine.SceneManager.SelectScene(scene.Id);

engine.InputManager.OnKeyboardKeyPress(data =>
{
    switch (data)
    {
        case { KeyboardKey: KeyboardKey.Space }:
            scene.Camera.Position = Vector3.Zero;
            scene.Camera.Target = Vector3.Zero;
            break;

        case { KeyboardKey: KeyboardKey.Escape }:
            Environment.Exit(0);
            break;
        
        case { KeyboardKey: KeyboardKey.LeftShift }:
            Console.WriteLine("Camera zooming in");
            scene.Camera.MoveForward(1);
            break;
        
        case { KeyboardKey: KeyboardKey.LeftControl }:
            Console.WriteLine("Camera zooming out");
            scene.Camera.MoveBackward(1);
            break;
        
        case { KeyboardKey: KeyboardKey.Up }:
            Console.WriteLine("Camera moving up");
            scene.Camera.MoveUp(1);
            break;
        
        case { KeyboardKey: KeyboardKey.Down }:
            Console.WriteLine("Camera moving down");
            scene.Camera.MoveDown(1);
            break;
        
        case { KeyboardKey: KeyboardKey.Left }:
            Console.WriteLine("Camera moving left");
            scene.Camera.MoveLeft(1);
            break;
        
        case { KeyboardKey: KeyboardKey.Right }:
            Console.WriteLine("Camera moving right");
            scene.Camera.MoveRight(1);
            break;
    }
});

engine.Initialize(renderer);

var simulator = new Simulator(SimulationStep);
simulator.MaxRunningTime = TimeSpan.FromMinutes(5);
simulator.Start();

void SimulationStep(TimeSpan elapsedTime)
{
    engine.Update(new UpdateArgs() {ElapsedTime = elapsedTime});
    engine.Draw();
    Console.WriteLine($"Ball Position: {scene.GameObjects[0].Transform.Position}");
}

namespace BouncingBall
{
    public static class GameConstants
    {
        public const int ScreenWidth = 1920 / 8;
        public const int ScreenHeight = 1080 / 8;
        public const float AspectRatio = (float)ScreenWidth / ScreenHeight;
        public const int TargetFps = 60;
        public const int TargetMs = 1000 / TargetFps;
        public const int BallRadius = 10;
        public const int BallSpeed = 10;
    }
}