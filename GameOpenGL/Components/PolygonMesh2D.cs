using System.Drawing;
using GameOpenGL.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace GameOpenGL;

public class PolygonMesh2D : Renderer
{
    public Color FillColor;
    private readonly VertexArrayObject _vao;
    private readonly ElementsBufferObject _elements;

    public Vector3[] Points { get; private set; } = null!;

    public PolygonMesh2D(ShaderProgram shader, Material material, int faces) : base(shader, material)
    {
        double deltaAngleRad = 2 * Math.PI / faces;
        var vertices = new List<float>();
        
        vertices.Add(0);
        vertices.Add(0);
        vertices.Add(0);
        vertices.Add(0);
        vertices.Add(0);
        vertices.Add(1);
        for (var i = 0; i <= faces; i++)
        {
            double delta = deltaAngleRad * i;
            
            float y = (float)Math.Cos(delta) / 2;
            float x = (float)Math.Sin(delta) / 2;
            // vertex
            vertices.Add(x);
            vertices.Add(y);
            vertices.Add(0);
            // normal
            vertices.Add(0);
            vertices.Add(0);
            vertices.Add(1);
        }

        var indices = new List<uint>();

        for (uint i = 0; i < vertices.Count / 2; i++)
        {
            indices.Add(0);
            indices.Add(i);
            indices.Add(i + 1);
        }

        _vao = new VertexArrayObject(vertices.ToArray());
        _elements = new ElementsBufferObject(_vao, indices.ToArray());

        _vao.VertexAttributePointer(0, 3, false, 6 * sizeof(float), 0);
        _vao.VertexAttributePointer(1, 3, false, 6 * sizeof(float), 3 * sizeof(float));
    }

    public override void Draw(Vector3 viewPosition, Matrix4 view, Matrix4 projection)
    {
        base.Draw(viewPosition, view, projection);
        
        ShaderProgram.Use();
        _elements.BindVertexArrayAndDraw(PrimitiveType.Triangles);
    }
}