using GameOpenGL.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace GameOpenGL;

public class SphereRenderer : Renderer
{
    private readonly List<float> _vertices = new();

    public readonly VertexArrayObject VertexArrayObject;

    public SphereRenderer(ShaderProgram shader, Material material) : base(shader, material)
    {
        var nx = 1000;
        var ny = 1000;
        int i, ix, iy;
        float x, y, z;
        var r = 0.5f;
        for (iy = 0; iy < ny; ++iy)
        {
            for (ix = 0; ix <= nx; ++ix)
            {
                x = (float)(r * Math.Sin(iy * Math.PI / ny) * Math.Cos(2 * ix * Math.PI / nx));
                y = (float)(r * Math.Sin(iy * Math.PI / ny) * Math.Sin(2 * ix * Math.PI / nx));
                z = (float)(r * Math.Cos(iy * Math.PI / ny));
                _vertices.Add(x);
                _vertices.Add(y);
                _vertices.Add(z);
                
                x = (float)(r * Math.Sin((iy + 1) * Math.PI / ny) * Math.Cos(2 * ix * Math.PI / nx));
                y = (float)(r * Math.Sin((iy + 1) * Math.PI / ny) * Math.Sin(2 * ix * Math.PI / nx));
                z = (float)(r * Math.Cos((iy + 1) * Math.PI / ny));
                _vertices.Add(x);
                _vertices.Add(y);
                _vertices.Add(z);
            }
        }
        
        VertexArrayObject = new VertexArrayObject(_vertices.ToArray());
        VertexArrayObject.VertexAttributePointer(0, 3, false, 3 * sizeof(float), 0);
        VertexArrayObject.VertexAttributePointer(1, 3, false, 3 * sizeof(float), 0);
    }

    public override void Draw(Vector3 viewPosition, Matrix4 view, Matrix4 projection)
    {
        base.Draw(viewPosition, view, projection);
        
        ShaderProgram.Use();
        
        VertexArrayObject.BindAndDrawArrays(PrimitiveType.TriangleStrip);
    }
}