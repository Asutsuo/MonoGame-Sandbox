using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MineSweeper;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D textura;
    Rectangle retangulo;
    Rectangle retanguloPosicao;

    int larguraTela;
    int alturaTela;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);

        _graphics.PreferredBackBufferWidth = 574;
        _graphics.PreferredBackBufferHeight = 702;
        _graphics.ApplyChanges();

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        larguraTela = GraphicsDevice.Viewport.Width;
        alturaTela = GraphicsDevice.Viewport.Height;

        retangulo = new Rectangle(new Point(0, 0), new Point(287, 351));
        retanguloPosicao = new Rectangle(new Point(0, 0), new Point(larguraTela, alturaTela));

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
        _spriteBatch.Draw(textura, retanguloPosicao, retangulo, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
