using Frank.GameEngine.Lagacy._2d.Extensions;
using Frank.GameEngine.Lagacy._2d.Models.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input.InputListeners;

namespace Frank.GameEngine.Lagacy._2d;

internal class GameWindow : Game, IGameWindow
{
    private readonly ILogger<GameWindow> _logger;
    private readonly IOptions<GameState> _gameState;
    private readonly IDrawer _drawer;
    private readonly IInputService _inputService;

    public Vector2 Center { get; private set; }
    public Game Game => this;

    private SpriteFont _spriteFont;

    public Point MousePosition { get; set; }

    public GameWindow(IOptions<GameOptions> gameOptions, ILogger<GameWindow> logger, IOptions<GameState> gameState, IDrawer drawer, IInputService inputService)
    {
        _logger = logger;
        _gameState = gameState;
        _drawer = drawer;
        _inputService = inputService;

        Content.RootDirectory = nameof(Content);

        MaxElapsedTime = TimeSpan.FromSeconds(5);
        TargetElapsedTime = TimeSpan.FromSeconds(0.33);

        IsFixedTimeStep = gameOptions.Value.FixedTimeStep;
        IsMouseVisible = gameOptions.Value.ShowPointer;
        Window.AllowUserResizing = gameOptions.Value.AllowUserResizing;
        Window.ClientSizeChanged += (_, _) => Center = GraphicsDevice.Viewport.Bounds.Center.ToVector2();
    }

    protected override void LoadContent()
    {
        _gameState.Value.SpriteBatch = GraphicsDevice.CreateSpriteBatch();
        Center = GraphicsDevice.Viewport.Bounds.Center.ToVector2();
        BallPosition = GraphicsDevice.Viewport.Bounds.Center.ToVector2();

        _spriteFont = Content.Load<SpriteFont>("Text");
        _inputService.GuiMouseListener.MouseMoved += GuiMouseListenerOnMouseMoved;
        base.LoadContent();
    }

    private void GuiMouseListenerOnMouseMoved(object? sender, MouseEventArgs e)
    {
        MousePosition = e.Position;
    }

    protected override void Update(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _gameState.Value.GameTime = gameTime;

        _inputService.GuiMouseListener.Update(gameTime);
        _inputService.GuiKeyboardListener.Update(gameTime);
        _inputService.GuiGamePadListener.Update(gameTime);
        _inputService.GuiTouchListener.Update(gameTime);

        _drawer.Begin();

        BallPosition = NextPosition(BallPosition, _gameState.Value.GameTime.TotalGameTime.Seconds, 10);

        _drawer.DrawCircle(BallPosition, 10, 42, Color.Aqua);
        Console.WriteLine(BallPosition.ToString());

        _drawer.DrawString(_spriteFont, MousePosition.ToString(), GraphicsDevice.GetOrigin().ToVector2(), Color.Aqua);
        _drawer.DrawString(_spriteFont, Center.ToString(), GraphicsDevice.GetOrigin().ToVector2().Add(500, 0), Color.Aqua);
        _drawer.DrawString(_spriteFont, BallPosition.ToString(), GraphicsDevice.GetOrigin().ToVector2().Add(0, 25), Color.Aqua);

        _drawer.End();

        base.Update(gameTime);
    }

    public Vector2 BallPosition { get; set; }

    Vector2 NextPosition(Vector2 origin, float time, float angle)
    {
        var gravity = -9.80665F;
        var gravityModifier = 0.01F;

        gravity = gravity * gravityModifier;

        var Sx = origin.X * MathF.Cos(ToRadian(angle)) * time;
        var Sy = origin.Y * MathF.Sin(ToRadian(angle)) * time - 0.5F * gravity * MathF.Pow(time, 2);

        return origin.Translate(new Vector2(Sx, Sy));
    }

    Vector2 NextPositionX(Vector2 origin, float time, float angle)
    {
        var gravity = -9.80665F;

        var Sx = origin.X * MathF.Cos(ToRadian(angle)) * time;
        var Sy = origin.Y * MathF.Sin(ToRadian(angle)) * time - 0.5F * gravity * MathF.Pow(time, 2);

        return origin.Translate(new Vector2(Sx, Sy));
    }

    Vector2 NextPositionY(Vector2 origin, float instant, float launchAngle, float initialVelocity = 12f)
    {
        origin.X = CalculateHorizontalVelocity(instant, initialVelocity, launchAngle);
        origin.Y = CalculateVerticalVelocity(instant, initialVelocity, launchAngle);
        return origin;
    }

    float CalculateHorizontalVelocity(float instant, float initialVelocity, float launchAngle) => initialVelocity * MathF.Cos(ToRadian(launchAngle)) * instant;
    float CalculateVerticalVelocity(float instant, float initialVelocity, float launchAngle) => initialVelocity * MathF.Sin(ToRadian(launchAngle)) * instant - 0.5F * 9.81F * instant * instant;

    float ToRadian(float angle) => angle * (MathF.PI / 180);
}