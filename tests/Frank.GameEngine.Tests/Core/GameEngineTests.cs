using FluentAssertions;
using Frank.GameEngine.Audio.Ogg;
using Frank.GameEngine.Physics;
using Frank.GameEngine.Primitives;
using Frank.GameEngine.Rendering;
using Moq;
using CoreGameEngine = Frank.GameEngine.Core.GameEngine;

namespace Frank.GameEngine.Tests.Core;

public class GameEngineTests
{
    [Test]
    public void CurrentScene_IsNull_UntilSelected()
    {
        var engine = new CoreGameEngine(new PhysicsEngine(new NullCollisionHandler()), new SilentAudioPlayer());

        engine.CurrentScene.Should().BeNull();
    }

    [Test]
    public void Initialize_Throws_WhenNoCurrentScene()
    {
        var engine = new CoreGameEngine(new PhysicsEngine(new NullCollisionHandler()), new SilentAudioPlayer());
        var renderer = Mock.Of<IRenderer>();

        var act = () => engine.Initialize(renderer);

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("*no current scene*");
    }

    [Test]
    public void Draw_Throws_WhenInitializeNotCalled()
    {
        var engine = new CoreGameEngine(new PhysicsEngine(new NullCollisionHandler()), new SilentAudioPlayer());
        engine.SceneManager.GameScenes.Add(new Scene("s", new Camera()));
        engine.SceneManager.SelectScene(engine.SceneManager.GameScenes[0].Id);

        var act = () => engine.Draw();

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("*Initialize*");
    }

    [Test]
    public void Update_DoesNothing_WhenNoCurrentScene()
    {
        var physics = new PhysicsEngine(new NullCollisionHandler());
        var engine = new CoreGameEngine(physics, new SilentAudioPlayer());

        var act = () => engine.Update(new Frank.GameEngine.Core.UpdateArgs(TimeSpan.FromMilliseconds(16), TimeSpan.Zero));

        act.Should().NotThrow();
    }

    [Test]
    public void Update_RunsPhysics_WhenSceneSelected()
    {
        var collision = new Mock<ICollisionHandler>();
        var physics = new PhysicsEngine(collision.Object);
        var engine = new CoreGameEngine(physics, new SilentAudioPlayer());
        var scene = new Scene("s", new Camera());
        scene.GameObjects.Add(new GameObject());
        engine.SceneManager.GameScenes.Add(scene);
        engine.SceneManager.SelectScene(scene.Id);

        engine.Update(new Frank.GameEngine.Core.UpdateArgs(TimeSpan.FromSeconds(1), TimeSpan.Zero));

        collision.Verify(c => c.HandleCollisions(scene), Times.Once);
    }

    [Test]
    public void Shutdown_IsIdempotent_AndStopsBackgroundWork()
    {
        var engine = new CoreGameEngine(new PhysicsEngine(new NullCollisionHandler()), new SilentAudioPlayer());
        engine.SceneManager.GameScenes.Add(new Scene("s", new Camera()));
        engine.SceneManager.SelectScene(engine.SceneManager.GameScenes[0].Id);
        engine.Initialize(Mock.Of<IRenderer>());

        var act = () =>
        {
            engine.Shutdown();
            engine.Shutdown();
        };

        act.Should().NotThrow();
        engine.IsInitialized.Should().BeFalse();
    }

    [Test]
    public void Initialize_Throws_WhenAlreadyInitialized()
    {
        var engine = new CoreGameEngine(new PhysicsEngine(new NullCollisionHandler()), new SilentAudioPlayer());
        engine.SceneManager.GameScenes.Add(new Scene("s", new Camera()));
        engine.SceneManager.SelectScene(engine.SceneManager.GameScenes[0].Id);
        var renderer = Mock.Of<IRenderer>();
        engine.Initialize(renderer);

        var act = () => engine.Initialize(renderer);

        act.Should().Throw<InvalidOperationException>().WithMessage("*already initialized*");
    }

    [Test]
    public void Dispose_CallsShutdown()
    {
        var engine = new CoreGameEngine(new PhysicsEngine(new NullCollisionHandler()), new SilentAudioPlayer());
        engine.SceneManager.GameScenes.Add(new Scene("s", new Camera()));
        engine.SceneManager.SelectScene(engine.SceneManager.GameScenes[0].Id);
        engine.Initialize(Mock.Of<IRenderer>());

        engine.Dispose();

        engine.IsInitialized.Should().BeFalse();
    }

    [Test]
    public void Initialize_Throws_WhenDisposed()
    {
        var engine = new CoreGameEngine(new PhysicsEngine(new NullCollisionHandler()), new SilentAudioPlayer());
        engine.SceneManager.GameScenes.Add(new Scene("s", new Camera()));
        engine.SceneManager.SelectScene(engine.SceneManager.GameScenes[0].Id);
        engine.Initialize(Mock.Of<IRenderer>());
        engine.Dispose();

        var act = () => engine.Initialize(Mock.Of<IRenderer>());

        act.Should().Throw<ObjectDisposedException>();
    }
}
