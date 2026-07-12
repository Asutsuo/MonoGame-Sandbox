using System;
using System.Data.Common;
using System.Diagnostics;
using System.Net;
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
    public int vizinhos = 0;

    private Texture2D textura;
    private Rectangle position;
    private Rectangle neighbors_position = new Rectangle(new Point(16, 351), new Point(16, 16));
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

        if (!marked && !aberta)
        {
            position = new Rectangle(new Point(0, 367), new Point(16, 16));
        }

        if (mine && aberta)
        {
            position = new Rectangle(new Point(0 + 1 * 16, 367), new Point(16, 16));
        }

        if (blank && aberta)
        {
            position = new Rectangle(new Point(0 + 2 * 16, 367), new Point(16, 16));
        }

        //Desenhando vizinhos

        if (vizinhos == 1)
        {
            neighbors_position = new Rectangle(new Point(16, 352), new Point(16, 15));
        }

        if (vizinhos == 2)
        {
            neighbors_position = new Rectangle(new Point(16 + (1 * 16), 352), new Point(16, 15));
        }

        if (vizinhos == 3)
        {
            neighbors_position = new Rectangle(new Point(16 + (2 * 16), 352), new Point(16, 15));
        }

        if (vizinhos == 4)
        {
            neighbors_position = new Rectangle(new Point(16 + (3 * 16), 352), new Point(16, 15));
        }

        if (vizinhos == 5)
        {
            neighbors_position = new Rectangle(new Point(16 + (4 * 16), 352), new Point(16, 15));
        }

        if (vizinhos == 6)
        {
            neighbors_position = new Rectangle(new Point(16 + (5 * 16), 352), new Point(16, 15));
        }

        if (vizinhos == 7)
        {
            neighbors_position = new Rectangle(new Point(16 + (6 * 16), 352), new Point(16, 15));
        }

        if (vizinhos == 8)
        {
            neighbors_position = new Rectangle(new Point(16 + (7 * 16), 352), new Point(16, 15));
        }
    }

    public bool foiClicado(MouseState atual, MouseState anterior, Point mouse)
    {
        if (atual.RightButton == ButtonState.Pressed && anterior.RightButton == ButtonState.Released && MouseSobre(mouse))
        {
            if (!aberta && !marked)
            {
                marked = true;
                return false;
            }

            else if (!aberta && marked)
            {
                marked = false;
                return false;
            }

            if (aberta)
            {
                return false;
            }
        }

        if (atual.LeftButton == ButtonState.Pressed && anterior.LeftButton == ButtonState.Released && MouseSobre(mouse) && !marked)
        {
            Console.WriteLine("Clique esquerdo");
            Console.WriteLine($"celula [{linha}, {coluna}]");
            return true;
        }
        else
        {
            return false;
        }
    }

    public void checarVizinhos(int LINHA, int COLUNA, Cell[,] tabuleiro)
    {
        //Teste de verificação de vizinhos adjacentes

        for (int deltaLinha = -1; deltaLinha <= 1; deltaLinha++)
        {
            for (int deltaColuna = -1; deltaColuna <= 1; deltaColuna++)
            {
                if (deltaLinha == 0 && deltaColuna == 0)
                {
                    continue;
                }

                int novaLinha = LINHA + deltaLinha;
                int novaColuna = COLUNA + deltaColuna;

                bool noLimite = novaLinha >= 0 && novaLinha < tabuleiro.GetLength(0) && novaColuna >= 0 && novaColuna < tabuleiro.GetLength(1);

                if (noLimite && tabuleiro[novaLinha, novaColuna].mine)
                {
                    vizinhos++;
                }
            }
        }
    }

    public void Draw()
    {
        spriteBatch.Draw(textura, area, position, Color.White);

        if (!marked && aberta && blank && vizinhos > 0)
        {
            spriteBatch.Draw(textura, area, neighbors_position, Color.White);
        }
    }
}
