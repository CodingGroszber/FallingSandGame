using System;
using System.Numerics;
using Raylib_cs;

class FallingSand
{
    private int screenWidth;
    private int screenHeight;
    private int cellSize;
    private Grid grid;
    private IMaterial? currentMaterial;
    private Vector2 sandButtonPosition = new Vector2(10, 10);
    private Vector2 waterButtonPosition = new Vector2(10, 40);
    private Vector2 buttonSize = new Vector2(80, 30);

    public FallingSand(int width, int height, int size)
    {
        screenWidth = width;
        screenHeight = height;
        cellSize = size;
        int gridWidth = screenWidth / cellSize;
        int gridHeight = screenHeight / cellSize;
        grid = new Grid(gridWidth, gridHeight);

        // Use the SandMaterial class
        //currentMaterial = new SandMaterial();
        //grid.SetMaterial(currentMaterial);
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
        CheckButtonClick();
        if (currentMaterial != null)
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
    }


private void AddMaterial()
{
    int mouseX = Raylib.GetMouseX() / cellSize;
    int mouseY = Raylib.GetMouseY() / cellSize;

    if (currentMaterial == null)
    {
        throw new NullReferenceException("Current material is null");
    }

    if (!grid.IsValidCell(mouseX, mouseY))
    {
        return;
    }

    grid.SetCell(mouseX, mouseY, currentMaterial.Symbol, currentMaterial);
}

private void RemoveMaterial()
{
    int mouseX = Raylib.GetMouseX() / cellSize;
    int mouseY = Raylib.GetMouseY() / cellSize;

    if (grid.IsValidCell(mouseX, mouseY))
    {
        grid.SetCell(mouseX, mouseY, ' ', new VoidMaterial());
    }
}


private void UpdatePhysics()
{
    for (int x = 0; x < screenWidth / cellSize; x++)
    {
        for (int y = screenHeight / cellSize - 1; y >= 0; y--)
        {
            IMaterial material = grid.GetCellMaterial(x, y);
            if (material != null && material.Symbol != ' ' && grid.GetCell(x, y) == material.Symbol)
            {
                material.Update(x, y, grid);
            }
        }
    }
}




private void CheckButtonClick()
{
    Vector2 mousePosition = new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY());

    // Check if the "Sand" button is clicked
    if (Raylib.CheckCollisionPointRec(mousePosition, new Rectangle((int)sandButtonPosition.X, (int)sandButtonPosition.Y, (int)buttonSize.X, (int)buttonSize.Y)) && Raylib.IsMouseButtonReleased(MouseButton.Left))
    {
        currentMaterial = new SandMaterial();
        grid.SetMaterial(currentMaterial);
    }

    // Check if the "Water" button is clicked
    if (Raylib.CheckCollisionPointRec(mousePosition, new Rectangle((int)waterButtonPosition.X, (int)waterButtonPosition.Y, (int)buttonSize.X, (int)buttonSize.Y)) && Raylib.IsMouseButtonReleased(MouseButton.Left))
    {
        currentMaterial = new WaterMaterial();
        grid.SetMaterial(currentMaterial);
    }

    // Initialize the grid with a default material (e.g., air) when no material is selected
    if (currentMaterial == null)
    {
        grid.SetMaterial(new VoidMaterial());
    }
}




private void DrawGrid()
{
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.White);

    for (int x = 0; x < screenWidth / cellSize; x++)
    {
        for (int y = 0; y < screenHeight / cellSize; y++)
        {
            IMaterial material = grid.GetCellMaterial(x, y);
            if (material != null)
            {
                Raylib.DrawRectangle(x * cellSize, y * cellSize, cellSize, cellSize, material.Color);
            }
        }
    }

        // Draw the "Sand" button
        Raylib.DrawRectangle((int)sandButtonPosition.X, (int)sandButtonPosition.Y, (int)buttonSize.X, (int)buttonSize.Y, Color.LightGray);
        Raylib.DrawText("Sand", (int)sandButtonPosition.X + 10, (int)sandButtonPosition.Y + 10, 20, Color.Black);

        // Draw the "Water" button
        Raylib.DrawRectangle((int)waterButtonPosition.X, (int)waterButtonPosition.Y, (int)buttonSize.X, (int)buttonSize.Y, Color.LightGray);
        Raylib.DrawText("Water", (int)waterButtonPosition.X + 10, (int)waterButtonPosition.Y + 10, 20, Color.Black);

        Raylib.EndDrawing();
    }



}
