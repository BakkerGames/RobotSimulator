namespace RobotSimulator;

public class Ball(int id, int x, int y, int radius, int alliance)
{
    public int ID { get; set; } = id;
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    public int Radius { get; set; } = radius;
    public int Alliance { get; set; } = alliance;
}
