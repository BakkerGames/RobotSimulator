using Raylib_cs;

namespace RobotSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int width = 1280;
            int height = 800;
            int rWidth = 30;
            int rHeight = 30;
            Raylib.InitWindow(width, height, "RobotSimulator");
            Raylib.SetTargetFPS(30);

            List<Robot> Robots = [];
            Robots.Add(new Robot(100, 100, rWidth, rHeight, 0, 0, Color.Blue));
            Robots.Add(new Robot(100, (height - rHeight) / 2, rWidth, rHeight, 0, 0, Color.Blue));
            Robots.Add(new Robot(100, height - 100 - rHeight, rWidth, rHeight, 0, 0, Color.Blue));
            Robots.Add(new Robot(width - 100 - rWidth, 100, rWidth, rHeight, 0, 0, Color.Red));
            Robots.Add(new Robot(width - 100 - rWidth, (height - rHeight) / 2, rWidth, rHeight, 0, 0, Color.Red));
            Robots.Add(new Robot(width - 100 - rWidth, height - 100 - rHeight, rWidth, rHeight, 0, 0, Color.Red));

            while (true)
            {
                Raylib.BeginDrawing();

                Raylib.ClearBackground(Color.Black);
                Raylib.DrawRectangle(0, 0, width, 10, Color.Yellow);
                Raylib.DrawRectangle(0, height - 10, width, 10, Color.Yellow);
                Raylib.DrawRectangle(0, 0, 10, height, Color.Yellow);
                Raylib.DrawRectangle(width - 10, 0, width, height, Color.Yellow);

                // Robots
                foreach (Robot r in Robots)
                {
                    Raylib.DrawRectangle(r.X, r.Y, r.Width, r.Height, r.Color);
                }

                Raylib.EndDrawing();

                if (Raylib.IsKeyPressed(KeyboardKey.Escape)) break;

                GetInput(Robots[0]);
            }

            Raylib.CloseWindow();
        }

        private static void GetInput(Robot r)
        {
            r.Speed = 0;
            if (Raylib.IsKeyDown(KeyboardKey.Left)
                || Raylib.IsKeyDown(KeyboardKey.Kp4)
                || Raylib.IsKeyDown(KeyboardKey.A)
                //|| (gamepad0 && Raylib.IsGamepadButtonDown(0, GamepadButton.LeftFaceLeft))
                )
            {
                r.Direction = 270;
                r.Speed = 5;
            }
            if (Raylib.IsKeyDown(KeyboardKey.Right)
                || Raylib.IsKeyDown(KeyboardKey.Kp6)
                || Raylib.IsKeyDown(KeyboardKey.D)
                //|| (gamepad0 && Raylib.IsGamepadButtonDown(0, GamepadButton.LeftFaceRight))
                )
            {
                r.Direction = 90;
                r.Speed = 5;
            }
            if (Raylib.IsKeyDown(KeyboardKey.Up)
                || Raylib.IsKeyDown(KeyboardKey.Kp8)
                || Raylib.IsKeyDown(KeyboardKey.W)
                //|| (gamepad0 && Raylib.IsGamepadButtonDown(0, GamepadButton.LeftFaceUp))
                )
            {
                r.Direction = 0;
                r.Speed = 5;
            }
            if (Raylib.IsKeyDown(KeyboardKey.Down)
                || Raylib.IsKeyDown(KeyboardKey.Kp2)
                || Raylib.IsKeyDown(KeyboardKey.S)
                || Raylib.IsKeyDown(KeyboardKey.X)
                //|| (gamepad0 && Raylib.IsGamepadButtonDown(0, GamepadButton.LeftFaceDown))
                )
            {
                r.Direction = 180;
                r.Speed = 5;
            }
            if (r.Speed > 0)
            {
                switch (r.Direction)
                {
                    case 0:
                        r.Y -= r.Speed;
                        break;
                    case 90:
                        r.X += r.Speed;
                        break;
                    case 180:
                        r.Y += r.Speed;
                        break;
                    case 270:
                        r.X -= r.Speed;
                        break;
                }
            }
        }
    }
}
