using Raylib_cs;

namespace RobotSimulator;

public class Region
{
    public Region()
    {
    }

    public Region(int id, int x, int y, int width, int height, int alliance)
    {
        ID = id;
        X = x;
        Y = y;
        Width = width;
        Height = height;
        Alliance = alliance;
    }

    public int ID { get; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Alliance { get; set; }

    public Rectangle BoundingBox
    {
        get
        {
            return new Rectangle(X, Y, Width, Height);
        }
    }
}
