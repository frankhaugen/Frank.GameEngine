﻿using Frank.GameEngine.Types;

namespace Frank.GameEngine.Rendering;

public class Renderer : IRenderer
{
    private readonly IRenderQueue _renderQueue;
    private readonly IGraphicsDeviceContext _graphicsDeviceContext;

    public Renderer(IRenderQueue renderQueue, IGraphicsDeviceContext graphicsDeviceContext)
    {
        _renderQueue = renderQueue;
        _graphicsDeviceContext = graphicsDeviceContext;
    }
    
    public void RenderPolygon(IPolygon polygon)
    {
        
    }
    
    public void Render()
    {
        // while (_renderQueue.TryDequeue(out var polygon))
        // {
        //     RenderPolygon(polygon);
        // }
    }
    
    public void Initialize()
    {
        _graphicsDeviceContext.Initialize();
    }
}