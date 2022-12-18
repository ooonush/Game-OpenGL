using GameOpenGL.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace GameOpenGL;

public class TorusRenderer : Renderer
{
    private readonly VertexArrayObject _vao;

    public TorusRenderer(ShaderProgram shader, Material material, float radius = 0.25f) : base(shader, material)
    {
        const int numc = 100;
        const int numt = 100;

        var vertices = new List<float>();
        
        const double TWOPI = 2 * Math.PI;
        for (var i = 0; i < numc; i++)
        {
            for (var j = 0; j <= numt; j++)
            {
                for (var k = 1; k >= 0; k--)
                {
                    double s = (i + k) % numc + 0.5;
                    double t = j % numt;

                    double x = (1 + radius * Math.Cos(s * TWOPI / numc)) * Math.Cos(t * TWOPI / numt);
                    double y = (1 + radius * Math.Cos(s * TWOPI / numc)) * Math.Sin(t * TWOPI / numt);
                    double z = radius * Math.Sin(s * TWOPI / numc);
                    vertices.Add((float)x);
                    vertices.Add((float)y);
                    vertices.Add((float)z);
                }
            }
        }
        
        _vao = new VertexArrayObject(vertices.ToArray());
        _vao.VertexAttributePointer(0, 3, false, 3 * sizeof(float), 0);
        _vao.VertexAttributePointer(1, 3, false, 3 * sizeof(float), 0);
    }

    public override void Draw(Vector3 viewPosition, Matrix4 view, Matrix4 projection)
    {
        base.Draw(viewPosition, view, projection);
        ShaderProgram.Use();
        
        _vao.BindAndDrawArrays(PrimitiveType.TriangleStrip);
    }
}