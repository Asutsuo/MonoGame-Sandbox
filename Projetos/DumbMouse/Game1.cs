using System;
using System.Threading;
using System.Transactions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SE = SandboxEngine;

namespace DumbMouse;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D mouse_sheet;
    Texture2D cheese;
    Texture2D tile;
    Grid board;
    Mouse mouse;
    MouseBrain brain;

    bool training = true;
    int indice;

    int dx;
    int dy;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);

        _graphics.PreferredBackBufferWidth = 960;
        _graphics.PreferredBackBufferHeight = 960;

        _graphics.ApplyChanges();

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        board = new Grid(30, 30);

        mouse = new Mouse(board.Matriz);

        brain = new MouseBrain();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        mouse_sheet = Content.Load<Texture2D>("mouse_sheet");
        tile = Content.Load<Texture2D>("grid");
        cheese = Content.Load<Texture2D>("cheese");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        dx = board.cheeseX - mouse.posX;
        dy = board.cheeseY - mouse.posY;

        brain.dx = dx;
        brain.dy = dy;

        board.mouseX = mouse.posX;
        board.mouseY = mouse.posY;

        if (training)
        {
            brain.PlayMatch(mouse, board);
            indice++;

            if (indice == 500)
            {
                training = false;
                Console.WriteLine($"Resultados:\n\nPeso direita X: {brain.rightWeightX}\nPeso direita Y: {brain.rightWeightY}\nPeso esquerda X: {brain.leftWeightX}\nPeso esquerda Y: {brain.leftWeightY}\nPeso cima X: {brain.upWeightX}\nPeso cima Y: {brain.upWeightY}\nPeso baixo X: {brain.downWeightX}\nPeso baixo Y: {brain.downWeightY}\n\nScore: {board.score}");
                board.score = 0;
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        board.Draw(_spriteBatch, tile, cheese);

        mouse.Draw(_spriteBatch, mouse_sheet);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
