using Raylib_cs;
class VoidMaterial : IMaterial
{
    public char Symbol => ' ';
    public Color Color => new Color(0, 0, 0, 0);

    public void Update(int x, int y, Grid grid)
    {
        //Console.WriteLine("VoidMaterial.Update()");
    }
}