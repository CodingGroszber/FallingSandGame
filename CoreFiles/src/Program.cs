using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Numerics;

public enum MaterialType
{
    Solid,
    Liquid,
    Gas
}

public abstract class Material
{
    public string Name { get; set; }
    public Color Color { get; set; }
    public float Density { get; set; }
    public bool Flammable { get; set; }
    public float Viscosity { get; set; }
    public MaterialType Type { get; protected set; }

    public abstract void Update(int x, int y, Material[,] grid);

    public virtual void Interact(int x, int y, Material otherMaterial, Material[,] grid)
    {
        // Default interaction logic based on material types and density
        if (this.Type == MaterialType.Solid && otherMaterial.Type == MaterialType.Solid)
        {
            // Solids do not push each other out
            return;
        }
        else if (this.Type == MaterialType.Solid && otherMaterial.Type == MaterialType.Liquid ||
                 this.Type == MaterialType.Liquid && otherMaterial.Type == MaterialType.Gas ||
                 this.Type == MaterialType.Solid && otherMaterial.Type == MaterialType.Gas)
        {
            // Solid pushes out liquid, liquid pushes out gas, solid pushes out gas
            PushOutMaterial(x, y, otherMaterial, grid);
        }
        else if (this.Type == MaterialType.Liquid && otherMaterial.Type == MaterialType.Liquid ||
                 this.Type == MaterialType.Gas && otherMaterial.Type == MaterialType.Gas)
        {
            // Lower density pushes out higher density
            if (this.Density < otherMaterial.Density)
            {
                PushOutMaterial(x, y, otherMaterial, grid);
            }
        }
    }


protected void PushOutMaterial(int x, int y, Material otherMaterial, Material[,] grid)
{
    int newX = x;
    int newY = y;

    // Find a free space to push the other material to
    while (grid[newX, newY] != null && (newX != x || newY != y))
    {
        // Try to move down first
        if (newY + 1 < grid.GetLength(1) && grid[newX, newY + 1] == null)
        {
            newY++;
            break;
        }
        // If can't move down, try to move left or right
        else if (newY + 1 < grid.GetLength(1))
        {
            int direction = Raylib.GetRandomValue(0, 1) * 2 - 1; // Randomly choose left (-1) or right (1) first
            for (int i = 0; i < 2; i++)
            {
                int newNewX = newX + direction;
                if (newNewX >= 0 && newNewX < grid.GetLength(0) && grid[newNewX, newY + 1] == null)
                {
                    newX = newNewX;
                    newY++;
                    break;
                }
                direction = -direction; // Switch direction to check the other side
            }
        }
        // If no free space is found, the materials stay in place
        else
        {
            break;
        }
    }

    // If a free space is found, swap the materials
    if (grid[newX, newY] == null)
    {
        grid[newX, newY] = otherMaterial;
        grid[x, y] = this;
    }
}




}







public class InteractionManager
{
    public void HandleInteraction(int x, int y, Material[,] grid)
    {
        Material currentMaterial = grid[x, y];
        if (currentMaterial == null) return;

        // Check adjacent cells for interactions
        foreach (var (dx, dy) in new[] { (0, 1), (1, 0), (-1, 0), (0, -1) })
        {
            int newX = x + dx;
            int newY = y + dy;

            if (newX >= 0 && newY >= 0 && newX < grid.GetLength(0) && newY < grid.GetLength(1))
            {
                Material adjacentMaterial = grid[newX, newY];
                if (adjacentMaterial != null && adjacentMaterial != currentMaterial)
                {
                    // Compare densities to determine interaction
                    if (currentMaterial.Density > adjacentMaterial.Density)
                    {
                        currentMaterial.Interact(newX, newY, adjacentMaterial, grid);
                    }
                    else
                    {
                        adjacentMaterial.Interact(x, y, currentMaterial, grid);
                    }
                }
            }
        }
    }
}

public class Sand : Material
{
    public Sand()
    {
        Name = "Sand";
        Color = Color.Yellow;
        Density = 1.5f;
        Flammable = false;
        Viscosity = 2.0f; // Higher viscosity means the sand will spread less
        Type = MaterialType.Solid;
    }

