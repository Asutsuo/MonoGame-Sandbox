using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DumbMouse
{
    public class Trainer
    {
        MouseBrain brain;
        bool training = true;
        int indice;
        int atualMatch;
        public int matchs;
        public int bestScore;

        public Trainer(int matchs)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(matchs, 1);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(matchs, 100);

            this.matchs = matchs;

            brain = new MouseBrain();
        }

        public void PlayTurn(Mouse mouse, Grid board)
        {
            board.Update();

            Mouse.Direction direction = brain.Think();

            mouse.Update(direction);
        }

        public void EndMatch(Mouse mouse, Grid board)
        {
            if (matchs > 0)
            {
                atualMatch++;
                matchs--;

                mouse.posX = 0;
                mouse.posY = 0;

                bestScore = board.score;

                board.score = 0;

                indice = 0;

                Console.WriteLine($"Resultados:\n\nPeso direita X: {brain.weights[0]}\nPeso direita Y: {brain.weights[1]}\nPeso esquerda X: {brain.weights[2]}\nPeso esquerda Y: {brain.weights[3]}\nPeso cima X: {brain.weights[4]}\nPeso cima Y: {brain.weights[5]}\nPeso baixo X: {brain.weights[6]}\nPeso baixo Y: {brain.weights[7]}\n\nPartida: {atualMatch}\nScore: {board.score}\nMelhor Score: {bestScore}");
            }
        }

        public void StartTraining(Mouse mouse, Grid board)
        {

            if (matchs >= 1 && matchs <= 99 && training)
            {
                if (indice < 500)
                {
                    PlayTurn(mouse, board);
                    indice++;
                }
                else
                {
                    EndMatch(mouse, board);
                }
            }
        }
    }
}