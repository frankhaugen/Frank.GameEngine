using Frank.GameEngine.Core;
using Frank.GameEngine.Core.Graphics.Management;
using Frank.GameEngine.Core.Graphics.Rendering;
using Frank.GameEngine.Core.Input;
using Frank.GameEngine.Core.Interfaces;
using Frank.GameEngine.Core.Physics;
using Frank.GameEngine.Core.Services;
using Frank.GameEngine.Core.Shapes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine;

internal class Block : IGameObject
{
    public string Name { get; set; }
    public ITransform Transform { get; set; }
    public IShape Shape { get; set; }
    public GameObjectOptions Options { get; set; }

    public Block(string name, ITransform transform, IShape shape, GameObjectOptions options)
    {
        Name = name;
        Transform = transform;
        Shape = shape;
        Options = options;
    }
}

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddGame(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging(x => x
                .ClearProviders()
                .AddJsonConsole()
            )
            .Configure<GameOptions>(configuration.GetSection(nameof(GameOptions)))
            .AddSingleton<GameService>()
            .AddSingleton<IDrawService, DrawService>()
            .AddSingleton<IUpdateService, UpdateService>()
            .AddSingleton<IGraphicsManager, GraphicsManager>()
            .AddSingleton<IRenderer, Renderer3D>()
            // .AddSingleton<IRenderer, SpriteBatchRenderer>()
            .AddSingleton<ICamera3D, Camera3D>()
            .AddSingleton<IInputHandler, InputHandler>()
            .AddSingleton(new PhysicalForces(new GravityForce()))
            .AddSingleton(new GameObjects(new Block(
                "Me", 
                new Transform(){Position = new Vector3(400, 300, 0), Rotation = Quaternion.Identity, Scale = Vector3.One},
                new Cube(100.0f, Color.Red), 
                new GameObjectOptions()
                {
                    IsPhysical = true,
                    IsVisible = true,
                })))
            .AddHostedService<GameHost>();

        return services;
    }
}