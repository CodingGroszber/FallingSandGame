using System;
using Raylib_cs;

class FallingSand
{
    private int screenWidth;
    private int screenHeight;
    private int cellSize;
    private Grid grid;
    private IMaterial currentMaterial;

    public FallingSand(int width, int height, int size)
    {
        screenWidth = width;
        screenHeight = height;
        cellSize = size;
        int gridWidth = screenWidth / cellSize;
        int gridHeight = screenHeight / cellSize;
        grid = new Grid(gridWidth, gridHeight);
        currentMaterial = new SandMaterial();
        grid.SetMaterial(currentMaterial);
    }

    public void Run()
    {
        Raylib.InitWindow(screenWidth, screenHeight, "Falling Sand Game");

        while (!Raylib.WindowShouldClose())
        {
            ProcessInput();
            UpdatePhysics();
            DrawGrid();
        }

        Raylib.CloseWindow();
    }

    private void ProcessInput()
    {
        if (Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            AddMaterial();
        }
        else if (Raylib.IsMouseButtonDown(MouseButton.Right))
        {
            RemoveMaterial();
        }
    }

    private void AddMaterial()
    {
        int mouseX = Raylib.GetMouseX() / cellSize;
        int mouseY = Raylib.GetMouseY() / cellSize;
        if (grid.IsValidCell(mouseX, mouseY))
        {
            grid.SetCell(mouseX, mouseY, currentMaterial.Symbol);
        }
    }

    private void RemoveMaterial()
    {
        int mouseX = Raylib.GetMouseX() / cellSize;
        int mouseY = Raylib.GetMouseY() / cellSize;
        if (grid.IsValidCell(mouseX, mouseY))
        {
            grid.SetCell(mouseX, mouseY, ' ');
        }
    }

    private void UpdatePhysics()
    {
        for (int x = 0; x < screenWidth / cellSize; x++)
        {
            for (int y = screenHeight / cellSize - 1; y >= 0; y--)
            {
                if (grid.GetCell(x, y) == currentMaterial.Symbol)
                {
                    currentMaterial.Update(x, y, grid);
                }
            }
        }
    }

    private void DrawGrid()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Raylib_cs.Color.White);
        for (int x = 0; x < screenWidth / cellSize; x++)
        {
            for (int y = 0; y < screenHeight / cellSize; y++)
            {
                if (grid.GetCell(x, y) == currentMaterial.Symbol)
                {
                    Raylib.DrawRectangle(x * cellSize, y * cellSize, cellSize, cellSize, currentMaterial.Color);
                }
            }
        }
        Raylib.EndDrawing();
    }
}
