using GameOpenGL.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace GameOpenGL;

public class CylinderRenderer : Renderer
{
    private readonly ElementsBufferObject _elements;

    public CylinderRenderer(ShaderProgram shader, Material material, uint segments = 100) : base(shader, material)
    {
        segments++;
        var vertices = new List<Vector3>();
        var normals = new List<Vector3>();
        for (var i = 0; i < 2; i++)
        {
            float y = 0.5f - i;
            Vector3 circleCenter = Vector3.UnitY * y;
            vertices.Add(circleCenter);
            normals.Add(circleCenter.Normalized());
            for (double x = 0; x < segments; x++)  
            {
                double theta = x / (segments - 1) * 2 * Math.PI;

                var v = new Vector3((float)Math.Cos(theta) / 2, y, (float)Math.Sin(theta) / 2);
                vertices.Add(v);
                normals.Add(new Vector3(v.X, 0, v.Z).Normalized());
            }
        }

        var indices = new List<uint>();

        for (uint x = 1; x < segments; x++)
        {
            indices.Add(0);
            indices.Add(x);
            indices.Add(x + 1);
            
            indices.Add(segments + 1);
            indices.Add(x + segments + 1);
            indices.Add(x + segments + 2);
            
            indices.Add(x);
            indices.Add(x + segments + 1);
            indices.Add(x + segments + 2);
            
            indices.Add(x + segments + 2);
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