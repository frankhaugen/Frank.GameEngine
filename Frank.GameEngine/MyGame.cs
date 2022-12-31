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
            Gravity = 9.81f,
            Medium = new Fluid(FluidName.Air),
            Wind = Vector2.Zero
        });

        _gameWorld = new GameWorld(renderer, physics);

        Window.Title = "My Game";
        IsMouseVisible = true;
        var position = Vector2.Zero;
        var position2 = new Vector2(_graphics.PreferredBackBufferWidth / 4f, _graphics.PreferredBackBufferHeight / 2f);

        var gameObject = new GameObject()
        {
            Name = "ArtilleryProjectile",
            Mass = 100f,
            Velocity = position,
            //Velocity = DirectionsCalculator.HeadingAndSpeedToVector2(45f, 1f),
            Color = Color.White,
            Position = position2,
            Polygon = PolygonFactory.GetArtilleryShellPolygon(position, 50),
            CollissionEnabled = true,
            PhysicsEnebled = true
        };

        _gameWorld.AddGameObject(gameObject);
        base.Initialize();
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
