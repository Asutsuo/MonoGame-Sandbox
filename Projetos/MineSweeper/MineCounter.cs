using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SandboxEngine;
using MonoGame;

namespace MineSweeper;

public class MineCounter
{
    private Texture2D textura;
    private SpriteBatch spriteBatch;

    public int unidade = 10;
    private int dezena;

    public int bandeiras = 10;
    public double deltaTime;
    public int contadorBandeiras;

    public MineCounter(Texture2D textura, SpriteBatch spriteBatch)
    {
        this.textura = textura;
        this.spriteBatch = spriteBatch;
    }

    public void Subtrai()
    {
        if (unidade > 0)
        {
            dezena = 0;
            unidade--;
        }

        if (dezena > 0)
        {
            dezena--;
            unidade = 9;
        }
    }

    public void Adiciona()
    {
        if (dezena < 1)
        {
            unidade++;
        }
    }

    public void Update()
    {
        if (unidade > 9)
        {
            unidade = 0;
            dezena++;
        }
    }

    public void Draw()
    {
        spriteBatch.Draw(textura, new Rectangle(72, 82, 22, 42), new Rectangle(new Point(0 + (11 * dezena), 383), new Point(11, 21)), Color.White);
        spriteBatch.Draw(textura, new Rectangle(98, 82, 22, 42), new Rectangle(new Point(0 + (11 * unidade), 383), new Point(11, 21)), Color.White);
    }
}