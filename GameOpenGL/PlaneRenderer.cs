using GameOpenGL.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace GameOpenGL;

public class PlaneRenderer : Renderer
{
    public readonly VertexArrayObject VertexArrayObject;
    private readonly ElementsBufferObject _elements;

    public PlaneRenderer(ShaderProgram shader, Material material) : base(shader, material)
    {
        var vertices = new float[]
        {
            -1, 0, -1, 0, 1, 0, // left bottom
            -1, 0, 1, 0, 1, 0,  // left top
            1, 0, 1, 0, 1, 0,   // right top
            1, 0, -1, 0, 1, 0,  // right bottom
        };

        var indices = new uint[]
        {
            0, 1, 2,
            0, 2, 3
        };
        
        VertexArrayObject = new VertexArrayObject(vertices);
        _elements = new ElementsBufferObject(VertexArrayObject, indices);
        VertexArrayObject.VertexAttributePointer(0, 3, false, 6 * sizeof(float), 0);
        VertexArrayObject.VertexAttributePointer(1, 3, false, 6 * sizeof(float), 3 * sizeof(float));
    }

    public override void Draw(Vector3 viewPosition, Matrix4 view, Matrix4 projection)
    {
        base.Draw(viewPosition, view, projection);
        
        ShaderProgram.Use();
        
        _elements.BindVertexArrayAndDraw(PrimitiveType.Triangles);
    }
}