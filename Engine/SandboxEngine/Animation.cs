using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SandboxEngine;

public class Animation
{
    private int quantidadeFrames;
    private bool loop;
    private Timer timer;

    private int frameAtual;

    public int FrameAtual
    {
        get
        {
            return frameAtual;
        }
    }

    public Animation(int quantidadeFrames, double tempoEntreFrames, bool loop)
    {
        timer = new Timer(tempoEntreFrames);

        this.quantidadeFrames = quantidadeFrames;
        this.loop = loop;
    }

    public void Atualizar(double deltaTime)
    {
        timer.Atualizar(deltaTime);

        if (!timer.Ativo)
        {
            timer.Resetar();

            atualizarFrame();
        }
    }

    public void atualizarFrame()
    {
        if (loop)
        {
            if (frameAtual < quantidadeFrames - 1)
            {
                frameAtual++;
            }

            if (frameAtual >= quantidadeFrames - 1)
            {
                resetarAnimacao();
            }
        }
        else
        {
            if (frameAtual < quantidadeFrames - 1)
            {
                frameAtual++;
            }
        }
    }

    public void resetarAnimacao()
    {
        frameAtual = 0;
    }
}
