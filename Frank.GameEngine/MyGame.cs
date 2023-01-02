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


        var gameObject = new GameObject()
        {
            Name = "ArtilleryProjectile",
            Mass = 10f,
            Velocity = Vector2.Zero,
            Direction = Vector2.Zero,
            //Velocity = DirectionsCalculator.HeadingAndSpeedToVector2(45f, 1f),
            Color = Color.White,
            Position = new Vector2(_graphics.PreferredBackBufferWidth / 4f, _graphics.PreferredBackBufferHeight / 2f),
            Polygon = PolygonFactory.GetArtilleryShellPolygon(50),
            CollissionEnabled = true,
            PhysicsEnebled = true
        };

        _gameWorld.AddGameObject(gameObject);
        _gameWorld.AddGameObject(new GameObject()
        {
            Name = "Rock",
            //Texture = _gameWorld.Renderer.GetTexture(RockType.Granite, 60, 60),
            Mass = 10f,
            Velocity = Vector2.Zero,
            Polygon = PolygonFactory.GetSquare(25),
            Position = new Vector2(_graphics.PreferredBackBufferWidth / 2f, _graphics.PreferredBackBufferHeight / 2f),
            CollissionEnabled = true,

            PhysicsEnebled = false
        })
            ;
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
