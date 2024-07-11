using Frank.GameEngine.Rendering.RayLib;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(x => x.AddConsole())
    // .ConfigureServices(services => { services.AddHostedService<ConsoleRendererWorker>(); })
    .ConfigureServices(services => { services.AddGameEngine(); })
    .Build();
 
host.Run();