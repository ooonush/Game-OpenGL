using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace GameOpenGL;

public class PolygonMesh2D : Component
{
    public int Faces;
    public Color FillColor;

    public Vector3[] Points { get; private set; } = null!;

    public PolygonMesh2D(int faces, Color fillColor)
    {
        Faces = faces;
        FillColor = fillColor;
    }

    public override void Update()
    {
        Points = SetupPoints();
    }

    public override void Render()
    {
        // GL.Begin(PrimitiveType.Polygon);
        // foreach (Vector3d point in Points)
        // {
        //     GL.Color3(FillColor);
        //     GL.Vertex3(point.X, point.Y, point.Z);
        // }
        // GL.End();
    }

    private Vector3[] SetupPoints()
    {
        double deltaAngleRad = 2 * Math.PI / Faces;
        var points = new List<Vector3>();
        Vector3d center = Transform.Position;
        
        for (var i = 1; i <= Faces; i++)
        {
            double delta = deltaAngleRad * i;
            double sizeY = Math.Cos(delta);
            
            double y = center.Y + sizeY * Transform.Scale.Y;
            double sizeX = Math.Sin(delta);
            double x = center.X + sizeX * Transform.Scale.X;
            points.Add(new Vector3( (float)(x + center.X), (float)(y + center.Y), (float)(center.Z)));
        }
        
        return points.ToArray();
    }
}