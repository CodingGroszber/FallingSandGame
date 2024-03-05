public class Grid
{
    private int width;
    private int height;
    private char[,] cells;
    private IMaterial currentMaterial;

    public Grid(int width, int height)
    {
        this.width = width;
        this.height = height;
        cells = new char[width, height];
        InitializeGrid();
    }

    public void InitializeGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cells[x, y] = ' ';
            }
        }
    }

    public void SetMaterial(IMaterial material)
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
        if (IsValidCell(newX, y) && cells[newX, y] == ' ')
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

    public bool IsValidCell(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    public char GetCell(int x, int y)
    {
        return cells[x, y];
    }

    public void SetCell(int x, int y, char value)
    {
        cells[x, y] = value;
    }
}
