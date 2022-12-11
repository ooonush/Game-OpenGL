// using System.Drawing;
//
// namespace GameOpenGL;
//
// public class Ellipse : IDrawable
// {
//     public readonly RectangleF Rect;
//     public readonly Brush FillBrush;
//     public readonly Pen BorderPen = new(Color.Black);
//
//     public Ellipse(RectangleF rect, Pen borderPen, Brush fillBrush)
//     {
//         Rect = rect;
//         BorderPen = borderPen;
//         FillBrush = fillBrush;
//     }
//
//     public Ellipse(Point center, float radius, Pen borderPen, Brush fillBrush)
//         : this(new RectangleF(center.X, center.Y, radius * 2, radius * 2), borderPen, fillBrush) { }
//
//     public Ellipse(RectangleF rect, Pen borderPen, Color fillColor) 
//         : this(rect, borderPen, new SolidBrush(fillColor)) { }
//
//     public Ellipse(RectangleF rect, Color borderColor, Color fillColor) 
//         : this(rect, new Pen(borderColor), new SolidBrush(fillColor)) { }
//     
//     public void Draw(Graphics graphics)
//     {
//         graphics.DrawEllipse(BorderPen, Rect);
//         graphics.FillEllipse(FillBrush, Rect);
//     }
// }