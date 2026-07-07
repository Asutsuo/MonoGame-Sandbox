using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace teste_matrizes;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D textura;
    Rectangle retangulo;

    int[,] matriz = new int[20, 20];

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        retangulo = new Rectangle(0, 367, 16, 16);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        textura = Content.Load<Texture2D>("minesweeper");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        for (int linha = 0; linha < 20; linha++)
        {
            for (int coluna = 0; coluna < 20; coluna++)
            {
                _spriteBatch.Draw(textura, new Rectangle((300 + coluna * 16), (100 + linha * 16), 16, 16), retangulo, Color.White);
            }
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
