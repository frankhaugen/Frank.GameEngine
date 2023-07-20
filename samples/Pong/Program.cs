using Frank.GameEngine.Core;
using Frank.GameEngine.Input;
using Frank.GameEngine.Primitives;
using Frank.GameEngine.Rendering.Console;
using Pong.Ai;
using Pong.GameObjects;
using Pong.Scenes;
using System.Numerics;

var renderer = new ConsoleRenderer(240, 4);
var engine = new GameEngine();
var camera = new Camera();

var playerMoveSpeed = 0.5f;


var scene = new PongBoard(camera);
scene.Player = new PlayerPaddle();
scene.Computer = new ComputerPaddle();
scene.Ball = new Ball();

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

engine.InputManager.OnKeyboardKeyPress(data =>
{
    switch (data)
    {
        case { KeyboardKey: KeyboardKey.W }:
            scene.Player.MoveUp(playerMoveSpeed);
            break;
        case { KeyboardKey: KeyboardKey.S }:
            scene.Player.MoveDown(playerMoveSpeed);
            break;
        case { KeyboardKey: KeyboardKey.A }:
            scene.Player.MoveLeft(playerMoveSpeed);
            break;
        case { KeyboardKey: KeyboardKey.D }:
            scene.Player.MoveRight(playerMoveSpeed);
            break;
        case { KeyboardKey: KeyboardKey.Space }:
            scene.Player.Transform.Position = Vector3.Zero;
            break;
        case { KeyboardKey: KeyboardKey.Escape }:
            Environment.Exit(0);
            break;
    }
});

engine.Initialize(renderer);

var simulator = new Simulator(deltaTime =>
{
    ai.Update();
    engine.Update(new UpdateArgs(deltaTime, TimeSpan.Zero));
    engine.Draw();

    Console.WriteLine($"{scene.ScoreBoard}");
    Console.WriteLine($"Player Position: {scene.Player.Transform.Position}");
    Console.WriteLine($"Computer Position: {scene.Computer.Transform.Position}");
    Console.WriteLine($"Ball Position: {scene.Ball.Transform.Position}");
})
{
    SimulationSpeed = 1f,
    TimeIncrement = TimeSpan.FromMilliseconds(33.33),
    MaxRunningTime = TimeSpan.FromMinutes(5)
};

simulator.Start();

Environment.Exit(0);