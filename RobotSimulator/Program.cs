using Raylib_cs;
using static RobotSimulator.Constants;
using static RobotSimulator.InputRoutines;
using static RobotSimulator.MoveRoutines;

namespace RobotSimulator
{
    internal class Program
    {

        //public static List<Rectangle> Walls = [];
        //public static List<Robot> Robots = [];
        public static readonly GameData gamedata = new();

        static void Main(string[] args)
        {
            Raylib.InitWindow(WIDTH, HEIGHT, "RobotSimulator");
            Raylib.SetTargetFPS(30);
            Raylib.SetExitKey(KeyboardKey.Escape);

            // add all walls
            gamedata.Walls.Add(new Rectangle(0, 0, WIDTH, 10));
            gamedata.Walls.Add(new Rectangle(0, HEIGHT - 10, WIDTH, 10));
            gamedata.Walls.Add(new Rectangle(0, 0, 10, HEIGHT));
            gamedata.Walls.Add(new Rectangle(WIDTH - 10, 0, WIDTH, HEIGHT));

            // add all robots
            gamedata.Robots.Add(new Robot(1, 100, 100, ROBOT_WIDTH, ROBOT_HEIGHT, Color.Blue));
            gamedata.Robots.Add(new Robot(2, 100, (HEIGHT - ROBOT_HEIGHT) / 2, ROBOT_WIDTH, ROBOT_HEIGHT, Color.Blue));
            gamedata.Robots.Add(new Robot(3, 100, HEIGHT - 100 - ROBOT_HEIGHT, ROBOT_WIDTH, ROBOT_HEIGHT, Color.Blue));
            gamedata.Robots.Add(new Robot(4, WIDTH - 100 - ROBOT_WIDTH, 100, ROBOT_WIDTH, ROBOT_HEIGHT, Color.Red));
            gamedata.Robots.Add(new Robot(5, WIDTH - 100 - ROBOT_WIDTH, (HEIGHT - ROBOT_HEIGHT) / 2, ROBOT_WIDTH, ROBOT_HEIGHT, Color.Red));
            gamedata.Robots.Add(new Robot(6, WIDTH - 100 - ROBOT_WIDTH, HEIGHT - 100 - ROBOT_HEIGHT, ROBOT_WIDTH, ROBOT_HEIGHT, Color.Red));

            for (int i = 0; i < 12; i++)
            {
                Console.WriteLine($"Gamepad {i}: {Raylib.GetGamepadName_(i)}");
            }

            // game loop
            while (true)
            {
                // Update section

                if (Raylib.WindowShouldClose() || Raylib.IsKeyPressed(KeyboardKey.Escape)) break;

                // Get different kinds of input for different robots
                GetInputWASD(gamedata.Robots[0]);
                GetInputArrows(gamedata.Robots[1]);
                GetInputKeypad(gamedata.Robots[2]);
                GetInputGamepad(0, gamedata.Robots[3]);
                GetInputGamepad(1, gamedata.Robots[4]);
                GetInputGamepad(2, gamedata.Robots[5]);

                // Move all robots
                foreach (Robot r in gamedata.Robots)
                {
                    MoveRobot(gamedata, r);
                }

                // Draw section

                Raylib.BeginDrawing();

                Raylib.ClearBackground(Color.Black);

                foreach (Rectangle wall in gamedata.Walls)
                {
                    Raylib.DrawRectangle((int)wall.X, (int)wall.Y, (int)wall.Width, (int)wall.Height, Color.Yellow);
                }

                foreach (Robot r in gamedata.Robots)
                {
                    Raylib.DrawRectangle(r.X, r.Y, r.Width, r.Height, r.Color);
                    var vector = Raylib.MeasureTextEx(Raylib.GetFontDefault(), r.ID.ToString(), 48, 0);
                    var xOfs = (int)((ROBOT_WIDTH - vector.X) / 2);
                    var yOfs = (int)((ROBOT_HEIGHT - vector.Y) / 2);
                    Raylib.DrawText(r.ID.ToString(), r.X + xOfs, r.Y + yOfs, 48, Color.White);
                }

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}