    public override void Update(int x, int y, Material[,] grid)
    {
        // Check if the sand can move down
        if (y + 1 < grid.GetLength(1) && grid[x, y + 1] == null)
        {
            grid[x, y + 1] = this;
            grid[x, y] = null;
        }
        else if (y + 1 < grid.GetLength(1))
        {
            // Try to move down-left or down-right if the space is available
            bool moved = false;
            int direction = Raylib.GetRandomValue(0, 1) * 2 - 1; // Randomly choose left (-1) or right (1) first
            for (int i = 0; i < 2; i++)
            {
                int newX = x + direction;
                if (newX >= 0 && newX < grid.GetLength(0) && grid[newX, y + 1] == null)
                {
                    // Consider viscosity; lower chance to move sideways if viscosity is higher
                    if (Raylib.GetRandomValue(0, (int)Viscosity) < Viscosity / 2)
                    {
                        grid[newX, y + 1] = this;
                        grid[x, y] = null;
                        moved = true;
                        break;
                    }
                }
                direction = -direction; // Switch direction to check the other side
            }

            // If the sand couldn't move down-left or down-right, it stays in place, piling up
            if (!moved)
            {
                // Sand piles up by staying in place
            }
        }
    }
}

public class Water : Material
{
    public Water()
    {
        Name = "Water";
        Color = Color.Blue;
        Density = 1.0f;
        Flammable = false;
        Viscosity = 1.5f; // Lower viscosity allows water to spread more easily
        Type = MaterialType.Liquid;
    }

    public override void Update(int x, int y, Material[,] grid)
    {
        // Check if the water can flow down or spread sideways
        if (y + 1 < grid.GetLength(1) && grid[x, y + 1] == null)
        {
            grid[x, y + 1] = this;
            grid[x, y] = null;
        }
        else
        {
            int direction = Raylib.GetRandomValue(0, 1) * 2 - 1; // Randomly choose left (-1) or right (1) first
            for (int i = 0; i < 2; i++)
            {
                int newX = x + direction;
                if (newX >= 0 && newX < grid.GetLength(0) && grid[newX, y] == null)
                {
                    // Consider viscosity; lower chance to spread sideways if viscosity is higher
                    if (Raylib.GetRandomValue(0, (int)Viscosity) < Viscosity / 2)
                    {
                        grid[newX, y] = this;
                        grid[x, y] = null;
                        break;
                    }
                }
                direction = -direction; // Switch direction to check the other side
            }
        }
    }
}


public class GameWindow
{
    private Material[,] grid;
    private int width = 800;
    private int height = 600;
    private int cellSize = 5;
    private Dictionary<int, Type> materialTypes = new Dictionary<int, Type>
    {
        { 1, typeof(Sand) },
        { 2, typeof(Water) }
    };
    private Material selectedMaterial;
    private InteractionManager interactionManager;

    public GameWindow()
    {
        grid = new Material[width / cellSize, height / cellSize];
        selectedMaterial = new Sand(); // Default selected material
        interactionManager = new InteractionManager(); // Create InteractionManager instance
    }

    public void Run()
    {
        Raylib.InitWindow(width, height, "Falling Sand Game");
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            HandleInput();
            Update();
            Draw();
        }

        Raylib.CloseWindow();
    }

    private void HandleInput()
    {
        Vector2 mousePosition = Raylib.GetMousePosition();
        int x = (int)mousePosition.X / cellSize;
        int y = (int)mousePosition.Y / cellSize;

        // Handle mouse input for adding/removing materials
        if (Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            if (x >= 0 && y >= 0 && x < grid.GetLength(0) && y < grid.GetLength(1))
            {
                grid[x, y] = (Material)Activator.CreateInstance(selectedMaterial.GetType());
            }
        }
        else if (Raylib.IsMouseButtonDown(MouseButton.Right))
        {
            if (x >= 0 && y >= 0 && x < grid.GetLength(0) && y < grid.GetLength(1))
            {
                grid[x, y] = null;
            }
        }

        // Handle keyboard input for selecting materials
        foreach (var entry in materialTypes)
        {
            if (Raylib.IsKeyPressed((KeyboardKey)(KeyboardKey.Kp1 + entry.Key - 1)))
            {
                selectedMaterial = (Material)Activator.CreateInstance(entry.Value);
            }
        }
    }

    private void Update()
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = grid.GetLength(1) - 1; y >= 0; y--)
            {
                Material material = grid[x, y];

                // Update materials based on they own logic
                material?.Update(x, y, grid);
                // Call InteractionManager 
                if (material != null) interactionManager?.HandleInteraction(x, y, grid);
            }
        }
    }

    private void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);

        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                Material material = grid[x, y];
                if (material != null)
                {
                    Raylib.DrawRectangle(x * cellSize, y * cellSize, cellSize, cellSize, material.Color);
                }
            }
        }

        Raylib.EndDrawing();
        // Draw tooltip text
        Raylib.DrawText("Press 1 for Sand\n\nPress 2 for Water", 10, 10, 20, Color.White);
    }
}



public class Program
{
    static void Main()
    {
        new GameWindow().Run();
    }
}
