using Raylib_cs;
public interface IMaterial
{
    char Symbol { get; }
    Color Color { get; }
    void Update(int x, int y, Grid grid);
}
