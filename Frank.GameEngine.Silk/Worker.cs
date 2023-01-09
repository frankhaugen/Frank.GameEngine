using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Numerics;

namespace Frank.GameEngine.Silk;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    private IWindow window;
    private PyramidRenderer renderer;
    private Pyramid pyramid = new Pyramid();
    private GL _openGL;
    
    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Create a window
        var windowOptions = WindowOptions.Default;
        windowOptions.Title = "My 3D Scene";
        windowOptions.Size = new Vector2D<int>(800, 600);
        
        window = Window.Create(windowOptions);

        //Assign events.
        window.Load += OnLoad;
        window.Update += OnUpdate;
        window.Render += OnRender;

        //Run the window.
        window.Run();
    }
    
    
    private void OnLoad()
    {
        //Set-up input context.
        IInputContext input = window.CreateInput();
        for (int i = 0; i < input.Keyboards.Count; i++)
        {
            input.Keyboards[i].KeyDown += KeyDown;
        }
        
        //Set-up OpenGL.
        _openGL = GL.GetApi(window);
        renderer = new PyramidRenderer(_openGL, pyramid);
        renderer.Initialize();
    }

    private void OnRender(double obj)
    {
        renderer.Render();
    }

    private void OnUpdate(double obj)
    {
        //Here all updates to the program should be done.
    }

    private void KeyDown(IKeyboard arg1, Key arg2, int arg3)
    {
        //Check to close the window on escape.
        if (arg2 == Key.Escape)
        {
            window.Close();
        }
    }
}
public class Pyramid
{
    public List<Vector3> Vertices { get; }
    public List<int> Indices { get; }

    public Pyramid()
    {
        // Create the vertices for the pyramid
        Vertices = new List<Vector3>
        {
            new Vector3(-1.0f, -1.0f, 1.0f), // bottom left
            new Vector3(1.0f, -1.0f, 1.0f),  // bottom right
            new Vector3(1.0f, -1.0f, -1.0f), // top right
            new Vector3(-1.0f, -1.0f, -1.0f), // top left
            new Vector3(0.0f, 1.0f, 0.0f)     // top
        };

        // Create the indices for the pyramid
        Indices = new List<int>
        {
            // bottom face
            0, 1, 2,
            0, 2, 3,

            // front face
            0, 4, 1,

            // right face
            1, 4, 2,

            // back face
            2, 4, 3,

            // left face
            3, 4, 0
        };
    }
}

public class PyramidRenderer
{
    private readonly GL _gl;
    private readonly Pyramid _pyramid;

    private uint _vertexArrayObject;
    private uint _vertexBufferObject;
    private uint _elementBufferObject;

    public PyramidRenderer(GL gl, Pyramid pyramid)
    {
        _gl = gl;
        _pyramid = pyramid;
    }

    public void Initialize()
    {
        // Generate the vertex array object, vertex buffer object, and element buffer object
        _vertexArrayObject = _gl.GenVertexArray();
        _vertexBufferObject = _gl.GenBuffer();
        _elementBufferObject = _gl.GenBuffer();

        // Bind the vertex array object
        _gl.BindVertexArray(_vertexArrayObject);

        // Bind the vertex buffer object and upload the vertices to it
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vertexBufferObject);
        // _gl.BufferData(BufferTargetARB.ArrayBuffer, (uint)_pyramid.Vertices.Count, _pyramid.Vertices.ToArray(), BufferUsageARB.StaticDraw);

        // Bind the element buffer object and upload the indices to it
        _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _elementBufferObject);
        // _gl.BufferData(BufferTargetARB.ElementArrayBuffer, (uint)_pyramid.Indices.Count, _pyramid.Indices.ToArray(), BufferUsageARB.StaticDraw);
    }

    public void Render()
    {
        // Bind the vertex array object and draw the pyramid using the indices
        _gl.BindVertexArray(_vertexArrayObject);
        _gl.DrawElements(PrimitiveType.Triangles, (uint)_pyramid.Indices.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
    }
}