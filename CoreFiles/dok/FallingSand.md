# FallingSand Class Documentation

## Description
The `FallingSand` class represents the core functionality of a Falling Sand Game. It provides the user with the ability to interact with different materials, such as sand and water, and observe their physics-based behavior within a grid-based environment. The class encapsulates the game's logic, including handling user input, updating the physics of the materials, and rendering the grid and buttons on the game window.

## Properties
- `private int screenWidth`: Width of the game window.
- `private int screenHeight`: Height of the game window.
- `private int cellSize`: Size of each cell in the grid.
- `private Grid grid`: Instance of the grid where materials are placed.
- `private IMaterial? currentMaterial`: Currently selected material.
- `private Vector2 sandButtonPosition`: Position of the "Sand" button.
- `private Vector2 waterButtonPosition`: Position of the "Water" button.
- `private Vector2 buttonSize`: Size of the buttons.

## Constructor
- `public FallingSand(int width, int height, int size)`: Initializes the `FallingSand` object with the specified width, height, and cell size. It creates a grid based on the dimensions provided.

## Methods
- `public void Run()`: Initiates the game loop where input is processed, physics are updated, and the grid is drawn.
- `private void ProcessInput()`: Handles user input such as adding or removing materials.
- `private void AddMaterial()`: Adds the current material to a cell based on the mouse position.
- `private void RemoveMaterial()`: Removes material from a cell based on the mouse position.
- `private void UpdatePhysics()`: Updates the physics of the materials in the grid.
- `private void CheckButtonClick()`: Checks if the user clicks on the "Sand" or "Water" buttons and sets the current material accordingly.
- `private void DrawGrid()`: Draws the grid, materials, and buttons on the game window.

## Exceptions
- `NullReferenceException` is thrown in `AddMaterial()` if the current material is null.
