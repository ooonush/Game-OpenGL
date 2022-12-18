using GameOpenGL.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace GameOpenGL;

public class ConusRenderer : Renderer
{
    private readonly ElementsBufferObject _elements;

    public ConusRenderer(ShaderProgram shader, Material material, uint segments = 100) : base(shader, material)
    {
        var vertices = new List<Vector3>();
        var normals = new List<Vector3>();
        
        Vector3 topCenter = Vector3.UnitY / 2;
        Vector3 circleCenter = - Vector3.UnitY / 2;
        
        vertices.Add(topCenter);
        normals.Add(topCenter.Normalized());
        vertices.Add(circleCenter);
        normals.Add(circleCenter.Normalized());
        
        for (double x = 0; x <= segments; x++)  
        {
            double theta = x / (segments) * 2 * Math.PI;

            var v = new Vector3((float)Math.Cos(theta) / 2, -0.5f, (float)Math.Sin(theta) / 2);
            vertices.Add(v);
            normals.Add(new Vector3(v.X, 0.5f, v.Z).Normalized());
        }

        var indices = new List<uint>();

        for (uint x = 2; x <= segments + 1; x++)
        {
            indices.Add(0);
            indices.Add(x);
            indices.Add(x + 1);
            
            indices.Add(1);
            indices.Add(x);
            indices.Add(x + 1);
        }

        var vert = new List<float>();
        for (var i = 0; i < vertices.Count; i++)
        {
            vert.Add(vertices[i].X);
            vert.Add(vertices[i].Y);
            vert.Add(vertices[i].Z);
            vert.Add(normals[i].X);
            vert.Add(normals[i].Y);
            vert.Add(normals[i].Z);
        }

        var vao = new VertexArrayObject(vert.ToArray());
        vao.VertexAttributePointer(0, 3, false, 6 * sizeof(float), 0);
        vao.VertexAttributePointer(1, 3, false, 6 * sizeof(float), 3 * sizeof(float));
        
        _elements = new ElementsBufferObject(vao, indices.ToArray());
    }
    
    public override void Draw(Vector3 viewPosition, Matrix4 view, Matrix4 projection)
    {
        base.Draw(viewPosition, view, projection);
        ShaderProgram.Use();
        
        _elements.BindVertexArrayAndDraw(PrimitiveType.Triangles);
    }
}