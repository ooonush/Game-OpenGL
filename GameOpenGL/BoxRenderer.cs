using GameOpenGL.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace GameOpenGL;

public class TextureBoxRenderer : Renderer
{
    public readonly TextureBoxMesh Mesh = new();
    
    public TextureBoxRenderer(ShaderProgram shader, Material material) : base(shader, material) { }
    
    public override void Draw(Vector3 viewPosition, Matrix4 view, Matrix4 projection)
    {
        base.Draw(viewPosition, view, projection);
        Material.Texture?.Use(TextureUnit.Texture0);
        ShaderProgram.Use();
        
        Mesh.VertexArrayObject.BindAndDrawArrays(PrimitiveType.Triangles);
    }
}

public class BoxRenderer : Renderer
{
    public readonly BoxMesh Mesh = new();
    
    public BoxRenderer(ShaderProgram shader, Material material) : base(shader, material) { }
    
    public override void Draw(Vector3 viewPosition, Matrix4 view, Matrix4 projection)
    {
        base.Draw(viewPosition, view, projection);
        ShaderProgram.Use();
        
        Mesh.VertexArrayObject.BindAndDrawArrays(PrimitiveType.Triangles);
    }
}