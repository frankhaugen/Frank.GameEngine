// See https://aka.ms/new-console-template for more information

using BouncingBall;
using Frank.GameEngine.Audio.Console;
using Frank.GameEngine.Audio.Midi;
using Frank.GameEngine.Core;
using Frank.GameEngine.Physics;
using Frank.GameEngine.Primitives;
using Frank.GameEngine.Rendering.Console;

Console.WriteLine("Hello, World!");

var renderer = new ConsoleRenderer(GameConstants.ScreenWidth, GameConstants.ScreenHeight / GameConstants.ScreenWidth);
var physics = new PhysicsEngine(new CollisionHandler());

var engine = new GameEngine(physics, new ConsoleAudioPlayer(new TuneLibrary()));
var camera = new Camera();

var scene = new MainScene("Main", camera);
scene.GameObjects.Add(new Floor());
scene.GameObjects.Add(new Ball());

engine.SceneManager.GameScenes.Add(scene);
engine.SceneManager.SelectScene(scene.Id);

public static class GameConstants
{
    public const int ScreenWidth = 1920;
    public const int ScreenHeight = 1080;
    public const int TargetFps = 60;
    public const int TargetMs = 1000 / TargetFps;
    public const int BallRadius = 10;
    public const int BallSpeed = 10;
}