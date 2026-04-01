using System.Numerics;
using Frank.GameEngine.Audio.Console;
using Frank.GameEngine.Audio.Midi;
using Frank.GameEngine.Audio.Ogg;
using Frank.GameEngine.Core;
using Frank.GameEngine.Input;
using Frank.GameEngine.Physics;
using Frank.GameEngine.Physics.Forces;
using Frank.GameEngine.Primitives;
using Frank.GameEngine.Rendering.RayLib;
using Pong;
using Pong.Ai;
using Pong.GameObjects;
using Pong.GameObjects.Walls;
using Pong.Scenes;
using PhysicsEngine = Frank.GameEngine.Physics.PhysicsEngine;

var physics = new PhysicsEngine(new NullCollisionHandler());

physics.Forces.Add(new DragForce(1f));

IAudioPlayer audioPlayer = OperatingSystem.IsWindows()
    ? new ConsoleAudioPlayer(new TuneLibrary())
    : new SilentAudioPlayer();
var engine = new GameEngine(physics, audioPlayer);
var camera = new Camera()
{
    Position = new Vector3(0, 0, 1000),
    Target = new Vector3(0, 0, 0),
    AspectRatio = 1.6666666666f,
    FieldOfView = Constants.MathConstants.PiOver4,
    NearPlaneDistance = 1f,
    FarPlaneDistance = 1_000f
};
var renderer = new RayLibRenderer(1080, camera.AspectRatio, "Frank.GameEngine.Samples.Pong");

var playerMoveSpeed = GameConstants.PaddleSpeed;

var scene = new PongBoard(camera)
{
    Player = new PlayerPaddle(),
    Computer = new ComputerPaddle(),
    Ball = new Ball()
};

scene.GameObjects.Add(scene.Player);
scene.GameObjects.Add(scene.Computer);
scene.GameObjects.Add(scene.Ball);

scene.GameObjects.Add(new LeftWall());
scene.GameObjects.Add(new RightWall());
scene.GameObjects.Add(new TopWall());
scene.GameObjects.Add(new BottomWall());

engine.SceneManager.GameScenes.Add(scene);
engine.SceneManager.SelectScene(scene.Id);

var ai = new PongAi(scene.Computer, scene.Ball);

engine.Input.OnKeyboardKeyPress(data =>
{
    switch (data)
    {
        case { KeyboardKey: KeyboardKey.W }:
            scene.Player.MoveDown(playerMoveSpeed);
            break;
        case { KeyboardKey: KeyboardKey.S }:
            scene.Player.MoveUp(playerMoveSpeed);
            break;

        case { KeyboardKey: KeyboardKey.Space }:
            scene.Player.Transform.Position = Vector3.Zero;
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

var totalTime = TimeSpan.Zero;

var simulator = new Simulator(deltaTime =>
{
    totalTime += deltaTime;
    // ai.Update();
    engine.Update(new UpdateArgs(deltaTime, totalTime));
    engine.Draw();
    Console.WriteLine($"Total time: {totalTime}");
})
{
    SimulationSpeed = 1f,
    TimeIncrement = TimeSpan.FromMilliseconds(33.33),
    MaxRunningTime = TimeSpan.FromMinutes(5)
};

try
{
    simulator.Start();
}
finally
{
    engine.Dispose();
}