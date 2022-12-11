using System.Drawing;

namespace GameOpenGL
{
    public class Line
    {
        public Point Begin;
        public Point End;
        public Color Color;
        public float Width;

        public Line(Point begin, Point end, Color color, float width = 1)
        {
            Begin = begin;
            End = end;
            Color = color;
            Width = width;
        }
    }
}