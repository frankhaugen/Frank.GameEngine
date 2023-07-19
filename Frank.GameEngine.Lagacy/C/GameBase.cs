using Frank.GameEngine.Collections;
using Frank.GameEngine.Extensions;
using Frank.GameEngine.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine;

public class GameBase : Game
{
    private readonly GameObjects _gameObjects;

    public GameBase(GameObjects gameObjects)
    {
        _gameObjects = gameObjects;
    }
    
    protected override void Initialize()
    {
        IsMouseVisible = true;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        var polygon = PolygonFactory.CreateSquare(Vector2.Zero, 1, 1);
        var polygons = new Polygons();
        polygons.Add(polygon);
        
        var myGameObject = GameObjectFactory.CreateTestSquare();
        _gameObjects.Add(myGameObject);
        base.LoadContent();
    }
    
    protected override void Update(GameTime gameTime)
    {
        _gameObjects.Update(gameTime);
        base.Update(gameTime);
    }
    
    // private void EnqueuePolygon()
    // {
    //     // var polygon = PolygonFactory.CreateTriangle(new Vertex(0, 0, 0), new Vertex(1, 0, 0), new Vertex(0, 1, 0));
    //     // var polygon = PolygonFactory.CreatePyramid(new Vertex(0, 0, 0), new Vertex(-1, 0, 0), new Vertex(0, -1, 0), new Vertex(0, 0, -1));
    //     var polygon = PolygonFactory.CreateSquare(new Vertex(0, 0, 0), 1);
    //     // var polygon = PolygonFactory.CreateCube(new Vertex(0, 0, 0), 1);
    //     // var polygon = PolygonFactory.CreateSphere(new Vertex(0, 0, 0), 1, 10);
    //     _renderQueue.Enqueue(polygon);
    // }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        using var effect = new BasicEffect(GraphicsDevice);
        effect.VertexColorEnabled = true;
        effect.World = Matrix.Identity;
        effect.View = Matrix.CreateLookAt(new Vector3(0, 0, 5), Vector3.Zero, Vector3.Up);
        effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1, 100);
        var renderQueue = _gameObjects.GetRenderQueue();
        
        while (renderQueue.TryDequeue(out var polygon))
        {
            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawPolygon(polygon, Color.LawnGreen, PrimitiveType.LineList);
            }
        }

        base.Draw(gameTime);
    }
}