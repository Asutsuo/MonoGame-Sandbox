using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SandboxEngine;

namespace MineSweeper;

public class Cell
{
    public bool blank = false;
    public bool mine = false;
    public bool marked = false;
    public bool aberta = false;

    private Texture2D textura;
    private Rectangle position;
    private int linha;
    private int coluna;
    private SpriteBatch spriteBatch;
    private Rectangle area;

    public enum CellSprite
    {
        Closed,
        Bomb,
        Empty,
        Flag
    }

    public Cell(Texture2D textura, Rectangle position, int linha, int coluna, SpriteBatch spriteBatch)
    {
        this.textura = textura;
        this.position = position;
        this.linha = linha;
        this.coluna = coluna;
        this.spriteBatch = spriteBatch;

        area = new Rectangle(new Point(30 + coluna * 32, 156 + linha * 32), new Point(32, 32));
    }

    public bool MouseSobre(Point mouse)
    {
        return area.Contains(mouse);
    }

    public void Update()
    {
        /*switch (estado)
        {
            case  CellSprite.Closed:
                position = new Rectangle(new Point(0 + 0 * 16, 367), new Point(16, 16));
                break;
            case CellSprite.Bomb:
                position = new Rectangle(new Point(0 + 1 * 16, 367), new Point(16, 16));
                break;
            case CellSprite.Empty:
                position = new Rectangle(new Point(0 + 2 * 16, 367), new Point(16, 16));
                break;
            case CellSprite.Flag:
                position = new Rectangle(new Point(0 + 3 * 16, 367), new Point(16, 16));
                break;
            default:
                Console.WriteLine("Sprite desconhecido");
                break;
        }*/

        if (mine)
        {
            blank = false;
        }

        if (!mine)
        {
            blank = true;
        }

        if (marked && !aberta)
        {
            position = new Rectangle(new Point(0 + 3 * 16, 367), new Point(16, 16));
        }

        if (mine && aberta)
        {
            position = new Rectangle(new Point(0 + 1 * 16, 367), new Point(16, 16));
        }

        if (blank && aberta)
        {
            position = new Rectangle(new Point(0 + 2 * 16, 367), new Point(16, 16));
        }
    }

    public void Draw()
    {
        spriteBatch.Draw(textura, area, position, Color.White);
    }

    public bool foiClicado(MouseState atual, MouseState anterior, Point mouse)
    {
        if (atual.LeftButton == ButtonState.Pressed && anterior.LeftButton == ButtonState.Released && MouseSobre(mouse))
        {
            Console.WriteLine("clicado");
            Console.WriteLine($"celula [{linha}, {coluna}]");
            return true;
        }
        else
        {
            return false;
        }
    }
}
