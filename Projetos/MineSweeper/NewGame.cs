using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SandboxEngine;

namespace MineSweeper;

public class NewGame
{
    private Texture2D textura;
    private SpriteBatch spriteBatch;
    private Rectangle area;
    public Rectangle position = new Rectangle(0, 404, 26, 26);

    public NewGame(Texture2D textura, SpriteBatch spriteBatch)
    {
        this.textura = textura;
        this.spriteBatch = spriteBatch;

        area = new Rectangle(260, 75, 52, 52);
    }

    public void Update()
    {

    }

    public void Draw()
    {
        spriteBatch.Draw(textura, new Rectangle(260, 75, 52, 52), position, Color.White);
    }

    public bool MouseSobre(Point mouse)
    {
        return area.Contains(mouse);
    }

    public bool foiClicado(MouseState anterior, MouseState atual, Point mouse)
    {
        if (atual.LeftButton == ButtonState.Released && anterior.LeftButton == ButtonState.Pressed && MouseSobre(mouse))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}