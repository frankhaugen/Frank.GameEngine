using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine._2D.Models.BasicShapes;

public readonly record struct Vertices(VertexPositionColor[] VertexArray, int VertexCount, int[] Indicies, int IndexCount);