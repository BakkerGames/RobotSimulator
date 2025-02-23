using Raylib_cs;

namespace RobotSimulator
{
    internal class Program
    {
        public static List<Rectangle> Walls = [];
        public static List<Robot> Robots = [];

        static void Main(string[] args)
        {
            int width = 1280;
            int height = 800;
            int rWidth = 30;
            int rHeight = 30;
            Raylib.InitWindow(width, height, "RobotSimulator");
            Raylib.SetTargetFPS(30);

            Robots.Add(new Robot(100, 100, rWidth, rHeight, 0, 0, Color.Blue));
            Robots.Add(new Robot(100, (height - rHeight) / 2, rWidth, rHeight, 0, 0, Color.Blue));
            Robots.Add(new Robot(100, height - 100 - rHeight, rWidth, rHeight, 0, 0, Color.Blue));
            Robots.Add(new Robot(width - 100 - rWidth, 100, rWidth, rHeight, 0, 0, Color.Red));
            Robots.Add(new Robot(width - 100 - rWidth, (height - rHeight) / 2, rWidth, rHeight, 0, 0, Color.Red));
            Robots.Add(new Robot(width - 100 - rWidth, height - 100 - rHeight, rWidth, rHeight, 0, 0, Color.Red));

            Walls.Add(new Rectangle(0, 0, width, 10));
            Walls.Add(new Rectangle(0, height - 10, width, 10));
            Walls.Add(new Rectangle(0, 0, 10, height));
            Walls.Add(new Rectangle(width - 10, 0, width, height));

            while (true)
            {
                Raylib.BeginDrawing();

                Raylib.ClearBackground(Color.Black);
                foreach (Rectangle wall in Walls)
                {
                    Raylib.DrawRectangle((int)wall.X, (int)wall.Y, (int)wall.Width, (int)wall.Height, Color.Yellow);
                }

                // Robots
                foreach (Robot r in Robots)
                {
                    Raylib.DrawRectangle(r.X, r.Y, r.Width, r.Height, r.Color);
                }

                Raylib.EndDrawing();

                if (Raylib.IsKeyPressed(KeyboardKey.Escape)) break;

                GetInput(Robots[0]);

                MoveRobot(Robots[0]);
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
            // TODO fix collision to check both direction and proper edge of wall
            //foreach (Rectangle wall in Walls)
            //{
            //    if (Raylib.CheckCollisionRecs(r.Outline, wall))
            //    {
            //        return;
            //    }
            //}
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

            if (NewDirs.Count >= 1)
            {
                r.Direction = NewDirs[0];
                r.Speed = 5;
            }
            // TODO fix diagonals
            //else if (NewDirs.Count == 2)
            //{

            //    r.Direction = NewDirs[0] 
            //    r.Speed = 5;
            //}
        }
    }
}
