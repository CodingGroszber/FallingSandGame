class PorousMaterial : SandMaterial
{
    private int maxPileHeight;

    public PorousMaterial(int maxPileHeight)
    {
        this.maxPileHeight = maxPileHeight;
    }

    public override void Update(int x, int y, Grid grid)
    {
        if (grid.PileUp(x, y, maxPileHeight))
        {
            return;
        }

        base.Update(x, y, grid);
    }
}
