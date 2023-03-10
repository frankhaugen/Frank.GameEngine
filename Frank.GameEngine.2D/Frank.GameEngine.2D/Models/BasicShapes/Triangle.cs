using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine._2D.Models.BasicShapes;

internal readonly record struct Triangle(VertexPosition A, VertexPosition B, VertexPosition C, Color Color);