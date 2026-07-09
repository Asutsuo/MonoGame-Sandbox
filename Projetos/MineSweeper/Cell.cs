using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SandboxEngine;

namespace MineSweeper;

public class Cell
{
    public bool blank;
    public bool mine;
    public bool marked;

    private Texture2D textura;
    private Rectangle position;
    private int linha;
    private int coluna;
    private int cellX;
    private SpriteBatch spriteBatch;

    public Cell(Texture2D textura, Rectangle position, int linha, int coluna, SpriteBatch spriteBatch)
    {
        this.textura = textura;
        this.position = position;
        this.linha = linha;
        this.coluna = coluna;
        this.spriteBatch = spriteBatch;
    }

    public void Update(int indice)
    {
        if (indice >= 0 && indice <= 3)
        {
            cellX = indice;
            position = new Rectangle(new Point(0 + cellX * 16, 367), new Point(16, 16));
        }
    }

    public void Draw()
    {
        spriteBatch.Draw(textura, new Rectangle(new Point(30 + coluna * 32, 156 + linha * 32), new Point(32, 32)), position, Color.White);
    }
}
