using Frank.GameEngine.Primitives;
using Microsoft.Xna.Framework;
using SharpHook.Native;
using Color = System.Drawing.Color;

namespace Frank.GameEngine.Rendering.MonoGame;

public class MonogameEngine : Game
{
    private readonly Core.GameEngine _gameEngine;
    private readonly IGraphicsDeviceContext _graphicsDeviceContext;
    private readonly IRenderer _renderer;

    public MonogameEngine(Core.GameEngine gameEngine)
    {
        _gameEngine = gameEngine;
        _graphicsDeviceContext = new GraphicsDeviceContext(new GraphicsDeviceManager(this));
        _renderer = new MonoGameRenderer(_graphicsDeviceContext);
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        var shape = ShapeFactory.CreateCube(Color.Chartreuse, 5f);
        var transform = TransformFactory.CreateTransform();
        var gameObject = GameObjectFactory.CreateGameObject(transform, shape, "Test Object");
        var camera = new Camera();
        var scene = new Scene("Test Scene", camera);
        scene.GameObjects.Add(gameObject);

        _gameEngine.InputManager.OnKeyboardKeyPress(x =>
        {
            if (x.KeyCode == KeyCode.VcEscape)
                Exit();
        });

        _gameEngine.SceneManager.GameScenes.Add(scene);
        _gameEngine.SceneManager.SelectScene(scene.Id);
        _gameEngine.Initialize(_renderer);
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        // _gameEngine.Update(new UpdateArgs(gameTime.ElapsedGameTime, gameTime.TotalGameTime));
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        _gameEngine.Draw();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        Exit();
        Environment.Exit(0);
    }
}