using Raylib_cs;
using static RobotSimulator.Constants;

namespace RobotSimulator;

public static class InputRoutines
{
    public static void GetInputWASD(Robot r)
    {
        r.Speed = 0;
        r.MovingX = 0;
        r.MovingY = 0;
        if (Raylib.IsKeyDown(KeyboardKey.LeftControl) ||
            Raylib.IsKeyDown(KeyboardKey.RightControl) ||
            Raylib.IsKeyDown(KeyboardKey.LeftAlt) ||
            Raylib.IsKeyDown(KeyboardKey.RightAlt))
        {
            return;
        }
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

    public static void GetInputArrows(Robot r)
    {
        r.Speed = 0;
        r.MovingX = 0;
        r.MovingY = 0;
        if (Raylib.IsKeyDown(KeyboardKey.LeftControl) ||
            Raylib.IsKeyDown(KeyboardKey.RightControl) ||
            Raylib.IsKeyDown(KeyboardKey.LeftAlt) ||
            Raylib.IsKeyDown(KeyboardKey.RightAlt))
        {
            return;
        }
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

    public static void GetInputKeypad(Robot r)
    {
        r.Speed = 0;
        r.MovingX = 0;
        r.MovingY = 0;
        if (Raylib.IsKeyDown(KeyboardKey.LeftControl) ||
            Raylib.IsKeyDown(KeyboardKey.RightControl) ||
            Raylib.IsKeyDown(KeyboardKey.LeftAlt) ||
            Raylib.IsKeyDown(KeyboardKey.RightAlt))
        {
            return;
        }
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

    public static void GetInputGamepad(int gamepad, Robot r)
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
        if (Math.Abs(leftX) <= DEADZONE)
        {
            leftX += Raylib.IsGamepadButtonDown(gamepad, GamepadButton.LeftFaceLeft) ? -1 : 0;
            leftX += Raylib.IsGamepadButtonDown(gamepad, GamepadButton.LeftFaceRight) ? 1 : 0;
        }
        if (Math.Abs(leftY) <= DEADZONE)
        {
            leftY += Raylib.IsGamepadButtonDown(gamepad, GamepadButton.LeftFaceUp) ? -1 : 0;
            leftY += Raylib.IsGamepadButtonDown(gamepad, GamepadButton.LeftFaceDown) ? 1 : 0;
        }
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
