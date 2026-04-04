using System.Numerics;
using Frank.GameEngine.Audio.Ogg;
using Frank.GameEngine.Core;
using Frank.GameEngine.Primitives;
using Frank.GameEngine.Rendering.RayLib;

var camera2D = new Camera2D
{
    Target = new Vector2(0f, 0f),
    Zoom = 1f,
    ViewportWidth = 800,
    ViewportHeight = 600
};

var scene = new Scene2D("Hello2D", camera2D)
{
    BackgroundColor = new Rgba32(30, 32, 40)
};

scene.GameObjects.Add(new GameObject2D
{
    Name = "Block",
    ZOrder = 0,
    Transform = new Transform2D { Position = Vector2.Zero },
    Sprite = new Sprite2D
    {
        Size = new Vector2(160f, 96f),
        Tint = Rgba32.Chartreuse
    }
});

scene.GameObjects.Add(new GameObject2D
{
    Name = "Orb",
    ZOrder = 1,
    Transform = new Transform2D { Position = new Vector2(48f, -24f), RotationDegrees = 22f },
    Sprite = new Sprite2D
    {
        Size = new Vector2(64f, 64f),
        Tint = Rgba32.Crimson
    }
});

var engine = new GameEngine2D(new SilentAudioPlayer(), new Frank.GameEngine.Input.NullInputSource());
engine.Scene2DManager.GameScenes.Add(scene);
engine.Scene2DManager.SelectScene(scene.Id);

var renderer = new RayLibRenderer2D(800, 600, "Frank.GameEngine 2D");
engine.Initialize(renderer);

try
{
    while (!renderer.ShouldClose)
        engine.Draw();
}
finally
{
    engine.Dispose();
}
