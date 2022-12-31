using Frank.GameEngine.Core;
using Microsoft.Xna.Framework;

public class MyGame : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private IGameWorld _gameWorld;

    public MyGame()
    {
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 800,
            PreferredBackBufferHeight = 600
        };
    }

    protected override void Initialize()
    {
        var renderer = new Renderer(_graphics);
        var physics = new Physics(new EnvironmentalFactors
        {
            Gravity = -9.81f,
            Medium = new Fluid(FluidName.Hydrogen),
            Wind = Vector2.Zero
        });

        _gameWorld = new GameWorld(renderer, physics);

        Window.Title = "My Game";
        IsMouseVisible = true;
        GraphicsDevice.Clear(Color.CornflowerBlue);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        var position = Vector2.Zero;
        var position2 = new Vector2(0f, _graphics.PreferredBackBufferHeight / 2f);

        var gameObject = new GameObject()
        {
            Name = "ArtilleryProjectile",
            Mass = 1f,
            Velocity = DirectionsCalculator.HeadingAndSpeedToVector2(45f, 100f),
            Color = Color.White,
            Position = position2,
            Polygon = PolygonFactory.GetArtilleryShellPolygon(position, 50),
        };

        _gameWorld.AddGameObject(gameObject);
    }

    protected override void Update(GameTime gameTime)
    {
        _gameWorld.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _gameWorld.Render(gameTime);
    }

    protected override void Dispose(bool disposing)
    {
        _gameWorld.Dispose();
        base.Dispose(disposing);
    }
}
