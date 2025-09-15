using System.Numerics;

namespace RobotSimulator;

public class Ball(int id, int x, int y, int radius, int alliance)
{
    public int ID { get; set; } = id;
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    public int Radius { get; set; } = radius;
    public int Alliance { get; set; } = alliance;

    public bool IsHidden { get; set; } = false;
    public bool IsScored { get; set; } = false;
    public DateTimeOffset TimeHidden { get; set; } = DateTimeOffset.MinValue;

    public Vector2 Center()
    {
        return new Vector2 { X = this.X, Y = this.Y };
    }
}
