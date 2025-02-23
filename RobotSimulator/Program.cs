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

                GetInputRobot0();
                GetInputRobot1();
                GetInputRobot2();

                MoveRobot(Robots[0]);
                MoveRobot(Robots[1]);
                MoveRobot(Robots[2]);

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

        private static void GetInputRobot0()
        {
            Robot r = Robots[0];
            r.Speed = 0;
            List<int> NewDirs = [];
            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                NewDirs.Add(0);
            }
            if (Raylib.IsKeyDown(KeyboardKey.E))
            {
                NewDirs.Add(45);
            }
            if (Raylib.IsKeyDown(KeyboardKey.D))
            {
                NewDirs.Add(90);
            }
            if (Raylib.IsKeyDown(KeyboardKey.C))
            {
                NewDirs.Add(135);
            }
            if (Raylib.IsKeyDown(KeyboardKey.S) ||
                Raylib.IsKeyDown(KeyboardKey.X))
            {
                NewDirs.Add(180);
            }
            if (Raylib.IsKeyDown(KeyboardKey.Z))
            {
                NewDirs.Add(225);
            }
            if (Raylib.IsKeyDown(KeyboardKey.A))
            {
                NewDirs.Add(270);
            }
            if (Raylib.IsKeyDown(KeyboardKey.Q))
            {
                NewDirs.Add(315);
            }
            (r.Direction, r.Speed) = CalcNewDirection(NewDirs);
        }

        private static void GetInputRobot1()
        {
            Robot r = Robots[1];
            r.Speed = 0;
            List<int> NewDirs = [];
            if (Raylib.IsKeyDown(KeyboardKey.Up))
            {
                NewDirs.Add(0);
            }
            if (Raylib.IsKeyDown(KeyboardKey.Right))
            {
                NewDirs.Add(90);
            }
            if (Raylib.IsKeyDown(KeyboardKey.Down))
            {
                NewDirs.Add(180);
            }
            if (Raylib.IsKeyDown(KeyboardKey.Left))
            {
                NewDirs.Add(270);
            }
            (r.Direction, r.Speed) = CalcNewDirection(NewDirs);
        }

        private static void GetInputRobot2()
        {
            Robot r = Robots[2];
            r.Speed = 0;
            List<int> NewDirs = [];
            if (Raylib.IsKeyDown(KeyboardKey.Kp8))
            {
                NewDirs.Add(0);
            }
            if (Raylib.IsKeyDown(KeyboardKey.Kp9))
            {
                NewDirs.Add(45);
            }
            if (Raylib.IsKeyDown(KeyboardKey.Kp6))
            {
                NewDirs.Add(90);
            }
            if (Raylib.IsKeyDown(KeyboardKey.Kp3))
            {
                NewDirs.Add(135);
            }
            if (Raylib.IsKeyDown(KeyboardKey.Kp2))
            {
                NewDirs.Add(180);
            }
            if (Raylib.IsKeyDown(KeyboardKey.Kp1))
            {
                NewDirs.Add(225);
            }
            if (Raylib.IsKeyDown(KeyboardKey.Kp4))
            {
                NewDirs.Add(270);
            }
            if (Raylib.IsKeyDown(KeyboardKey.Kp7))
            {
                NewDirs.Add(315);
            }
            (r.Direction, r.Speed) = CalcNewDirection(NewDirs);
        }

        private static (int direction, int speed) CalcNewDirection(List<int> NewDirs)
        {
            int direction = 0;
            int speed = 0;
            // save new direction
            if (NewDirs.Count == 1)
            {
                direction = NewDirs[0];
                speed = 5;
            }
            if (NewDirs.Count == 2)
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
                            direction= 45;
                            speed = 5;
                        }
                        else if (NewDirs[1] == 270)
                        {
                            direction = 315;
                            speed = 5;
                        }
                        break;
                    case 90:
                    case 180:
                    case 270:
                        direction = (NewDirs[1] + NewDirs[0]) / 2;
                        speed = 5;
                        break;
                }
            }
            return (direction, speed);
        }
    }
}
