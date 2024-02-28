using System;
using System.Drawing;
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

			Raylib.InitWindow(screenWidth, screenHeight, "Falling Sand Game");

			// Create a 2D grid to store material types
			int gridWidth = screenWidth / cellSize;
			int gridHeight = screenHeight / cellSize;
			char[,] grid = new char[gridWidth, gridHeight];

			// Initialize grid with empty cells
			for (int x = 0; x < gridWidth; x++)
			{
				for (int y = 0; y < gridHeight; y++)
				{
					grid[x, y] = ' ';
				}
			}

			// Main game loop
			while (!Raylib.WindowShouldClose())
			{
				// User input
				if (Raylib.IsMouseButtonDown(MouseButton.Left))
				{
					// Add material (e.g., sand) where the mouse is clicked
					int mouseX = Raylib.GetMouseX() / cellSize;
					int mouseY = Raylib.GetMouseY() / cellSize;
					if (mouseX >= 0 && mouseX < gridWidth && mouseY >= 0 && mouseY < gridHeight)
					{
						grid[mouseX, mouseY] = 'S'; // 'S' for sand
					}
				}
				else if (Raylib.IsMouseButtonDown(MouseButton.Right))
				{
					// Remove material where the mouse is clicked
					int mouseX = Raylib.GetMouseX() / cellSize;
					int mouseY = Raylib.GetMouseY() / cellSize;
					if (mouseX >= 0 && mouseX < gridWidth && mouseY >= 0 && mouseY < gridHeight)
					{
						grid[mouseX, mouseY] = ' '; // Empty cell
					}
				}

				// Update physics (e.g., gravity and rolling)
				for (int x = 0; x < gridWidth; x++)
				{
					for (int y = gridHeight - 1; y >= 0; y--)
					{
						if (grid[x, y] == 'S')
						{
							// Sand falls down
							if (y < gridHeight - 1 && grid[x, y + 1] == ' ')
							{
								grid[x, y] = ' ';
								grid[x, y + 1] = 'S';
							}
							else
							{
								// Sand rolls left or right randomly
								int rollDirection = new Random().Next(2); // 0 or 1
								int newX = x + (rollDirection == 0 ? -1 : 1);
								if (newX >= 0 && newX < gridWidth && grid[newX, y] == ' ')
								{
									grid[x, y] = ' ';
									grid[newX, y] = 'S';
								}
							}
						}
					}
				}

				// Draw grid
				Raylib.BeginDrawing();
				Raylib.ClearBackground(Raylib_cs.Color.White);
				for (int x = 0; x < gridWidth; x++)
				{
					for (int y = 0; y < gridHeight; y++)
					{
						if (grid[x, y] == 'S')
						{
							Raylib.DrawRectangle(x * cellSize, y * cellSize, cellSize, cellSize, Raylib_cs.Color.Blue);
						}
					}
				}
				Raylib.EndDrawing();
			}

			Raylib.CloseWindow();
		}
	}
}



//using System;
//using Raylib_cs;

//namespace FallingSandGame
//{
//	class Program
//	{
//		static int screenWidth = 800;
//		static int screenHeight = 600;
//		static Color sandColor = Color.Yellow;
//		static int sandSize = 3; // Size of sand particles
//		static int sandSpeed = 2; // Speed at which sand falls

//		static int[,] grid; // 2D grid to represent pixels/materials

//		static void Main()
//		{
//			Raylib.InitWindow(screenWidth, screenHeight, "Falling Sand Game");

//			// Initialize grid (0 = empty, 1 = sand)
//			grid = new int[screenWidth / sandSize, screenHeight / sandSize];

//			while (!Raylib.WindowShouldClose())
//			{
//				// Update
//				UpdateSand();
//				HandleUserInput();

//				// Draw
//				Raylib.BeginDrawing();
//				Raylib.ClearBackground(Color.Black);
//				DrawSand();
//				Raylib.EndDrawing();
//			}

//			Raylib.CloseWindow();
//		}

//		static void UpdateSand()
//		{
//			// Simulate sand falling
//			for (int x = 0; x < grid.GetLength(0); x++)
//			{
//				for (int y = 0; y < grid.GetLength(1); y++)
//				{
//					if (grid[x, y] == 1)
//					{
//						// Check if there's an empty space below
//						if (y < grid.GetLength(1) - 1 && grid[x, y + 1] == 0)
//						{
//							// Move sand down
//							grid[x, y] = 0;
//							grid[x, y + 1] = 1;
//						}
//						else if (x > 0 && x < grid.GetLength(0) - 1)
//						{
//							// Randomly move left or right
//							if (Raylib.GetRandomValue(0, 1) == 0)
//							{
//								if (grid[x - 1, y] == 0)
//								{
//									grid[x, y] = 0;
//									grid[x - 1, y] = 1;
//								}
//							}
//							else
//							{
//								if (grid[x + 1, y] == 0)
//								{
//									grid[x, y] = 0;
//									grid[x + 1, y] = 1;
//								}
//							}
//						}
//					}
//				}
//			}
//		}

//		static void HandleUserInput()
//		{
//			// Add sand particles on left mouse click
//			if (Raylib.IsMouseButtonDown(MouseButton.Left))
//			{
//				int mouseX = Raylib.GetMouseX() / sandSize;
//				int mouseY = Raylib.GetMouseY() / sandSize;

//				if (grid[mouseX, mouseY] == 0)
//					grid[mouseX, mouseY] = 1;
//			}

//			// Erase sand particles while holding left mouse button
//			//if (Raylib.IsMouseButtonDown(MouseButton.Right))
//			{
//				int mouseX = Raylib.GetMouseX() / sandSize;
//				int mouseY = Raylib.GetMouseY() / sandSize;

//				if (grid[mouseX, mouseY] == 1)
//					grid[mouseX, mouseY] = 0;
//			}
//		}

//		static void DrawSand()
//		{
//			for (int x = 0; x < grid.GetLength(0); x++)
//			{
//				for (int y = 0; y < grid.GetLength(1); y++)
//				{
//					if (grid[x, y] == 1)
//					{
//						// Draw sand particle
//						Raylib.DrawRectangle(x * sandSize, y * sandSize, sandSize, sandSize, sandColor);
//					}
//				}
//			}
//		}
//	}
//}
