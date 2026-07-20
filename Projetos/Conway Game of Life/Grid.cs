using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SandboxEngine;

namespace Conway_Game_of_Life
{
    public class Grid
    {
        public Timer timer;
        public double deltaTime;
        public bool[,] ProximaMatriz;
        public bool[,] Matriz;
        public int tempo = 0;
        public bool pausado;
        public double tempoTimer = 0.25;

        private List<bool[,]> historico = new();

        public void SaveState(bool[,] origem)
        {
            bool[,] copia = new bool[Matriz.GetLength(0), Matriz.GetLength(1)];

            Array.Copy(origem, copia, Matriz.Length);

            historico.Add(copia);
        }

        public void GetState(int indice)
        {
            Array.Copy(historico[indice], Matriz, Matriz.Length);
        }

        public Grid(int largura, int altura)
        {
            Matriz = new bool[largura, altura];
            ProximaMatriz = new bool[largura, altura];

            timer = new Timer(tempoTimer);
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

        public void UpVel()
        {
            if (tempoTimer >= 0.05)
            {
                tempoTimer -= 0.05;
                tempoTimer = Math.Round(tempoTimer, 2);
            }

            timer = new Timer(tempoTimer);
        }

        public void DownVel()
        {
            if (tempoTimer <= 0.95)
            {
                tempoTimer += 0.05;
                tempoTimer = Math.Round(tempoTimer, 2);
            }

            timer = new Timer(tempoTimer);
        }

        public void ToggleCell(int x, int y)
        {
            Matriz[x, y] = !Matriz[x, y];
        }

        public Point GetCoord(Point mouse)
        {
            return new Point(mouse.X / 10, mouse.Y / 10);
        }

        public void ActiveCell(MouseState atual, MouseState anterior, Point mouse)
        {
            Point coordenada;
            if (atual.LeftButton == ButtonState.Pressed && anterior.LeftButton == ButtonState.Released)
            {
                coordenada = GetCoord(mouse);

                if (coordenada.X >= 0 && coordenada.X < Matriz.GetLength(0) && coordenada.Y >= 0 && coordenada.Y < Matriz.GetLength(1))
                {
                    Matriz[coordenada.X, coordenada.Y] = true;
                }
            }
        }

        public int UpdateGrid()
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

                    if (Matriz[linha, coluna])
                    {
                        //Tá viva
                        ProximaMatriz[linha, coluna] = vizinhosVivos == 2 || vizinhosVivos == 3;
                    }
                    else
                    {
                        //Tá morta
                        ProximaMatriz[linha, coluna] = vizinhosVivos == 3;
                    }
                }
            }

            timer.Atualizar(deltaTime);

            if (!timer.Ativo && !pausado)
            {
                Array.Copy(ProximaMatriz, 0, Matriz, 0, Matriz.GetLength(0) * Matriz.GetLength(1));
                Array.Clear(ProximaMatriz);

                SaveState(Matriz);
                tempo++;

                timer.Resetar();
            }

            return tempo;

        }
    }
}
