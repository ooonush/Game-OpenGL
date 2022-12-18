using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace GameOpenGL;

public class VertexArrayObject
{
    public readonly VertexArrayHandle Handle;
    public readonly float[] Vertices;
    public readonly VertexBufferObject VertexBufferObject;

    public VertexArrayObject(float[] vertices, BufferUsageARB usage = BufferUsageARB.StaticDraw)
    {
        Vertices = vertices;
        VertexBufferObject = new VertexBufferObject(vertices, BufferTargetARB.ArrayBuffer, usage);
        VertexBufferObject.BindBuffer();
        
        Handle = GL.GenVertexArray();
        
        VertexBufferObject.UnbindBuffer();
        UnbindVertexArray();
    }

    public void VertexAttributePointer(uint index, int size, bool normalized, int stride, int offset)
    {
        BindVertexArray();
        VertexBufferObject.BindBuffer();
        
        GL.EnableVertexAttribArray(index);
        GL.VertexAttribPointer(index, size, VertexAttribPointerType.Float, normalized, stride, offset);
    }
    
    public void BindAndDrawArrays(PrimitiveType primitiveType)
    {
        BindVertexArray();
        GL.DrawArrays(primitiveType, 0, Vertices.Length);
    }
    
    public void BindVertexArray()
    {
        GL.BindVertexArray(Handle);
    }

    public void UnbindVertexArray()
    {
        GL.BindVertexArray(VertexArrayHandle.Zero);
    }
}