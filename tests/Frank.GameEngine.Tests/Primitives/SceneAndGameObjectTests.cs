using FluentAssertions;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Tests.Primitives;

public class SceneAndGameObjectTests
{
    [Test]
    public void Scene_GeneratesUniqueIds()
    {
        var camera = new Camera();
        var a = new Scene("a", camera);
        var b = new Scene("b", camera);

        a.Id.Should().NotBe(b.Id);
        a.Name.Should().Be("a");
        b.Name.Should().Be("b");
    }

    [Test]
    public void GameObject_HasUniqueId()
    {
        var g1 = new GameObject();
        var g2 = new GameObject();

        g1.Id.Should().NotBe(g2.Id);
        g1.IsActive.Should().BeTrue();
    }

    [Test]
    public void Scene_GameObjects_CanAddAndEnumerate()
    {
        var scene = new Scene("s", new Camera());
        var go = new GameObject { Name = "player" };
        scene.GameObjects.Add(go);

        scene.GameObjects.Should().ContainSingle().Which.Should().BeSameAs(go);
    }
}
