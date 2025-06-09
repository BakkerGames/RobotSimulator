using Raylib_cs;
using static RobotSimulator.Constants;

namespace RobotSimulator;

public static class MoveRoutines
{
    public static void MoveRobot(GameData gamedata, Robot r)
    {
        int newX = r.X;
        int newY = r.Y;
        if (r.Speed > 0)
        {
            newX += (int)(r.MovingX * r.Speed);
            newY += (int)(r.MovingY * r.Speed);
        }

        // check collisions
        foreach (Rectangle wall in gamedata.Walls)
        {
            if (Raylib.CheckCollisionRecs(new Rectangle(newX, r.Y, r.Width, r.Height), wall))
            {
                newX = r.X; // reset
            }
            if (Raylib.CheckCollisionRecs(new Rectangle(r.X, newY, r.Width, r.Height), wall))
            {
                newY = r.Y; // reset
            }
        }
        if (newX == r.X && newY == r.Y) return;
        foreach (Region region in gamedata.Regions)
        {
            if (r.Alliance == region.Alliance)
            {
                // ignore if in alliance region
                continue;
            }
            if (Raylib.CheckCollisionRecs(new Rectangle(newX, newY, r.Width, r.Height), region.BoundingBox))
            {
                newX = r.X;
                newY = r.Y;
            }
        }
        if (newX == r.X && newY == r.Y) return;
        foreach (Robot robot in gamedata.Robots)
        {
            if (robot.ID == r.ID)
            {
                continue;
            }
            if (Raylib.CheckCollisionRecs(new Rectangle(newX, r.Y, r.Width, r.Height), robot.BoundingBox))
            {
                newX = r.X; // reset
            }
            if (Raylib.CheckCollisionRecs(new Rectangle(r.X, newY, r.Width, r.Height), robot.BoundingBox))
            {
                newY = r.Y; // reset
            }
        }
        if (newX == r.X && newY == r.Y) return;
        // actually move the robot
        r.X = newX;
        r.Y = newY;
        // check for ball pickup
        if (r.CarryingBall == null)
        {
            foreach (Ball b in gamedata.Balls)
            {
                if (Raylib.CheckCollisionCircleRec(b.Center(), b.Radius / 4, r.BoundingBox))
                {
                    gamedata.Balls.Remove(b);
                    r.CarryingBall = b;
                    gamedata.Balls.Add(new Ball(gamedata.LastBallID++, b.X, b.Y, b.Radius, b.Alliance));
                    break;
                }
            }
        }
        // check for ball scoring
        if (r.CarryingBall != null)
        {
            r.CarryingBall.X = r.X + (r.Width / 2);
            r.CarryingBall.Y = r.Y + (r.Height / 2);
            if (r.Alliance == ALLIANCE_BLUE)
            {
                if (RectangleContains(gamedata.BlueScoreZone.BoundingBox, r.BoundingBox))
                {
                    r.CarryingBall = null;
                    gamedata.BlueScore++;
                }
            }
            else if (r.Alliance == ALLIANCE_RED)
            {
                if (RectangleContains(gamedata.RedScoreZone.BoundingBox, r.BoundingBox))
                {
                    r.CarryingBall = null;
                    gamedata.RedScore++;
                }
            }
        }
    }

    private static bool RectangleContains(Rectangle outer, Rectangle inner)
    {
        return outer.X < inner.X &&
            outer.X + outer.Width > inner.X + inner.Width &&
            outer.Y < inner.Y &&
            outer.Y + outer.Height > inner.Y + inner.Height;
    }
}
