using Raylib_cs;
class SandMaterial : IMaterial
{
    public char Symbol => 'S';
    public Color Color => Raylib_cs.Color.Gold;

    public virtual void Update(int x, int y, Grid grid)
    {
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
