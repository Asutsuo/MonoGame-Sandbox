using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SandboxEngine;

namespace MineSweeper;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D tex_minesweeper;
    SpriteFont fonte;

    Rectangle spr_tela;
    Rectangle pos_tela;

    Rectangle spr_celula;
    Rectangle pos_celula;

    DebugOverlay debug;
    Random random = new Random();
    MouseState mouse;

    //Ideia da Chat GPT
    MouseState mouseAtual;
    MouseState mouseAnterior;

    int larguraTela;
    int alturaTela;
    int quantidadeBombas;
    bool partidaIniciada;

    Cell[,] tabuleiro = new Cell[16, 16];

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

        //Inicialização de variáveis

        larguraTela = GraphicsDevice.Viewport.Width;
        alturaTela = GraphicsDevice.Viewport.Height;

        spr_tela = new Rectangle(new Point(0, 0), new Point(287, 351));
        pos_tela = new Rectangle(new Point(0, 0), new Point(larguraTela, alturaTela));

        spr_celula = new Rectangle(new Point(0, 367), new Point(16, 16));
        pos_celula = new Rectangle(new Point(32, 156), new Point(32, 32));

        quantidadeBombas = 40;
        partidaIniciada = false;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        tex_minesweeper = Content.Load<Texture2D>("minesweeper");
        fonte = Content.Load<SpriteFont>("Fonte");
        debug = new DebugOverlay(Content.Load<SpriteFont>("Fonte"));
        debug.Mouse = true;

        //Lógica de Inicialização de células

        for (int linha = 0; linha < 16; linha++)
        {
            for (int coluna = 0; coluna < 16; coluna++)
            {

                tabuleiro[linha, coluna] = new Cell(tex_minesweeper, spr_celula, linha, coluna, _spriteBatch);
            }
        }

        for (int i = 0; i < quantidadeBombas; i++)
        {
            int linha = random.Next(16);
            int coluna = random.Next(16);

            tabuleiro[linha, coluna].mine = true;
        }
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        debug.MousePosition = Mouse.GetState().Position;

        mouse = Mouse.GetState();

        mouseAnterior = mouseAtual;
        mouseAtual = Mouse.GetState();

        //Update de estados das células

        for (int linha = 0; linha < 16; linha++)
        {
            for (int coluna = 0; coluna < 16; coluna++)
            {
                if (tabuleiro[linha, coluna].foiClicado(mouseAtual, mouseAnterior, mouse.Position))
                {
                    if (!partidaIniciada)
                    {
                        tabuleiro[linha, coluna].mine = false;
                        tabuleiro[linha, coluna].blank = true;

                        for (int LINHA = 0; LINHA < 16; LINHA++)
                        {
                            for (int COLUNA = 0; COLUNA < 16; COLUNA++)
                            {
                                tabuleiro[LINHA, COLUNA].checarVizinhos(LINHA, COLUNA, tabuleiro);
                            }
                        }

                        partidaIniciada = true;

                        tabuleiro[linha, coluna].expandir(tabuleiro);
                    }

                    if (partidaIniciada)
                    {
                        tabuleiro[linha, coluna].aberta = true;
                        tabuleiro[linha, coluna].expandir(tabuleiro);
                    }
                }

                tabuleiro[linha, coluna].Update();
                tabuleiro[linha, coluna].tabuleiro = tabuleiro;
            }
        }



        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        //desenhando a tela
        _spriteBatch.Draw(tex_minesweeper, pos_tela, spr_tela, Color.White);

        //desenhando células
        for (int linha = 0; linha < 16; linha++)
        {
            for (int coluna = 0; coluna < 16; coluna++)
            {
                tabuleiro[linha, coluna].Draw();
            }
        }


        //DEBUG
        debug.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
