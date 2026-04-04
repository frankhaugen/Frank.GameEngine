using System.Numerics;
using Frank.GameEngine.Assets;
using Frank.GameEngine.Audio.Ogg;
using Frank.GameEngine.Core;
using Frank.GameEngine.Physics;
using Frank.GameEngine.Primitives;
using Frank.GameEngine.Rendering.RayLib;
using Raylib_CSharp.Interact;
using PhysicsEngine = Frank.GameEngine.Physics.PhysicsEngine;

const float moveSpeed = 12f;
const float mouseSensitivity = 0.0025f;

var modelsDir = Path.Combine(AppContext.BaseDirectory, "Models");
var levelPath = Path.Combine(modelsDir, "level.obj");
var cratePath = Path.Combine(modelsDir, "crate.obj");

var levelMesh = SceneMeshImporter.ImportFile(levelPath);
var crateMesh = SceneMeshImporter.ImportFile(cratePath);

var camera = new Camera
{
    Position = Vector3.Zero,
    Target = Vector3.UnitZ,
    AspectRatio = 16f / 9f,
    NearPlaneDistance = 0.1f,
    FarPlaneDistance = 500f
};

var fps = new FpsCameraState
{
    Position = new Vector3(0f, 1.6f, 8f),
    Yaw = MathF.PI
};

var renderer = new RayLibRenderer(1280, camera.AspectRatio, "Frank.GameEngine FPS (OBJ/FBX via Assimp)");
var physics = new PhysicsEngine(new NullCollisionHandler());
var engine = new GameEngine(physics, new SilentAudioPlayer(), new Frank.GameEngine.Input.NullInputSource());

var scene = new Scene("FPS", camera)
{
    BackgroundColor = new Rgba32(20, 24, 32)
};

scene.GameObjects.Add(new GameObject
{
    Name = "Ground",
    Transform = TransformFactory.CreateTransform(),
    Shape = new Shape
    {
        Polygon = new Polygon(Array.Empty<Vector3>()),
        TriangleMesh = levelMesh,
        Color = new Rgba32(60, 70, 90)
    }
});

scene.GameObjects.Add(new GameObject
{
    Name = "Crate",
    Transform = new Transform
    {
        Position = new Vector3(0f, 0.5f, 2f),
        Scale = 1f
    },
    Shape = new Shape
    {
        Polygon = new Polygon(Array.Empty<Vector3>()),
        TriangleMesh = crateMesh,
        Color = Rgba32.Crimson
    }
});

engine.SceneManager.GameScenes.Add(scene);
engine.SceneManager.SelectScene(scene.Id);
engine.Initialize(renderer);

try
{
    while (!renderer.ShouldClose)
    {
        var dt = Math.Clamp(renderer.FrameDeltaSeconds, 0f, 0.1f);
        var delta = TimeSpan.FromSeconds(dt);

        var deltaMouse = Input.GetMouseDelta();
        fps.AddLookDelta(-deltaMouse.X * mouseSensitivity, -deltaMouse.Y * mouseSensitivity);

        var forward = fps.GetForward();
        forward.Y = 0;
        if (forward.LengthSquared() > 1e-6f)
            forward = Vector3.Normalize(forward);

        var right = fps.GetRight();
        right.Y = 0;
        if (right.LengthSquared() > 1e-6f)
            right = Vector3.Normalize(right);

        var move = Vector3.Zero;
        if (Input.IsKeyDown(KeyboardKey.W))
            move += forward;
        if (Input.IsKeyDown(KeyboardKey.S))
            move -= forward;
        if (Input.IsKeyDown(KeyboardKey.D))
            move += right;
        if (Input.IsKeyDown(KeyboardKey.A))
            move -= right;
        if (move.LengthSquared() > 1e-6f)
            fps.Position += Vector3.Normalize(move) * (moveSpeed * dt);

        if (Input.IsKeyDown(KeyboardKey.Escape))
            break;

        fps.ApplyTo(scene.Camera, verticalFovDegrees: 72f, aspectRatio: scene.Camera.AspectRatio);

        engine.Update(new UpdateArgs { ElapsedTime = delta });
        engine.Draw();
    }
}
finally
{
    engine.Dispose();
}
