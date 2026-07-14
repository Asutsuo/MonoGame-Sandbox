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

    public NewGame(Texture2D textura, SpriteBatch spriteBatch)
    {
        this.textura = textura;
        this.spriteBatch = spriteBatch;
    }

    public void Update()
    {

    }

    /*    public void Draw()
        {
            spriteBatch.Draw(textura, );
        }*/
}
