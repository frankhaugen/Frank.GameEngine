using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Lagacy.A._2d.Models.BasicShapes;

public readonly record struct Vertices(VertexPositionColor[] VertexArray, int VertexCount, int[] Indicies, int IndexCount);