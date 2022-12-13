using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace GameOpenGL;

public class ElementsBufferObject
{
    public readonly VertexArrayObject VAO;
    public BufferHandle BufferHandle;
    private readonly uint[] _indices;
    
    public ElementsBufferObject(VertexArrayObject vao, uint[] indices, BufferUsageARB usage = BufferUsageARB.StaticDraw)
    {
        VAO = vao;
        _indices = indices;
        vao.BindVertexArray();
        
        BufferHandle = GL.GenBuffer();
        GL.BindBuffer(BufferTargetARB.ElementArrayBuffer, BufferHandle);
        GL.BufferData(BufferTargetARB.ElementArrayBuffer, indices, usage);
        
        GL.BindVertexArray(VertexArrayHandle.Zero);
        GL.BindBuffer(BufferTargetARB.ElementArrayBuffer, BufferHandle.Zero);
    }

    public void BindVertexArrayAndDraw(PrimitiveType primitiveType)
    {
        VAO.BindVertexArray();
        GL.DrawElements(primitiveType, _indices.Length, DrawElementsType.UnsignedInt, 0);
    }
}