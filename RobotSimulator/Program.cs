using Raylib_cs;

namespace RobotSimulator
{
    internal class Program
    {
        private const int WIDTH = 1280;
        private const int HEIGHT = 800;
        private const int ROBOT_WIDTH = 30;
        private const int ROBOT_HEIGHT = 30;
        private const float DEADZONE = 0.1f;
        private const float SPEED = 5;

        public static List<Rectangle> Walls = [];
        public static List<Robot> Robots = [];

        static void Main(string[] args)
        {
            Raylib.InitWindow(WIDTH, HEIGHT, "RobotSimulator");
            Raylib.SetTargetFPS(30);
            Raylib.SetExitKey(KeyboardKey.Escape);

            // add all walls
            Walls.Add(new Rectangle(0, 0, WIDTH, 10));
            Walls.Add(new Rectangle(0, HEIGHT - 10, WIDTH, 10));
            Walls.Add(new Rectangle(0, 0, 10, HEIGHT));
            Walls.Add(new Rectangle(WIDTH - 10, 0, WIDTH, HEIGHT));

            // add all robots
            Robots.Add(new Robot(1, 100, 100, ROBOT_WIDTH, ROBOT_HEIGHT, Color.Blue));
            Robots.Add(new Robot(2, 100, (HEIGHT - ROBOT_HEIGHT) / 2, ROBOT_WIDTH, ROBOT_HEIGHT, Color.Blue));
            Robots.Add(new Robot(3, 100, HEIGHT - 100 - ROBOT_HEIGHT, ROBOT_WIDTH, ROBOT_HEIGHT, Color.Blue));
            Robots.Add(new Robot(4, WIDTH - 100 - ROBOT_WIDTH, 100, ROBOT_WIDTH, ROBOT_HEIGHT, Color.Red));
            Robots.Add(new Robot(5, WIDTH - 100 - ROBOT_WIDTH, (HEIGHT - ROBOT_HEIGHT) / 2, ROBOT_WIDTH, ROBOT_HEIGHT, Color.Red));
            Robots.Add(new Robot(6, WIDTH - 100 - ROBOT_WIDTH, HEIGHT - 100 - ROBOT_HEIGHT, ROBOT_WIDTH, ROBOT_HEIGHT, Color.Red));

            for (int i = 0; i < 16; i++)
            {
                Console.WriteLine($"Gamepad {i}: {Raylib.GetGamepadName_(i)}");
            }

            // game loop
            while (true)
            {
                // Update section

                if (Raylib.WindowShouldClose() || Raylib.IsKeyPressed(KeyboardKey.Escape)) break;

                // Get different kinds of input for different robots
                GetInputWASD(Robots[0]);
                GetInputArrows(Robots[1]);
                GetInputKeypad(Robots[2]);
                GetInputGamepad(0, Robots[3]);
                GetInputGamepad(1, Robots[4]);
                GetInputGamepad(2, Robots[5]);

                // Move all robots
                foreach (Robot r in Robots)
                {
                    MoveRobot(r);
                }

                // Draw section

                Raylib.BeginDrawing();

                Raylib.ClearBackground(Color.Black);

                foreach (Rectangle wall in Walls)
                {
                    Raylib.DrawRectangle((int)wall.X, (int)wall.Y, (int)wall.Width, (int)wall.Height, Color.Yellow);
                }

                foreach (Robot r in Robots)
                {
                    Raylib.DrawRectangle(r.X, r.Y, r.Width, r.Height, r.Color);
                }

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        private static void MoveRobot(Robot r)
        {
            int newX = r.X;
            int newY = r.Y;
            if (r.Speed > 0)
            {
                newX += (int)(r.MovingX * r.Speed);
                newY += (int)(r.MovingY * r.Speed);
            }
            // check collisions
            foreach (Rectangle wall in Walls)
            {
                if (Raylib.CheckCollisionRecs(new Rectangle(newX, newY, r.Width, r.Height), wall))
                {
                    return;
                }
            }
            foreach (Robot robot in Robots)
            {
                if (robot.ID == r.ID)
                {
                    continue;
                }
                if (Raylib.CheckCollisionRecs(new Rectangle(newX, newY, r.Width, r.Height), robot.BoundingBox))
                {
                    return;
                }
            }
            // actually move the robot
            r.X = newX;
            r.Y = newY;
        }

        private static void GetInputWASD(Robot r)
        {
            r.Speed = 0;
            r.MovingX = 0;
            r.MovingY = 0;
            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                r.MovingY = -1;
            }
            if (Raylib.IsKeyDown(KeyboardKey.E))
            {
                r.MovingX = 1;
                r.MovingY = -1;
            }
            if (Raylib.IsKeyDown(KeyboardKey.D))
            {
                r.MovingX = 1;
            }
            if (Raylib.IsKeyDown(KeyboardKey.C))
            {
                r.MovingX = 1;
                r.MovingY = 1;
            }
            if (Raylib.IsKeyDown(KeyboardKey.S) ||
                Raylib.IsKeyDown(KeyboardKey.X))
            {
                r.MovingY = 1;
            }
            if (Raylib.IsKeyDown(KeyboardKey.Z))
            {
                r.MovingX = -1;
                r.MovingY = 1;
            }
            if (Raylib.IsKeyDown(KeyboardKey.A))
            {
                r.MovingX = -1;
            }
            if (Raylib.IsKeyDown(KeyboardKey.Q))
            {
                r.MovingX = -1;
                r.MovingY = -1;
            }
            if (r.MovingX != 0 || r.MovingY != 0)
            {
                r.Speed = SPEED;
            }
        }

        private static void GetInputArrows(Robot r)
        {
            r.Speed = 0;
            r.MovingX = 0;
            r.MovingY = 0;
            if (Raylib.IsKeyDown(KeyboardKey.Up))
            {
                r.MovingY = -1;
            }
            if (Raylib.IsKeyDown(KeyboardKey.Right))
            {
                r.MovingX = 1;
            }
            if (Raylib.IsKeyDown(KeyboardKey.Down))
            {
                r.MovingY = 1;
            }
            if (Raylib.IsKeyDown(KeyboardKey.Left))
            {
                r.MovingX = -1;
            }
            if (r.MovingX != 0 || r.MovingY != 0)
            {
                r.Speed = SPEED;
            }
        }

        private static void GetInputKeypad(Robot r)
        {
            r.Speed = 0;
            r.MovingX = 0;
            r.MovingY = 0;
            if (Raylib.IsKeyDown(KeyboardKey.Kp8))
            {
                r.MovingY = -1;
            }
            if (Raylib.IsKeyDown(KeyboardKey.Kp9))
            {
                r.MovingX = 1;
                r.MovingY = -1;
            }
            if (Raylib.IsKeyDown(KeyboardKey.Kp6))
            {
                r.MovingX = 1;
            }
            if (Raylib.IsKeyDown(KeyboardKey.Kp3))
            {
                r.MovingX = 1;
                r.MovingY = 1;
            }
            if (Raylib.IsKeyDown(KeyboardKey.Kp2))
            {
                r.MovingY = 1;
            }
            if (Raylib.IsKeyDown(KeyboardKey.Kp1))
            {
                r.MovingX = -1;
                r.MovingY = 1;
            }
            if (Raylib.IsKeyDown(KeyboardKey.Kp4))
            {
                r.MovingX = -1;
            }
            if (Raylib.IsKeyDown(KeyboardKey.Kp7))
            {
                r.MovingX = -1;
                r.MovingY = -1;
            }
            if (r.MovingX != 0 || r.MovingY != 0)
            {
                r.Speed = SPEED;
            }
        }

        private static void GetInputGamepad(int gamepad, Robot r)
        {
            if (!Raylib.IsGamepadAvailable(gamepad))
            {
                return;
            }
            r.Speed = 0;
            r.MovingX = 0;
            r.MovingY = 0;
            var leftX = Raylib.GetGamepadAxisMovement(gamepad, GamepadAxis.LeftX);
            var leftY = Raylib.GetGamepadAxisMovement(gamepad, GamepadAxis.LeftY);
            if (Math.Abs(leftX) > DEADZONE)
            {
                r.MovingX = Math.Sign(leftX);
            }
            if (Math.Abs(leftY) > DEADZONE)
            {
                r.MovingY = Math.Sign(leftY);
            }
            if (r.MovingX != 0 || r.MovingY != 0)
            {
                r.Speed = SPEED;
            }
        }
    }
}
