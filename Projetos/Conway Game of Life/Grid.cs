using System;
using System.Data.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SandboxEngine;

namespace Conway_Game_of_Life
{
    public class Grid
    {
        private Timer timer;

        public double deltaTime;
        public bool[,] ProximaMatriz;
        public bool[,] Matriz;
        public int tempo = 0;
        public bool pausado;

        public Grid(int largura, int altura)
        {
            Matriz = new bool[largura, altura];
            ProximaMatriz = new bool[largura, altura];

            timer = new Timer(.2);
        }

        public void DrawGrid(SpriteBatch spriteBatch, Texture2D pixel)
        {
            for (int largura = 0; largura < Matriz.GetLength(0); largura++)
            {
                for (int altura = 0; altura < Matriz.GetLength(1); altura++)
                {
                    if (Matriz[largura, altura])
                    {
                        spriteBatch.Draw(pixel, new Rectangle(largura * 10, altura * 10, 10, 10), Color.LimeGreen);
                    }
                    else
                    {
                        spriteBatch.Draw(pixel, new Rectangle(largura * 10, altura * 10, 10, 10), Color.Black);
                    }
                }
            }
        }

        public void ToggleCell(int x, int y)
        {
            Matriz[x, y] = !Matriz[x, y];
        }

        public Point GetCoord(Point mouse)
        {
            return new Point(mouse.X / 10, mouse.Y / 10);
        }

        public void AtivarCelula(MouseState atual, MouseState anterior, Point mouse)
        {
            Point coordenada;
            if (atual.LeftButton == ButtonState.Pressed && anterior.LeftButton == ButtonState.Released)
            {
                coordenada = GetCoord(mouse);

                if (coordenada.X >= 0 && coordenada.X <= Matriz.GetLength(0) && coordenada.Y >= 0 && coordenada.Y <= Matriz.GetLength(1))
                {
                    Matriz[coordenada.X, coordenada.Y] = true;
                }
            }
        }

        public void UpdateGrid()
        {

            //Checando vizinhos
            for (int linha = 0; linha < Matriz.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < Matriz.GetLength(1); coluna++)
                {

                    int vizinhosVivos = 0;

                    for (int deltaLinha = -1; deltaLinha <= 1; deltaLinha++)
                    {
                        for (int deltaColuna = -1; deltaColuna <= 1; deltaColuna++)
                        {
                            if (deltaLinha == 0 && deltaColuna == 0)
                            {
                                continue;
                            }

                            int novaLinha = deltaLinha + linha;
                            int novaColuna = deltaColuna + coluna;

                            bool noLimite = novaLinha >= 0 && novaLinha < Matriz.GetLength(0) && novaColuna >= 0 && novaColuna < Matriz.GetLength(1);

                            if (noLimite)
                            {
                                if (Matriz[novaLinha, novaColuna])
                                {
                                    vizinhosVivos++;
                                }
                            }
                        }
                    }

                    if (!Matriz[linha, coluna] && vizinhosVivos == 3)
                    {
                        ProximaMatriz[linha, coluna] = true;
                    }

                    if (Matriz[linha, coluna] && vizinhosVivos < 2)
                    {
                        ProximaMatriz[linha, coluna] = false;
                    }

                    if (Matriz[linha, coluna] && vizinhosVivos > 3)
                    {
                        ProximaMatriz[linha, coluna] = false;
                    }

                    if (Matriz[linha, coluna] && vizinhosVivos > 1 && vizinhosVivos <= 3)
                    {
                        ProximaMatriz[linha, coluna] = true;
                    }
                }
            }

            timer.Atualizar(deltaTime);

            if (!timer.Ativo && !pausado)
            {
                Array.Copy(ProximaMatriz, 0, Matriz, 0, Matriz.GetLength(0) * Matriz.GetLength(1));
                Console.WriteLine(tempo);
                tempo++;
                timer.Resetar();
            }

        }
    }
}
