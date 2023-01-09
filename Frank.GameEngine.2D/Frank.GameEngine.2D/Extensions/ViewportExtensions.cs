using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine._2D.Extensions;

public static class ViewportExtensions
{
    public static Point GetOrigin(this Viewport source) => source.Bounds.Location;
}