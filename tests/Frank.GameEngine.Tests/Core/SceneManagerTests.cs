using FluentAssertions;
using Frank.GameEngine.Core;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Tests.Core;

public class SceneManagerTests
{
    [Test]
    public void SelectScene_FindsSceneInGameScenes()
    {
        var camera = new Camera();
        var scene = new Scene("game", camera);
        var manager = new SceneManager();
        manager.GameScenes.Add(scene);

        manager.SelectScene(scene.Id);

        manager.CurrentScene.Should().BeSameAs(scene);
    }

    [Test]
    public void SelectScene_FindsSceneInMenuScenes()
    {
        var scene = new Scene("menu", new Camera());
        var manager = new SceneManager();
        manager.MenuScenes.Add(scene);

        manager.SelectScene(scene.Id);

        manager.CurrentScene.Should().BeSameAs(scene);
    }

    [Test]
    public void SelectScene_SearchesGameScenes_WhenIdNotInMenuScenes()
    {
        var menu = new Scene("menu", new Camera());
        var game = new Scene("game", new Camera());
        var manager = new SceneManager();
        manager.MenuScenes.Add(menu);
        manager.GameScenes.Add(game);

        manager.SelectScene(game.Id);

        manager.CurrentScene.Should().BeSameAs(game);
    }

    [Test]
    public void SelectScene_UnknownId_SetsCurrentSceneToNull()
    {
        var manager = new SceneManager();
        manager.GameScenes.Add(new Scene("a", new Camera()));

        manager.SelectScene(Guid.NewGuid());

        manager.CurrentScene.Should().BeNull();
    }
}
