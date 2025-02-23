using Raylib_cs;

namespace RobotSimulator
{
    internal class Program
    {
        public const int width = 1280;
        public const int height = 800;
        public const int rWidth = 30;
        public const int rHeight = 30;

        public static List<Rectangle> Walls = [];
        public static List<Robot> Robots = [];

        static void Main(string[] args)
        {
            Raylib.InitWindow(width, height, "RobotSimulator");
            Raylib.SetTargetFPS(30);
            Raylib.SetExitKey(KeyboardKey.Escape);

            // add all walls
            Walls.Add(new Rectangle(0, 0, width, 10));
            Walls.Add(new Rectangle(0, height - 10, width, 10));
            Walls.Add(new Rectangle(0, 0, 10, height));
            Walls.Add(new Rectangle(width - 10, 0, width, height));

            // add all robots
            Robots.Add(new Robot(1, 100, 100, rWidth, rHeight, 0, 0, Color.Blue));
            Robots.Add(new Robot(2, 100, (height - rHeight) / 2, rWidth, rHeight, 0, 0, Color.Blue));
            Robots.Add(new Robot(3, 100, height - 100 - rHeight, rWidth, rHeight, 0, 0, Color.Blue));
            Robots.Add(new Robot(4, width - 100 - rWidth, 100, rWidth, rHeight, 0, 0, Color.Red));
            Robots.Add(new Robot(5, width - 100 - rWidth, (height - rHeight) / 2, rWidth, rHeight, 0, 0, Color.Red));
            Robots.Add(new Robot(6, width - 100 - rWidth, height - 100 - rHeight, rWidth, rHeight, 0, 0, Color.Red));

            // game loop
            while (true)
            {
                if (Raylib.WindowShouldClose() || Raylib.IsKeyPressed(KeyboardKey.Escape)) break;

                Raylib.BeginDrawing();

                Raylib.ClearBackground(Color.Black);

                foreach (Rectangle wall in Walls)
                {
                    Raylib.DrawRectangle((int)wall.X, (int)wall.Y, (int)wall.Width, (int)wall.Height, Color.Yellow);
                }

                GetInput(Robots[0]);

                MoveRobot(Robots[0]);

                // Draw Robots
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
                switch (r.Direction % 360)
                {
                    case 0:
                        newY = r.Y - r.Speed;
                        break;
                    case 45:
                        newY = r.Y - r.Speed;
                        newX = r.X + r.Speed;
                        break;
                    case 90:
                        newX = r.X + r.Speed;
                        break;
                    case 135:
                        newX = r.X + r.Speed;
                        newY = r.Y + r.Speed;
                        break;
                    case 180:
                        newY = r.Y + r.Speed;
                        break;
                    case 225:
                        newY = r.Y + r.Speed;
                        newX = r.X - r.Speed;
                        break;
                    case 270:
                        newX = r.X - r.Speed;
                        break;
                    case 315:
                        newY = r.Y - r.Speed;
                        newX = r.X - r.Speed;
                        break;
                }
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
                if (Raylib.CheckCollisionRecs(new Rectangle(newX, newY, r.Width, r.Height), robot.BoundingRectangle))
                {
                    return;
                }
            }
            // actually move the robot
            r.X = newX;
            r.Y = newY;
        }

        private static void GetInput(Robot r)
        {
            r.Speed = 0;
            List<int> NewDirs = [];
            if (Raylib.IsKeyDown(KeyboardKey.Left)
                || Raylib.IsKeyDown(KeyboardKey.Kp4)
                || Raylib.IsKeyDown(KeyboardKey.A)
                //|| (gamepad0 && Raylib.IsGamepadButtonDown(0, GamepadButton.LeftFaceLeft))
                )
            {
                NewDirs.Add(270);
            }
            if (Raylib.IsKeyDown(KeyboardKey.Right)
                || Raylib.IsKeyDown(KeyboardKey.Kp6)
                || Raylib.IsKeyDown(KeyboardKey.D)
                //|| (gamepad0 && Raylib.IsGamepadButtonDown(0, GamepadButton.LeftFaceRight))
                )
            {
                NewDirs.Add(90);
            }
            if (Raylib.IsKeyDown(KeyboardKey.Up)
                || Raylib.IsKeyDown(KeyboardKey.Kp8)
                || Raylib.IsKeyDown(KeyboardKey.W)
                //|| (gamepad0 && Raylib.IsGamepadButtonDown(0, GamepadButton.LeftFaceUp))
                )
            {
                NewDirs.Add(0);
            }
            if (Raylib.IsKeyDown(KeyboardKey.Down)
                || Raylib.IsKeyDown(KeyboardKey.Kp2)
                || Raylib.IsKeyDown(KeyboardKey.S)
                || Raylib.IsKeyDown(KeyboardKey.X)
                //|| (gamepad0 && Raylib.IsGamepadButtonDown(0, GamepadButton.LeftFaceDown))
                )
            {
                NewDirs.Add(180);
            }
            if (Raylib.IsKeyDown(KeyboardKey.Kp1)
                || Raylib.IsKeyDown(KeyboardKey.Z))
            {
                NewDirs.Add(225);
            }
            if (Raylib.IsKeyDown(KeyboardKey.Kp3)
                || Raylib.IsKeyDown(KeyboardKey.C))
            {
                NewDirs.Add(135);
            }
            if (Raylib.IsKeyDown(KeyboardKey.Kp7)
                || Raylib.IsKeyDown(KeyboardKey.Q))
            {
                NewDirs.Add(315);
            }
            if (Raylib.IsKeyDown(KeyboardKey.Kp9)
                || Raylib.IsKeyDown(KeyboardKey.E))
            {
                NewDirs.Add(45);
            }
            // save new direction
            if (NewDirs.Count == 1)
            {
                r.Direction = NewDirs[0];
                r.Speed = 5;
            }
            else if (NewDirs.Count == 2)
            {
                if (NewDirs[0] > NewDirs[1])
                {
                    // swap so [0] is lower
                    (NewDirs[1], NewDirs[0]) = (NewDirs[0], NewDirs[1]);
                }
                switch (NewDirs[0])
                {
                    case 0:
                        if (NewDirs[1] == 90)
                        {
                            r.Direction = 45;
                        }
                        else if (NewDirs[1] == 270)
                        {
                            r.Direction = 315;
                        }
                        break;
                    case 90:
                    case 180:
                    case 270:
                        r.Direction = (NewDirs[1] + NewDirs[0]) / 2;
                        break;
                }
                r.Speed = 5;
            }
        }
    }
}
