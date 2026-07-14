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

public class Clock
{
    private Texture2D textura;
    private SpriteBatch spriteBatch;
    private Timer timer = new Timer(1);
    private Animation clock_animation;

    private int unidade;
    private int dezena;
    private int centena;

    public double deltaTime;

    public Clock(Texture2D textura, SpriteBatch spriteBatch)
    {
        this.textura = textura;
        this.spriteBatch = spriteBatch;

        clock_animation = new Animation(11, 1, true);
    }

    public void Update()
    {
        //Reset do timer
        timer.Atualizar(deltaTime);

        if (!timer.Ativo)
        {
            unidade++;
            timer.Resetar();
        }

        //Configurando animações do relógio
        clock_animation.Atualizar(deltaTime);

        //Configurando estados do relógio

        if (unidade > 9)
        {
            unidade = 0;
            dezena++;
        }

        if (dezena > 9)
        {
            dezena = 0;
            centena++;
        }

        if (centena > 9)
        {
            centena = 0;
        }
    }

    public void Draw()
    {
        spriteBatch.Draw(textura, new Rectangle(478, 82, 22, 42), new Rectangle(new Point(0 + (11 * dezena), 383), new Point(11, 21)), Color.White);
        spriteBatch.Draw(textura, new Rectangle(504, 82, 22, 42), new Rectangle(new Point(0 + (11 * unidade), 383), new Point(11, 21)), Color.White);
        spriteBatch.Draw(textura, new Rectangle(452, 82, 22, 42), new Rectangle(new Point(0 + (11 * centena), 383), new Point(11, 21)), Color.White);
    }
}