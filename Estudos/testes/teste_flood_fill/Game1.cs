using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace teste_flood_fill;

public class Game1 : Game
{
    Rectangle cell = new Rectangle(new Point(200, 150), new Point(32, 32));
    Random random = new Random();
    Texture2D pixel;
    int[,] matriz;
    int valor;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        matriz = new int[3, 3];

        for (int linha = 0; linha < 3; linha++)
        {
            for (int coluna = 0; coluna < 3; coluna++)
            {
                valor = random.Next(1, 3);

                matriz[linha, coluna] = valor;
            }
        }

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

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        for (int linha = 0; linha < 3; linha++)
        {
            for (int coluna = 0; coluna < 3; coluna++)
            {
                if (matriz[linha, coluna] == 1)
                {
                    _spriteBatch.Draw(pixel, new Rectangle(new Point(200 + (linha * 32), 150 + (coluna * 32)), new Point(32, 32)), cell, Color.Red);
                }

                if (matriz[linha, coluna] == 2)
                {
                    _spriteBatch.Draw(pixel, new Rectangle(new Point(200 + (linha * 32), 150 + (coluna * 32)), new Point(32, 32)), cell, Color.Green);
                }
            }
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
