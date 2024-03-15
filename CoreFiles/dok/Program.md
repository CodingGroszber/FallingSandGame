# Program class

## Description
The `Program` class contains the entry point for the application. It initializes the screen dimensions and cell size, creates an instance of the `FallingSand` class, and runs the simulation.

## Methods
- `Main()`: The entry point of the application. It initializes the screen dimensions and cell size, creates an instance of the `FallingSand` class, and runs the simulation.

## Properties
- `screenWidth` (int): The width of the screen.
- `screenHeight` (int): The height of the screen.
- `cellSize` (int): The size of each cell in pixels.

## Usage
```csharp
class Program
{
    static void Main()
    {
        const int screenWidth = 800;
        const int screenHeight = 600;
        const int cellSize = 10; // Size of each cell (pixel)

        try
        {
            FallingSand fallingSand = new FallingSand(screenWidth, screenHeight, cellSize);
            fallingSand.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}