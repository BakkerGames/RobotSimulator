using Raylib_cs;

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
        foreach (Region region in gamedata.Regions)
        {
            if (r.Alliance == region.Alliance) continue;
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
    }
}
