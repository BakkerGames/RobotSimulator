using Raylib_cs;

namespace RobotSimulator
{
    internal class Robot(int id, int x, int y, int width, int height, int direction, int speed, Raylib_cs.Color color)
    {
        public int ID { get; set; } = id;

        public int X { get; set; } = x;
        public int Y { get; set; } = y;

        public int Width { get; set; } = width;
        public int Height { get; set; } = height;

        public int Direction { get; set; } = direction;
        public int Speed { get; set; } = speed;

        public Color Color { get; set; } = color;

        public Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle(X, Y, Width, Height);
            }
        }
    }
}
