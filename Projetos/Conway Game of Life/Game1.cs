using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using SandboxEngine;

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

        int larguraTela;
        int alturaTela;

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

            // Definindo largura e altura da tela
            larguraTela = GraphicsDevice.Viewport.Width;
            alturaTela = GraphicsDevice.Viewport.Height;

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

            // TODO: use this.Content to load your game content here
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

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

            tabuleiro.AtivarCelula(mouseAtual, mouseAnterior, mouseAtual.Position);
            tabuleiro.deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
            tabuleiro.UpdateGrid();

            if (tecladoAtual.IsKeyDown(Keys.Space) && tecladoAnterior.IsKeyUp(Keys.Space))
            {
                tabuleiro.pausado = !tabuleiro.pausado;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            tabuleiro.DrawGrid(_spriteBatch, pixel);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
