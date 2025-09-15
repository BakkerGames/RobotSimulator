using Raylib_cs;
using static RobotSimulator.Constants;
using static RobotSimulator.InitRoutines;
using static RobotSimulator.InputRoutines;
using static RobotSimulator.MoveRoutines;
using static RobotSimulator.DrawRoutines;

namespace RobotSimulator;

internal class Program
{
    public static void Main(string[] args)
    {
        GameData gamedata = new();
        Raylib.InitWindow(WIDTH, HEIGHT, "RobotSimulator");
        Raylib.SetTargetFPS(30);
        Raylib.SetExitKey(KeyboardKey.Escape);
        Raylib.SetWindowState(ConfigFlags.FullscreenMode);

        for (int i = 0; i < 6; i++)
        {
            Console.WriteLine($"Gamepad {i}: {Raylib.GetGamepadName_(i)}");
        }

        InitGame(gamedata);
        gamedata.StartTime = DateTimeOffset.Now;
        gamedata.TimeRemaining = MATCH_TIME_SECONDS;

        // game loop
        while (true)
        {
            // Update section

            if (Raylib.WindowShouldClose() || Raylib.IsKeyPressed(KeyboardKey.Escape)) break;

            if (gamedata.TimeRemaining > 0)
            {
                // Get different kinds of input for different robots
                GetInputGamepad(0, gamedata.Robots[0]);
                GetInputGamepad(1, gamedata.Robots[1]);
                GetInputWASD(gamedata.Robots[2]);
                GetInputGamepad(2, gamedata.Robots[3]);
                GetInputGamepad(2, gamedata.Robots[4]);
                GetInputArrows(gamedata.Robots[5]);

                // Move all robots
                foreach (Robot r in gamedata.Robots)
                {
                    MoveRobot(gamedata, r);
                }
                var now = DateTimeOffset.Now;
                foreach (var b in gamedata.Balls)
                {
                    if (b.IsHidden && (now - b.TimeHidden).TotalSeconds >= 2)
                    {
                        b.IsHidden = false;
                    }
                }
                gamedata.TimeRemaining = MATCH_TIME_SECONDS - (int)(now - gamedata.StartTime).TotalSeconds;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.Space))
            {
                InitGame(gamedata);
                gamedata.StartTime = DateTimeOffset.Now;
                gamedata.TimeRemaining = MATCH_TIME_SECONDS;
            }

            // Draw section
            DrawGame(gamedata);
        }

        Raylib.CloseWindow();
    }
}
