using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SandboxEngine;

public class SpriteSheet
{
    private readonly Texture2D textura;

    private readonly Dictionary<string, List<Rectangle>> Sequencias = new();

    public SpriteSheet(Texture2D textura)
    {
        this.textura = textura;
    }

    public void RegistrarSequencia(
        string nome,
        Point origem,
        Point tamanho,
        int espacamentoHorizontal,
        int quantidadeFrames
    )
    {
        var frames = new List<Rectangle>();

        for(int i = 0 ; i < quantidadeFrames; i++)
        {
            int x = origem.X + (i * espacamentoHorizontal);

            Rectangle frame = new Rectangle(new Point(x, origem.Y), tamanho);

            frames.Add(frame);
        }

            Sequencias.Add(nome, frames);
    }

    public List<Rectangle> ObterSequencia(string nome)
    {
        return Sequencias[nome];
    } 
}
