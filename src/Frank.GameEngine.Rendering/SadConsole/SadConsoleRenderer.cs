using Frank.GameEngine.Primitives;
using SadConsole;
using SadRogue.Primitives;
using Console = SadConsole.Console;

namespace Frank.GameEngine.Rendering.SadConsoleS;

public class SadConsoleRenderer : IRenderer
{
    private readonly SadConsole.Console _console;

    public SadConsoleRenderer(int width,int height)
    {
        _console = new SadConsole.Console(width, height);
    }

    public void Render(Scene scene, Action<string> callback) => Render(scene);

    public void Render(Scene scene)
    {
        _console.Clear();
        foreach (var gameObject in scene.GameObjects)
        {
            var polygon = gameObject.Shape.Polygon;
            var lines = polygon.Edges;
            foreach (var line in lines)
            {
                var start = line.A;
                var end = line.B;
            }
        }
    }

}