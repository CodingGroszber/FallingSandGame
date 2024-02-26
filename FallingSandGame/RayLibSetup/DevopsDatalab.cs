//using System;
//using Raylib_cs;

//namespace FallingSandGameDevopsDatalab
//{
//	class Program
//	{
//		// Constants for the game window size
//		const int screenWidth = 800;
//		const int screenHeight = 600;

//		// Constants for the sand particle properties
//		const int particleSize = 4;
//		const int particleSpeed = 2;

//		// 2D array to represent the game world
//		static int[,] gameWorld;

//		static void Main(string[] args)
//		{
//			// Initialize the game window
//			Raylib.InitWindow(screenWidth, screenHeight, "Falling Sand Game");

//			// Initialize the game world
//			gameWorld = new int[screenWidth / particleSize, screenHeight / particleSize];

//			// Set up the game world with initial sand particles
//			InitializeGameWorld();

//			// Game loop
//			while (!Raylib.WindowShouldClose())
//			{
//				// Update
//				UpdateGame();

//				// Draw
//				DrawGame();
//			}

//			// Close the game window
//			Raylib.CloseWindow();
//		}

//		static void InitializeGameWorld()
//		{
//			// Set up the game world with initial sand particles
//			for (int x = 0; x < gameWorld.GetLength(0); x++)
//			{
//				for (int y = 0; y < gameWorld.GetLength(1); y++)
//				{
//					if (y == 0)
//					{
//						// Place sand particles at the top of the game world
//						gameWorld[x, y] = 1;
//					}
//					else
//					{
//						// Empty space
//						gameWorld[x, y] = 0;
//					}
//				}
//			}
//		}

//		static void UpdateGame()
//		{
//			// Update the game world
//			for (int x = 0; x < gameWorld.GetLength(0); x++)
//			{
//				for (int y = gameWorld.GetLength(1) - 1; y >= 0; y--)
//				{
//					if (gameWorld[x, y] == 1)
//					{
//						// Check if there is empty space below the sand particle
//						if (y < gameWorld.GetLength(1) - 1 && gameWorld[x, y + 1] == 0)
//						{
//							// Move the sand particle down
//							gameWorld[x, y] = 0;
//							gameWorld[x, y + 1] = 1;
//						}
//						else
//						{
//							// Check if there is empty space diagonally below the sand particle
//							if (y < gameWorld.GetLength(1) - 1 && x > 0 && gameWorld[x - 1, y + 1] == 0)
//							{
//								// Move the sand particle diagonally down-left
//								gameWorld[x, y] = 0;
//								gameWorld[x - 1, y + 1] = 1;
//							}
//							else if (y < gameWorld.GetLength(1) - 1 && x < gameWorld.GetLength(0) - 1 && gameWorld[x + 1, y + 1] == 0)
//							{
//								// Move the sand particle diagonally down-right
//								gameWorld[x, y] = 0;
//								gameWorld[x + 1, y + 1] = 1;
//							}
//						}
//					}
//				}
//			}
//		}

//		static void DrawGame()
//		{
//			// Begin drawing
//			Raylib.BeginDrawing();

//			// Clear the screen
//			Raylib.ClearBackground(Color.White);

//			// Draw the game world
//			for (int x = 0; x < gameWorld.GetLength(0); x++)
//			{
//				for (int y = 0; y < gameWorld.GetLength(1); y++)
//				{
//					if (gameWorld[x, y] == 1)
//					{
//						// Draw sand particles
//						Raylib.DrawRectangle(x * particleSize, y * particleSize, particleSize, particleSize, Color.Yellow);
//					}
//				}
//			}

//			// End drawing
//			Raylib.EndDrawing();
//		}
//	}
//}
