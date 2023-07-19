using Frank.GameEngine.Lagacy.B;

// IHost host = Host.CreateDefaultBuilder(args)
//     .ConfigureServices(services =>
//     {
//         services.AddHostedService<Worker>();
//     })
//     .Build();
//
// host.Run();

var silk = new HelloTriangleApplication();

silk.Run();

namespace Frank.GameEngine.Lagacy.B
{
    unsafe class HelloTriangleApplication
    {
        private const int WIDTH = 800;
        private const int HEIGHT = 600;

        private readonly IWindow _window;
    
        private Vk _vulkan;

        public HelloTriangleApplication()
        {
            //Create a window.
            var options = WindowOptions.DefaultVulkan with
            {
                Size = new Vector2D<int>(WIDTH, HEIGHT),
                Title = "Vulkan"
            };

            _window = Window.Create(options);
        
            _window.Load += OnLoad;
            _window.Update += OnUpdate;
            _window.Render += OnRender;
            _window.Closing += OnClosing;
        
            _vulkan = Vk.GetApi();
        }

        public void Run()
        {
            _window.Initialize();

            if (_window.VkSurface is null)
                throw new Exception("Windowing platform doesn't support Vulkan.");
        }
    
        private void OnLoad()
        {
        }
    
        private void OnClosing()
        {
            _window.Dispose();
            Environment.Exit(0);
        }

        private void OnRender(double obj)
        {
            _vulkan.CmdDraw(new CommandBuffer(), 3, 1, 0, 0);
            // _vulkan..ClearColor(_window.VkSurface, 0.0f, 0.0f, 0.0f, 1.0f);
        }

        private void OnUpdate(double obj)
        {
        }

    }

    public class VulkanRederer
    {
    
    }

    public interface IRenderer
    {
        void Initialize();
        void Render();
    }
}