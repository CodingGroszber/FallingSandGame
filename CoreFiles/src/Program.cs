using System;
using Raylib_cs;

    /// <summary>
    /// Program class containing the entry point of the program
    /// </summary>
    class Program
    {
        // This is the entry point of the program
        static void Main()
        {
            // These are the dimensions of the game screen
            const int screenWidth = 800;
            const int screenHeight = 600;

            // This is the size of each cell (in pixels)
            const int cellSize = 5;

            try
            {
                // Create an instance of the FallingSand class
                FallingSand fallingSand = new FallingSand(screenWidth, screenHeight, cellSize);

                // Start the game loop
                fallingSand.Run();
            }
            catch (Exception ex)
            {
                // Print the error message to the console
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }



