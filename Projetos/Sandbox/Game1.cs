using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SandboxEngine;

namespace Sandbox;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    List<Rectangle> idle;

    SpriteSheet sheet;

    Texture2D textura;

    KeyboardState teclado;

    Animation idle_animation;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        idle_animation = new Animation(7, 0.5, true);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        textura = Content.Load<Texture2D>("void_monster");

        sheet = new SpriteSheet(textura);

        sheet.RegistrarSequencia("idle", new Point(16, 714), new Point(71, 82), 76, 7);

        idle = sheet.ObterSequencia("idle");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        teclado = Keyboard.GetState();

        idle_animation.Atualizar(gameTime.ElapsedGameTime.TotalSeconds);

        Console.WriteLine(idle[idle_animation.FrameAtual]);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(textura, new Vector2(300, 200), idle[idle_animation.FrameAtual], Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
