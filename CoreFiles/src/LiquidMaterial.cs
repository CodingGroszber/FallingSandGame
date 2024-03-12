using  Raylib_cs;

abstract class LiquidMaterial : IMaterial
{
    public char Symbol { get; protected set; }
    public Color Color { get; protected set; }

    protected LiquidMaterial(char symbol, Color color)
    {
        Symbol = symbol;
        Color = color;
    }

    public abstract void Update(int x, int y, Grid grid);
}
