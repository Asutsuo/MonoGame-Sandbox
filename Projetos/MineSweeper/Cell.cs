using System;
using System.Data.Common;
using System.Diagnostics;
using System.Net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MineSweeper;

public class Cell
{
    public bool blank = false;
    public bool mine = false;
    public bool marked = false;
    public bool aberta = false;
    public bool gameOver = false;
    public bool comBandeira = false;
    public bool checada = false;
    public int vizinhos = 0;
    public int contadorBandeiras;
    public Cell[,] tabuleiro;

    private Texture2D textura;
    private Rectangle position;
    private Rectangle neighbors_position = new Rectangle(new Point(16, 351), new Point(16, 16));
    private int linha;
    private int coluna;
    private SpriteBatch spriteBatch;
    private Rectangle area;

    public Cell(Texture2D textura, Rectangle position, int linha, int coluna, SpriteBatch spriteBatch)
    {
        this.textura = textura;
        this.position = position;
        this.linha = linha;
        this.coluna = coluna;
        this.spriteBatch = spriteBatch;

        area = new Rectangle(new Point(30 + coluna * 32, 156 + linha * 32), new Point(32, 32));
    }

    public void expandir(Cell[,] tabuleiro)
    {

        for (int deltaLinha = -1; deltaLinha <= 1; deltaLinha++)
        {
            for (int deltaColuna = -1; deltaColuna <= 1; deltaColuna++)
            {
                if (deltaLinha == 0 && deltaColuna == 0)
                {
                    continue;
                }

                int novaLinha = linha + deltaLinha;
                int novaColuna = coluna + deltaColuna;

                bool noLimite = novaLinha >= 0 && novaLinha < tabuleiro.GetLength(0) && novaColuna >= 0 && novaColuna < tabuleiro.GetLength(1);

                if (noLimite && vizinhos == 0)
                {
                    tabuleiro[novaLinha, novaColuna].aberta = true;
                }

            }
        }
    }

    public void chord(Cell[,] tabuleiro, bool partidaIniciada)
    {

        for (int deltaLinha = -1; deltaLinha <= 1; deltaLinha++)
        {
            for (int deltaColuna = -1; deltaColuna <= 1; deltaColuna++)
            {
                if (deltaLinha == 0 && deltaColuna == 0)
                {
                    continue;
                }

                int novaLinha = linha + deltaLinha;
                int novaColuna = coluna + deltaColuna;

                bool noLimite = novaLinha >= 0 && novaLinha < tabuleiro.GetLength(0) && novaColuna >= 0 && novaColuna < tabuleiro.GetLength(1);

                if (noLimite && aberta && !mine && !tabuleiro[novaLinha, novaColuna].marked)
                {
                    if (!partidaIniciada && !tabuleiro[novaLinha, novaColuna].mine)
                    {
                        tabuleiro[novaLinha, novaColuna].aberta = true;
                    }

                    if (partidaIniciada)
                    {
                        tabuleiro[novaLinha, novaColuna].aberta = true;
                    }
                }

            }
        }
    }

    public bool MouseSobre(Point mouse)
    {
        return area.Contains(mouse);
    }

    public void Update(bool gameOver)
    {
        if (mine)
        {
            blank = false;
        }

        if (!mine)
        {
            blank = true;
        }

        if (aberta)
        {
            comBandeira = false;
        }

        if (marked && !aberta && contadorBandeiras < 10)
        {
            position = new Rectangle(new Point(0 + 3 * 16, 367), new Point(16, 16));

            comBandeira = true;
        }

        if (!marked && !aberta)
        {
            position = new Rectangle(new Point(0, 367), new Point(16, 16));

            comBandeira = false;
        }

        if (mine && aberta)
        {
            position = new Rectangle(new Point(0 + 1 * 16, 367), new Point(16, 16));
            gameOver = true;
            this.gameOver = gameOver;
        }

        if (!mine && aberta)
        {
            position = new Rectangle(new Point(0 + 2 * 16, 367), new Point(16, 16));

            expandir(tabuleiro);
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
        //checando clique direito
        if (atual.RightButton == ButtonState.Pressed && anterior.RightButton == ButtonState.Released && MouseSobre(mouse))
        {
            if (!aberta && !marked && contadorBandeiras < 10)
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

        //checando clique esquerdo
        if (atual.LeftButton == ButtonState.Released && anterior.LeftButton == ButtonState.Pressed && MouseSobre(mouse) && !marked)
        {
            //Console.WriteLine($"Clicado em X: {linha}, Y: {coluna}");
            return true;
        }
        else
        {
            return false;
        }
    }

    public void checarVizinhos(int LINHA, int COLUNA, Cell[,] tabuleiro)
    {
        //Verificação de vizinhos adjacentes

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
