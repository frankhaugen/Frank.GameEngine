namespace Frank.GameEngine.Rendering;

public class Renderer : IRenderer
{
    private readonly IRenderQueue _renderQueue;

    public Renderer(IRenderQueue renderQueue)
    {
        _renderQueue = renderQueue;
    }

    public void Render()
    {

        while (_renderQueue.TryDequeue(out var polygon))
        {

            foreach (var vertex in polygon.Vertices)
            {
                
            }

        }
    }
    
    public void Initialize()
    {

    }
}