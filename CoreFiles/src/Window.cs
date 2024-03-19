using Raylib_cs;
public class Window
{
        private List<IMaterial> materials;
        private IMaterial selectedMaterial;

        public Window()
        {
            materials = new List<IMaterial>();
        }

    public void Initialize(int screenWidth, int screenHeight, string windowTitle)
    {
        Raylib.InitWindow(screenWidth, screenHeight, windowTitle);
        Raylib.SetTargetFPS(60);

        // Add your window initialization logic here
    }

    public void AddMaterial(IMaterial material)
    {
        materials.Add(material);
    }

    public void RemoveMaterial(IMaterial material)
    {
        materials.Remove(material);
    }

    public void HandleInput()
    {
        // Add your input handling logic here
    }

    public void Update()
    {
        // Add your game state update logic here
    }

    public void Render()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.White);

        // Add your rendering logic here

        Raylib.EndDrawing();
    }

    public void RunGame()
    {
        while (!Raylib.WindowShouldClose())
        {
            HandleInput();
            Update();
            Render();
        }
    }
}


