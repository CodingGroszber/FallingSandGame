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

    public void Move(int srcX, int srcY, int destX, int destY)
    {


        // Update cell symbol
        cells[srcX, srcY] = ' ';
        cells[destX, destY] = currentMaterial.Symbol;

        // Update cell material information (important for tracking material movement)
        cellMaterials[new Tuple<int, int>(srcX, srcY)] = null;
        cellMaterials[new Tuple<int, int>(destX, destY)] = currentMaterial;



    }

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
    // Prioritize flowing down first
    if (CanFall(x, y))
    {
        Move(x, y, x, y + 1);
        return;
    }

    // Check left neighboring cell (if valid and empty)
    int leftX = x - 1;
    if (IsValidCell(leftX, y) && cells[leftX, y] == ' ')
    {
        // Check if the cell below the left neighbor is empty (for diagonal flow)
        if (IsValidCell(leftX, y + 1) && cells[leftX, y + 1] == ' ')
        {
            Move(x, y, leftX, y + 1); // Move diagonally down-left
            return;
        }
        else
        {
            Move(x, y, leftX, y); // Move left
            return;
        }
    }

    // Check right neighboring cell (if valid and empty)
    int rightX = x + 1;
    if (IsValidCell(rightX, y) && cells[rightX, y] == ' ')
    {
        // Check if the cell below the right neighbor is empty (for diagonal flow)
        if (IsValidCell(rightX, y + 1) && cells[rightX, y + 1] == ' ')
        {
            Move(x, y, rightX, y + 1); // Move diagonally down-right
            return;
        }
        else
        {
            Move(x, y, rightX, y); // Move right
            return;
        }
    }

    // No suitable empty cells found, water remains at the current position
}




    public bool IsValidCell(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }


}