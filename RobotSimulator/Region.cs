using Raylib_cs;

namespace RobotSimulator;

public class Region(int id, int x, int y, int width, int height, int alliance)
{
    public int Id { get; } = id;
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    public int Width { get; set; } = width;
    public int Height { get; set; } = height;
    public int Alliance { get; set; } = alliance;

    public Rectangle BoundingBox
    {
        get
        {
            return new Rectangle(X, Y, Width, Height);
        }
    }
}
