using Frank.GameEngine._2D;
using Frank.GameEngine._2D.Extensions;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        context.HostingEnvironment.ContentRootPath = AppContext.BaseDirectory;

        services.AddGame(context.Configuration);
        services.AddHostedService<GameHost>();
    })
    .Build();

await host.RunAsync();