using Raylib_cs;
using static RobotSimulator.Constants;

namespace RobotSimulator;

public static class InitRoutines
{
    public static void InitGame(GameData gamedata)
    {
        gamedata.Walls.Clear();
        gamedata.Robots.Clear();
        gamedata.Regions.Clear();
        gamedata.Balls.Clear();
        gamedata.LastBallID = 200;
        gamedata.BlueScore = 0;
        gamedata.RedScore = 0;

        // add all walls
        gamedata.Walls.Add(new Rectangle(0, 0, WIDTH, 10));
        gamedata.Walls.Add(new Rectangle(0, HEIGHT - 10, WIDTH, 10));
        gamedata.Walls.Add(new Rectangle(0, 0, 10, HEIGHT));
        gamedata.Walls.Add(new Rectangle(WIDTH - 10, 0, WIDTH, HEIGHT));

        // add all robots
        gamedata.Robots.Add(new Robot(1, 200, 200, ROBOT_WIDTH, ROBOT_HEIGHT, ALLIANCE_BLUE));
        gamedata.Robots.Add(new Robot(2, 200, (HEIGHT - ROBOT_HEIGHT) / 2, ROBOT_WIDTH, ROBOT_HEIGHT, ALLIANCE_BLUE));
        gamedata.Robots.Add(new Robot(3, 200, HEIGHT - 200 - ROBOT_HEIGHT, ROBOT_WIDTH, ROBOT_HEIGHT, ALLIANCE_BLUE));
        gamedata.Robots.Add(new Robot(4, WIDTH - 200 - ROBOT_WIDTH, 200, ROBOT_WIDTH, ROBOT_HEIGHT, ALLIANCE_RED));
        gamedata.Robots.Add(new Robot(5, WIDTH - 200 - ROBOT_WIDTH, (HEIGHT - ROBOT_HEIGHT) / 2, ROBOT_WIDTH, ROBOT_HEIGHT, ALLIANCE_RED));
        gamedata.Robots.Add(new Robot(6, WIDTH - 200 - ROBOT_WIDTH, HEIGHT - 200 - ROBOT_HEIGHT, ROBOT_WIDTH, ROBOT_HEIGHT, ALLIANCE_RED));

        gamedata.Regions.Add(new Region(101, WALL_THICKNESS, WALL_THICKNESS, PICKUP_WIDTH, PICKUP_HEIGHT, ALLIANCE_RED));
        gamedata.Regions.Add(new Region(102, WALL_THICKNESS, HEIGHT - WALL_THICKNESS - PICKUP_HEIGHT, PICKUP_WIDTH, PICKUP_HEIGHT, ALLIANCE_RED));
        gamedata.Regions.Add(new Region(103, WIDTH - WALL_THICKNESS - PICKUP_WIDTH, WALL_THICKNESS, PICKUP_WIDTH, PICKUP_HEIGHT, ALLIANCE_BLUE));
        gamedata.Regions.Add(new Region(104, WIDTH - WALL_THICKNESS - PICKUP_WIDTH, HEIGHT - WALL_THICKNESS - PICKUP_HEIGHT, PICKUP_WIDTH, PICKUP_HEIGHT, ALLIANCE_BLUE));

        var blueScore = new Region(105, WALL_THICKNESS, (HEIGHT - SCORE_HEIGHT) / 2, SCORE_WIDTH, SCORE_HEIGHT, ALLIANCE_BLUE);
        var redScore = new Region(106, WIDTH - WALL_THICKNESS - SCORE_WIDTH, (HEIGHT - SCORE_HEIGHT) / 2, SCORE_WIDTH, SCORE_HEIGHT, ALLIANCE_RED);
        gamedata.Regions.Add(blueScore);
        gamedata.Regions.Add(redScore);
        gamedata.BlueScoreZone = blueScore;
        gamedata.RedScoreZone = redScore;

        AddBall(gamedata, 0);
        AddBall(gamedata, 1);
        AddBall(gamedata, 2);
        AddBall(gamedata, 3);
    }

    private static void AddBall(GameData gamedata, int ballLocation)
    {
        switch (ballLocation)
        {
            case 0:
                gamedata.Balls.Add(new Ball(gamedata.LastBallID++, (PICKUP_WIDTH / 2) + WALL_THICKNESS, (PICKUP_HEIGHT / 2) + WALL_THICKNESS, BALL_RADIUS, ALLIANCE_RED));
                break;

            case 1:
                gamedata.Balls.Add(new Ball(gamedata.LastBallID++, WIDTH - (PICKUP_WIDTH / 2) - WALL_THICKNESS, (PICKUP_HEIGHT / 2) + WALL_THICKNESS, BALL_RADIUS, ALLIANCE_BLUE));
                break;

            case 2:
                gamedata.Balls.Add(new Ball(gamedata.LastBallID++, (PICKUP_WIDTH / 2) + WALL_THICKNESS, HEIGHT - (PICKUP_HEIGHT / 2) - WALL_THICKNESS, BALL_RADIUS, ALLIANCE_RED));
                break;

            case 3:
                gamedata.Balls.Add(new Ball(gamedata.LastBallID++, WIDTH - (PICKUP_WIDTH / 2) - WALL_THICKNESS, HEIGHT - (PICKUP_HEIGHT / 2) - WALL_THICKNESS, BALL_RADIUS, ALLIANCE_BLUE));
                break;
        }
    }
}
