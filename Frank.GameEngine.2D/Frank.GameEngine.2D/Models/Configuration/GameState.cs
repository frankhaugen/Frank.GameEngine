﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine._2D.Models.Configuration;

public class GameState
{
    public SpriteBatch SpriteBatch { get; set; }
    public GameTime GameTime { get; set; }
    public Vector2 Center { get; set; }
    public Point MousePointerPosition { get; set; }
}