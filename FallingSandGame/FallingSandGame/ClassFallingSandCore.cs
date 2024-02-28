using System;
using Raylib_cs;

class FallingSand
    {
        private int screenWidth;
        private int screenHeight;
        private int cellSize;
        private char[,] grid;

        public FallingSand(int width, int height, int size)
        {
            screenWidth = width;
            screenHeight = height;
            cellSize = size;
            grid = new char[screenWidth / cellSize, screenHeight / cellSize];
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            for (int x = 0; x < screenWidth / cellSize; x++)
            {
                for (int y = 0; y < screenHeight / cellSize; y++)
                {
                    grid[x, y] = ' ';
                }
            }
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
            if (IsValidCell(mouseX, mouseY))
            {
                grid[mouseX, mouseY] = 'S';
            }
        }

        private void RemoveMaterial()
        {
            int mouseX = Raylib.GetMouseX() / cellSize;
            int mouseY = Raylib.GetMouseY() / cellSize;
            if (IsValidCell(mouseX, mouseY))
            {
                grid[mouseX, mouseY] = ' ';
            }
        }

        private bool IsValidCell(int x, int y)
        {
            return x >= 0 && x < screenWidth / cellSize && y >= 0 && y < screenHeight / cellSize;
        }

        private void UpdatePhysics()
        {
            for (int x = 0; x < screenWidth / cellSize; x++)
            {
                for (int y = screenHeight / cellSize - 1; y >= 0; y--)
                {
                    if (grid[x, y] == 'S')
                    {
                        SimulateSandPhysics(x, y);
                    }
                }
            }
        }

        private void SimulateSandPhysics(int x, int y)
        {
            if (y < screenHeight / cellSize - 1 && grid[x, y + 1] == ' ')
            {
                grid[x, y] = ' ';
                grid[x, y + 1] = 'S';
            }
            else
            {
                RollSand(x, y);
            }
        }

        private void RollSand(int x, int y)
        {
            int rollDirection = new Random().Next(2); // 0 or 1
            int newX = x + (rollDirection == 0 ? -1 : 1);
            if (newX >= 0 && newX < screenWidth / cellSize && grid[newX, y] == ' ')
            {
                grid[x, y] = ' ';
                grid[newX, y] = 'S';
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
                    if (grid[x, y] == 'S')
                    {
                        Raylib.DrawRectangle(x * cellSize, y * cellSize, cellSize, cellSize, Raylib_cs.Color.Blue);
                    }
                }
            }
            Raylib.EndDrawing();
        }
    }
