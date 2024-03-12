using  Raylib_cs;

class WaterMaterial : LiquidMaterial
{
    public WaterMaterial() : base('W', Color.Blue) { }

    public override void Update(int x, int y, Grid grid)
    {
        //Console.WriteLine("WaterMaterial.Update()");
        if (grid.CanFall(x, y))
        {
            grid.Move(x, y, x, y + 1);
        }
        else
        {
            grid.Spread(x, y);
        }
    }
}
