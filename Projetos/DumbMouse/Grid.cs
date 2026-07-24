using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DumbMouse
{
    public class Grid
    {
        public bool[,] Matriz;
        public int score;

        private Random random = new Random();
        private int width;
        private int height;
        private bool cheese_exists = true;

        public int mouseX;
        public int mouseY;

        int cheeseX = 1;
        int cheeseY = 1;

        public Grid(int width, int height)
        {
            this.width = width;
            this.height = height;

            Matriz = new bool[width, height];
        }

        public void SpawnCheese()
        {
            int varX;
            int varY;

            do
            {
                varX = random.Next(30);
                varY = random.Next(30);
            } while (varX == mouseX && varY == mouseY);

            cheeseX = varX;
            cheeseY = varY;
        }

        public void Update()
        {
            if (!cheese_exists)
            {
                SpawnCheese();
                cheese_exists = true;
            }

            if (cheeseX == mouseX && cheeseY == mouseY)
            {
                cheese_exists = false;
                score++;
                Console.WriteLine($"Score: {score}");
            }

        }

        public void Draw(SpriteBatch spriteBatch, Texture2D grid_texture, Texture2D cheese_texture)
        {
            for (int linha = 0; linha < Matriz.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < Matriz.GetLength(1); coluna++)
                {
                    spriteBatch.Draw(grid_texture, new Rectangle(linha * 32, coluna * 32, 32, 32), new Rectangle(0, 0, 32, 32), Color.White);
                }
            }

            spriteBatch.Draw(cheese_texture, new Rectangle(cheeseX * 32, cheeseY * 32, 32, 32), new Rectangle(0, 0, 32, 32), Color.White);

        }

    }
}