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
        BindVertexArray();
        
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
        
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);
        
        VertexBufferObject.UnbindBuffer();
        UnbindVertexArray();
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