# Grid Class Documentation

## Description
The `Grid` class represents a grid where materials can be placed in a Falling Sand Game. It provides methods for initializing the grid, getting and setting cell contents and materials, moving materials within the grid, checking if a material can fall, rolling, piling up, spreading, and validating cell coordinates.

## Properties
- `private int width`: Width of the grid.
- `private int height`: Height of the grid.
- `private char[,] cells`: Array representing the contents of each cell in the grid.
- `private IMaterial? currentMaterial`: Currently selected material.
- `private Dictionary<Tuple<int, int>, IMaterial> cellMaterials`: Dictionary mapping cell coordinates to materials.

## Constructor
- `public Grid(int width, int height)`: Initializes the `Grid` object with the specified width and height. It creates a grid with empty cells and default materials.

## Methods
- `public void InitializeGrid()`: Initializes the grid with empty cells and default materials.
- `public char GetCell(int x, int y)`: Returns the content of the cell at the specified coordinates.
- `public IMaterial GetCellMaterial(int x, int y)`: Returns the material of the cell at the specified coordinates.
- `public void SetCell(int x, int y, char symbol, IMaterial material)`: Sets the content and material of the cell at the specified coordinates.
- `public void SetMaterial(IMaterial? material)`: Sets the current material.
- `public bool CanFall(int x, int y)`: Checks if the material at the specified cell can fall.
- `public void Move(int srcCol, int srcRow, int destCol, int destRow)`: Moves a material from one cell to another.
- `public void Roll(int x, int y)`: Simulates rolling behavior of a material.
- `public bool PileUp(int x, int y, int maxPileHeight)`: Simulates piling up behavior of a material.
- `public void Spread(int x, int y)`: Simulates spreading behavior of a material.
- `public bool IsValidCell(int x, int y)`: Checks if the cell coordinates are valid.

## Exceptions
- `ArgumentException` is thrown in `ValidateCoordinates()` if the source or destination coordinates are invalid.
- `NullReferenceException` is thrown in `ValidateCoordinates()` if the current material is null.
