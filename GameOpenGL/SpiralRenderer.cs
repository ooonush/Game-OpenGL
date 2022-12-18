using GameOpenGL.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace GameOpenGL;

public class SpiralRenderer : Renderer
{
    private readonly VertexArrayObject _vao;

    public SpiralRenderer(ShaderProgram shader, Material material) : base(shader, material)
    {
        var vertices = new List<float>();
        float z = -50.0f;
        for (var angle = 0.0f; angle <= 2.0f * Math.PI * 3.0f; angle += 0.1f)
        {
            float x = 50.0f * (float)Math.Sin(angle);
            float y = 50.0f * (float)Math.Cos(angle);

            z += 0.5f;
            vertices.Add(x);
            vertices.Add(y);
            vertices.Add(z);
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