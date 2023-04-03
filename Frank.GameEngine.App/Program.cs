using Frank.GameEngine.App;
using Frank.GameEngine.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddGame();
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();