using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SandboxEngine;
public class Timer
{
    private double tempoLimite;
    private double tempoAcumulado;
    private bool ativo = true;

    public bool Ativo
    {
        get
        {
            return ativo;
        }
    }

    public Timer(double tempoLimite)
    {
        this.tempoLimite = tempoLimite;
    }
    public void Atualizar(double deltaTime)
    {
        if (!ativo)
        {
            return;
        }

        tempoAcumulado += deltaTime;

        if(tempoAcumulado >= tempoLimite)
        {
            ativo = false;
        }
    }

    public void Resetar()
    {
        tempoAcumulado = 0;
        ativo = true;
    }

    public void Parar()
    {
        ativo = false;
    }
}
