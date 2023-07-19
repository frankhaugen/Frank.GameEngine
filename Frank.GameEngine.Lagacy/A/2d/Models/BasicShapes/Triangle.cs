using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Lagacy.A._2d.Models.BasicShapes;

internal readonly record struct Triangle(VertexPosition A, VertexPosition B, VertexPosition C, Color Color);