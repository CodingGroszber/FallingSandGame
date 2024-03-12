using  Raylib_cs;
class SandMaterial : SolidMaterial
{
    public SandMaterial() : base('S', Color.Gold, int.MaxValue) { }

    public override void Update(int x, int y, Grid grid)
    {
        //Console.WriteLine("SandMaterial.Update()");
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
