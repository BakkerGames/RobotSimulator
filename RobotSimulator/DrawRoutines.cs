using Raylib_cs;
using static RobotSimulator.Constants;

namespace RobotSimulator;

internal class DrawRoutines
{
    public static void DrawGame(GameData gamedata)
    {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.Black);

        foreach (Rectangle wall in gamedata.Walls)
        {
            Raylib.DrawRectangle((int)wall.X, (int)wall.Y, (int)wall.Width, (int)wall.Height, Color.Yellow);
        }

        foreach (Region r in gamedata.Regions)
        {
            var color = r.Alliance switch
            {
                ALLIANCE_BLUE => Color.Blue,
                ALLIANCE_RED => Color.Red,
                _ => Color.White,
            };

            Raylib.DrawRectangle(r.X, r.Y, WALL_THICKNESS, r.Height, color);
            Raylib.DrawRectangle(r.X + r.Width - WALL_THICKNESS, r.Y, WALL_THICKNESS, r.Height, color);
            Raylib.DrawRectangle(r.X, r.Y, r.Width, WALL_THICKNESS, color);
            Raylib.DrawRectangle(r.X, r.Y + r.Height - WALL_THICKNESS, r.Width, WALL_THICKNESS, color);
        }

        foreach (Ball b in gamedata.Balls)
        {
            Raylib.DrawCircle(b.X, b.Y, b.Radius, Color.White);
        }

        foreach (Robot r in gamedata.Robots)
        {
            var color = r.Alliance switch
            {
                ALLIANCE_BLUE => Color.Blue,
                ALLIANCE_RED => Color.Red,
                _ => Color.White,
            };
            Raylib.DrawRectangle(r.X, r.Y, r.Width, r.Height, color);
            if (r.CarryingBall != null)
            {
                Raylib.DrawCircle(r.CarryingBall.X, r.CarryingBall.Y, r.CarryingBall.Radius, Color.White);
            }
            var vector = Raylib.MeasureTextEx(Raylib.GetFontDefault(), r.ID.ToString(), 48, 0);
            var xOfs = (int)((ROBOT_WIDTH - vector.X) / 2);
            var yOfs = (int)((ROBOT_HEIGHT - vector.Y) / 2);
            var textColor = r.CarryingBall != null ? Color.Black : Color.White;
            Raylib.DrawText(r.ID.ToString(), r.X + xOfs, r.Y + yOfs, 48, textColor);
        }

        var blueText = $"Score {gamedata.BlueScore}";
        var redText = $"Score {gamedata.RedScore}";
        var blueLen = Raylib.MeasureText(blueText, 24);
        var redLen = Raylib.MeasureText(redText, 24);
        Raylib.DrawText(blueText, WALL_THICKNESS + ((SCORE_WIDTH - blueLen) / 2), 220, 24, Color.Yellow);
        Raylib.DrawText(redText, WIDTH - WALL_THICKNESS - SCORE_WIDTH + ((SCORE_WIDTH - redLen) / 2), 220, 24, Color.Yellow);

        Raylib.EndDrawing();
    }
}
