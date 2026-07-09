using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class DebugOverlay
{
    public Point MousePosition;

    //Novas funcionalidades são adicionadas aqui
    public bool Mouse = false;


    private readonly SpriteFont fonte;

    public DebugOverlay(SpriteFont fonte)
    {
        this.fonte = fonte;
    }


    public void Draw(SpriteBatch spriteBatch)
    {
        if (Mouse)
        {
            spriteBatch.DrawString(fonte, $"Mouse: {MousePosition}", new Vector2(10, 10), Color.Red);
        }
    }

}