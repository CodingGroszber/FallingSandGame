using System;
using Raylib_cs;

namespace FallingSandGame
{
    class Program
    {
        static void Main()
        {
            const int screenWidth = 800;
            const int screenHeight = 600;
            const int cellSize = 5; // Size of each cell (pixel)

            FallingSand fallingSand = new FallingSand(screenWidth, screenHeight, cellSize);
            fallingSand.Run();
        }
    }
}

