using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DumbMouse
{
    public class Mouse
    {
        private bool[,] Matriz;
        public int posX;
        public int posY;

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        public Mouse(bool[,] Matriz)
        {
            this.Matriz = Matriz;
        }

        public void Update(Direction direction)
        {
            if (direction == Direction.Left)
            {
                if (posX > 0)
                {
                    posX -= 1;
                }

            }

            if (direction == Direction.Right)
            {
                if (posX < Matriz.GetLength(0) - 1)
                {
                    posX += 1;
                }
            }

            if (direction == Direction.Down)
            {
                if (posY > 0)
                {
                    posY -= 1;
                }
            }

            if (direction == Direction.Up)
            {
                if (posY < Matriz.GetLength(1) - 1)
                {
                    posY += 1;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D textura)
        {
            spriteBatch.Draw(textura, new Rectangle(posX * 32, posY * 32, 32, 32), new Rectangle(2, 58, 16, 16), Color.White);
        }
    }
}