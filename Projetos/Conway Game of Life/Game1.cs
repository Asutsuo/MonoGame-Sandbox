using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using SandboxEngine;
using System.Runtime.InteropServices;

namespace Conway_Game_of_Life
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D pixel;
        Grid tabuleiro;
        MouseState mouseAtual;
        MouseState mouseAnterior;
        KeyboardState tecladoAtual;
        KeyboardState tecladoAnterior;
        SpriteFont fonte;
        DebugOverlay debug;

        int generation;
        int indiceHistorico;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Criando matriz para o game
            tabuleiro = new Grid(72, 72);

            // Definindo tamanho da tela
            _graphics.PreferredBackBufferWidth = 720;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            //Testando o ToggleCell
            tabuleiro.ToggleCell(20, 20);
            tabuleiro.ToggleCell(21, 20);
            tabuleiro.ToggleCell(22, 20);
            tabuleiro.ToggleCell(20, 21);
            tabuleiro.ToggleCell(21, 22);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Literalmente criando um pixel
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            // Carregando fonte
            fonte = Content.Load<SpriteFont>("Arial");

            //DEBUG
            debug = new DebugOverlay(fonte);
            debug.Mouse = true;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            mouseAnterior = mouseAtual;
            mouseAtual = Mouse.GetState();

            tecladoAnterior = tecladoAtual;
            tecladoAtual = Keyboard.GetState();

            tabuleiro.ActiveCell(mouseAtual, mouseAnterior, mouseAtual.Position);
            tabuleiro.deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

            debug.MousePosition = mouseAtual.Position;

            //Atualiza as gerações em Grid.cs e armazena o índice atual da matriz
            if (!tabuleiro.pausado)
            {
                generation = tabuleiro.UpdateGrid();
            }

            //Programando 'PAUSE'
            if (tecladoAtual.IsKeyDown(Keys.Space) && tecladoAnterior.IsKeyUp(Keys.Space))
            {
                tabuleiro.pausado = !tabuleiro.pausado;

                if (tabuleiro.pausado)
                {
                    indiceHistorico = generation;
                }
            }

            //Programando botão para diminuir FPS
            if (tecladoAtual.IsKeyDown(Keys.OemMinus) && tecladoAnterior.IsKeyUp(Keys.OemMinus))
            {
                tabuleiro.DownVel();
                Console.WriteLine(tabuleiro.tempoTimer);
            }

            //Programando botão para aumentar FPS
            if (tecladoAtual.IsKeyDown(Keys.OemPlus) && tecladoAnterior.IsKeyUp(Keys.OemPlus))
            {
                tabuleiro.UpVel();
                Console.WriteLine(tabuleiro.tempoTimer);
            }

            if (tabuleiro.pausado)
            {
                //Programando controle de estados com setas do teclado
                if (tecladoAtual.IsKeyDown(Keys.Left) && tecladoAnterior.IsKeyUp(Keys.Left))
                {

                    if (indiceHistorico > 0)
                    {
                        indiceHistorico--;
                        tabuleiro.GetState(indiceHistorico);
                    }
                }

                if (tecladoAtual.IsKeyDown(Keys.Right) && tecladoAnterior.IsKeyUp(Keys.Right))
                {
                    if (indiceHistorico < generation - 1)
                    {
                        indiceHistorico++;
                        tabuleiro.GetState(indiceHistorico);
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            tabuleiro.DrawGrid(_spriteBatch, pixel);

            _spriteBatch.DrawString(fonte, $"Tempo: {generation}", new Vector2(550, 50), Color.White);

            debug.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
