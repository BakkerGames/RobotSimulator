using Raylib_cs;

namespace RobotSimulator;

public class GameData
{
    public DateTimeOffset StartTime { get; set; } = DateTimeOffset.MinValue;
    public int TimeRemaining { get; set; } = 0;

    public List<Rectangle> Walls = [];
    public List<Robot> Robots = [];
    public List<Region> Regions = [];
    public List<Ball> Balls = [];

    public int LastBallID { get; set; } = 200;
    public int BlueScore { get; set; } = 0;
    public int RedScore { get; set; } = 0;

    public Region BlueScoreZone { get; set; } = new();
    public Region RedScoreZone { get; set; } = new();
}
