using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DumbMouse
{
    public class MouseBrain
    {
        private Random random;
        private Mouse.Direction bestDirection;
        private double bestScore;

        public double rightWeightX;
        public double rightWeightY;
        public double leftWeightX;
        public double leftWeightY;
        public double upWeightX;
        public double upWeightY;
        public double downWeightX;
        public double downWeightY;

        public int dx;
        public int dy;

        public MouseBrain()
        {
            random = new Random();

            rightWeightX = random.NextDouble() * 2 - 1;
            rightWeightY = random.NextDouble() * 2 - 1;
            leftWeightX = random.NextDouble() * 2 - 1;
            leftWeightY = random.NextDouble() * 2 - 1;
            upWeightX = random.NextDouble() * 2 - 1;
            upWeightY = random.NextDouble() * 2 - 1;
            downWeightX = random.NextDouble() * 2 - 1;
            downWeightY = random.NextDouble() * 2 - 1;
        }

        public Mouse.Direction Think()
        {
            double Right;
            double Left;
            double Up;
            double Down;

            Right = dx * rightWeightX + dy * rightWeightY;
            Left = dx * leftWeightX + dy * leftWeightY;
            Up = dx * upWeightX + dy * upWeightY;
            Down = dx * downWeightX + dy * downWeightY;

            bestScore = double.MinValue;

            if (Right > bestScore)
            {
                bestScore = Right;
                bestDirection = Mouse.Direction.Right;
            }

            if (Left > bestScore)
            {
                bestScore = Left;
                bestDirection = Mouse.Direction.Left;
            }

            if (Up > bestScore)
            {
                bestScore = Up;
                bestDirection = Mouse.Direction.Up;
            }

            if (Down > bestScore)
            {
                bestScore = Down;
                bestDirection = Mouse.Direction.Down;
            }

            //Console.WriteLine($"dx: {dx}\ndy: {dy}\n\nRight: {Right}\nLeft: {Left}\nUp: {Up}\nDown: {Down}\n\nEscolha: {bestDirection}");

            return bestDirection;
        }

        public void PlayMatch(Mouse mouse, Grid board)
        {
            board.Update();

            Mouse.Direction direction = Think();

            mouse.Update(direction);
        }
    }
}