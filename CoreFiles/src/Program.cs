public class Program
{
    public static void Main()
    {
        Window window = new Window();
        window.Initialize(800, 600, "Falling Sand Game");

        // Add your material creation and initialization logic here

        window.RunGame();
    }
}