using GameOpenGL.Shaders;
using ObjRenderer;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace GameOpenGL;

public class ObjRenderer : Renderer
{
    private readonly VertexArrayObject _vao;

    public ObjRenderer(ShaderProgram shader, Material material, Mesh mesh) : base(shader, material)
    {
        var vertices = new List<float>();
        
        for (var i = 0; i < mesh.verts.Count; i++)
        {
            Vector3 vertex = mesh.verts[i];
            Vector3 normal = mesh.norms[i];
            
            vertices.Add(vertex.X);
            vertices.Add(vertex.Y);
            vertices.Add(vertex.Z);
            
            vertices.Add(normal.X);
            vertices.Add(normal.Y);
            vertices.Add(normal.Z);
            
            if (material.Texture == null) continue;
            Vector3 textCoord = mesh.textCoords[i];
            vertices.Add(textCoord.X);
            vertices.Add(textCoord.Y);
        }
        
        _vao = new VertexArrayObject(vertices.ToArray());
        _vao.VertexAttributePointer(0, 3, false, 8 * sizeof(float), 0);
        _vao.VertexAttributePointer(1, 3, false, 8 * sizeof(float), 3 * sizeof(float));
        if (material.Texture != null)
        {
            _vao.VertexAttributePointer(2, 2, false, 8 * sizeof(float), 6 * sizeof(float));
        }

        // var indices = new List<uint>();
        // for (var i = 0; i < mesh.vertexIndices.Count / 4; i++)
        // {
        //     indices.Add(mesh.vertexIndices[i]);
        //     indices.Add(mesh.vertexIndices[i] + 1);
        //     indices.Add(mesh.vertexIndices[i] + 2);
        //     
        //     indices.Add(mesh.vertexIndices[i] + 3);
        //     indices.Add(mesh.vertexIndices[i] + 2);
        //     indices.Add(mesh.vertexIndices[i]);
        // }
        //
        // _elements = new ElementsBufferObject(vao, indices.ToArray());
    }

    public override void Draw(Vector3 viewPosition, Matrix4 view, Matrix4 projection)
    {
        base.Draw(viewPosition, view, projection);
        Material.Texture?.Use(TextureUnit.Texture0);

        ShaderProgram.Use();
        _vao.BindAndDrawArrays(PrimitiveType.Triangles);
    }
}