using Raylib_cs;

namespace RobotSimulator
{
    public class Robot(int id, int x, int y, int width, int height, Raylib_cs.Color color)
    {
        public int ID { get; set; } = id;

        public int X { get; set; } = x;
        public int Y { get; set; } = y;

        public int Width { get; set; } = width;
        public int Height { get; set; } = height;

        public Color Color { get; set; } = color;

        public float MovingX { get; set; } = 0;
        public float MovingY { get; set; } = 0;
        public float Speed { get; set; } = 0;

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(X, Y, Width, Height);
            }
        }
    }
}
