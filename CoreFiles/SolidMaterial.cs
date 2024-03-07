using  Raylib_cs;

class SolidMaterial : IMaterial
{
    public char Symbol { get; protected set; }
    public Color Color { get; protected set; }
    protected int maxPileHeight;

    public SolidMaterial(char symbol, Color color, int maxPileHeight)
    {
        Symbol = symbol;
        Color = color;
        this.maxPileHeight = maxPileHeight;
    }

    public virtual void Update(int x, int y, Grid grid)
    {
        if (grid.PileUp(x, y, maxPileHeight))
        {
            return;
        }

        if (grid.CanFall(x, y))
        {
            grid.Move(x, y, x, y + 1);
        }
        else
        {
            grid.Roll(x, y);
        }
    }
}
