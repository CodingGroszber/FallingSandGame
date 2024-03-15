public class Grid
{
    private int width;
    private int height;
    private char[,] cells;
    private IMaterial? currentMaterial;
    private Dictionary<Tuple<int, int>, IMaterial> cellMaterials;

    public Grid(int width, int height)
    {
        this.width = width;
        this.height = height;
        cells = new char[width, height];
        cellMaterials = new Dictionary<Tuple<int, int>, IMaterial>();
        InitializeGrid();
    }

    public void InitializeGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cells[x, y] = ' ';
                cellMaterials[new Tuple<int, int>(x, y)] = new VoidMaterial();
            }
        }
    }

    public char GetCell(int x, int y)
    {
        return cells[x, y];
    }

    public IMaterial GetCellMaterial(int x, int y)
    {
        return cellMaterials[new Tuple<int, int>(x, y)];
    }

    public void SetCell(int x, int y, char symbol, IMaterial material)
    {
        cells[x, y] = symbol;
        cellMaterials[new Tuple<int, int>(x, y)] = material;
    }

    public void SetMaterial(IMaterial? material)
    {
        currentMaterial = material;
    }

    public bool CanFall(int x, int y)
    {
        return y < height - 1 && cells[x, y + 1] == ' ';
    }

    public void Move(int srcCol, int srcRow, int destCol, int destRow)
    {
        ValidateCoordinates(srcCol, srcRow, destCol, destRow);

        cells[srcCol, srcRow] = ' ';
        cells[destCol, destRow] = currentMaterial!.Symbol;

        cellMaterials[CoordToTuple(srcCol, srcRow)] = null;
        cellMaterials[CoordToTuple(destCol, destRow)] = currentMaterial;
    }

    private void ValidateCoordinates(int srcCol, int srcRow, int destCol, int destRow)
    {
        if (!IsValidCell(srcCol, srcRow))
            throw new ArgumentException($"Invalid src coordinates: {srcCol}, {srcRow}");

        if (!IsValidCell(destCol, destRow))
            throw new ArgumentException($"Invalid dest coordinates: {destCol}, {destRow}");

        if (currentMaterial == null)
            throw new NullReferenceException("Current material is null");
    }

    private Tuple<int, int> CoordToTuple(int col, int row) => new Tuple<int, int>(col, row);


    public void Roll(int x, int y)
    {
        int rollDirection = new Random().Next(2); // 0 or 1
        int newX = x + (rollDirection == 0 ? -1 : 1);
        if (IsValidCell(newX, y) &&
         cells[newX, y] == ' ' &&
         CanFall(newX, y))
        {
            Move(x, y, newX, y);
        }
    }

    public void Roll(int x, int y, bool isLiquid)
    {
        int rollDirection = new Random().Next(2); // 0 or 1
        int newX = x + (rollDirection == 0 ? -1 : 1);
        if (IsValidCell(newX, y) &&
         cells[newX, y] == ' ' &&
         isLiquid)
        {
            Move(x, y, newX, y);
        }
    }

    public bool PileUp(int x, int y, int maxPileHeight)
    {
        if (y >= maxPileHeight && CanFall(x, y))
        {
            Move(x, y, x, y + 1);
            return true;
        }
        return false;
    }

public void Spread(int x, int y)
{
    // Check left neighboring cell (if valid and empty)
    int leftX = x - 1;
    if (IsValidCell(leftX, y) && cells[leftX, y] == ' ')
    {
        Move(x, y, leftX, y); // Move left
        return;
    }

    // Check right neighboring cell (if valid and empty)
    int rightX = x + 1;
    if (IsValidCell(rightX, y) && cells[rightX, y] == ' ')
    {
        Move(x, y, rightX, y); // Move right
        return;
    }

    // Check diagonal movement only if horizontal movement is not possible
    int leftBelowY = y + 1;
    int leftBelowX = x - 1;
    int rightBelowY = y + 1;
    int rightBelowX = x + 1;

    if (IsValidCell(leftBelowX, leftBelowY) && cells[leftBelowX, leftBelowY] == ' ')
    {
        Move(x, y, leftBelowX, leftBelowY); // Move diagonally down-left
        return;
    }
    else if (IsValidCell(rightBelowX, rightBelowY) && cells[rightBelowX, rightBelowY] == ' ')
    {
        Move(x, y, rightBelowX, rightBelowY); // Move diagonally down-right
        return;
    }

    // No suitable empty cells found, water remains at the current position
}




    public bool IsValidCell(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }


}
