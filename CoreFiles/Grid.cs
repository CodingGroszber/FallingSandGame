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
        cells[srcX, srcY] = ' ';
        cells[destX, destY] = currentMaterial.Symbol;
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
        int leftX = x - 1;
        int rightX = x + 1;

        if (IsValidCell(leftX, y) && cells[leftX, y] == ' ')
        {
            Move(x, y, leftX, y);
            return;
        }

        if (IsValidCell(rightX, y) && cells[rightX, y] == ' ')
        {
            Move(x, y, rightX, y);
            return;
        }
    }



    public bool IsValidCell(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }


}
