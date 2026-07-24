using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DumbMouse
{
    public class MouseBrain
    {
        private Random random;

        public MouseBrain()
        {
            random = new Random();
        }

        public Mouse.Direction Think()
        {
            int value = random.Next(4);
            return (Mouse.Direction)value;
        }
    }
}