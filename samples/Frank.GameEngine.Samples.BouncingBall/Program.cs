using BouncingBall;
using Frank.GameEngine.Audio.Console;
using Frank.GameEngine.Audio.Midi;
using Frank.GameEngine.Core;
using Frank.GameEngine.Physics;
using Frank.GameEngine.Physics.Forces;
using Frank.GameEngine.Primitives;
using Frank.GameEngine.Rendering.Console;

Console.WriteLine("Hello, World!");

// var renderer = new RayLibRenderer();

var camera = new Camera();

// var renderer = new RayLibRenderer(1080, camera.AspectRatio, "Bouncing Ball");
var renderer = new ConsoleRenderer(System.Console.BufferWidth, camera.AspectRatio);

var physics = new PhysicsEngine(new CollisionHandler());
physics.Forces.Add(new GravityForce());

var engine = new GameEngine(physics, new ConsoleAudioPlayer(new TuneLibrary()));

var scene = new MainScene("Main", camera);
scene.GameObjects.Add(new Floor());
scene.GameObjects.Add(new Ball());

engine.SceneManager.GameScenes.Add(scene);
engine.SceneManager.SelectScene(scene.Id);

engine.Initialize(renderer);

var simulator = new Simulator(SimulationStep);

simulator.Start();

void SimulationStep(TimeSpan elapsedTime)
{
    engine.Update(new UpdateArgs() {ElapsedTime = elapsedTime});
    engine.Draw();
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