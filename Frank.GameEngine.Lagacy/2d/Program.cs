using Frank.GameEngine.Lagacy._2d;
using Frank.GameEngine.Lagacy._2d.Extensions;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        context.HostingEnvironment.ContentRootPath = AppContext.BaseDirectory;

        services.AddGame(context.Configuration);
        services.AddHostedService<GameHost>();
    })
    .Build();

await host.RunAsync();
// var host = Host.CreateDefaultBuilder(args)
//     .ConfigureServices(services => { services.AddHostedService<Worker>(); })
//     .Build();
//
// host.Run();