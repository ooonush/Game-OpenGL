using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace GameOpenGL;

public class TextureVAO
{
    public readonly VertexArrayHandle VertexArrayHandle;
    public readonly float[] Vertices;
    public readonly BufferHandle BufferHandle;

    public TextureVAO(float[] vertices, BufferUsageARB usage = BufferUsageARB.StaticDraw)
    {
        Vertices = vertices;
        BufferHandle = GL.GenBuffer();
        
        GL.BindBuffer(BufferTargetARB.ArrayBuffer, BufferHandle);
        GL.BufferData(BufferTargetARB.ArrayBuffer, vertices, usage);
        
        VertexArrayHandle = GL.GenVertexArray();
        BindVertexArray();
        
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
        
        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);
        
        GL.BindBuffer(BufferTargetARB.ArrayBuffer, BufferHandle.Zero);
        
        GL.BindBuffer(BufferTargetARB.ArrayBuffer, BufferHandle.Zero);
        UnbindVertexArray();
    }
    
    public void BindAndDrawArrays(PrimitiveType primitiveType)
    {
        BindVertexArray();
        GL.DrawArrays(primitiveType, 0, Vertices.Length);
    }
    
    public void BindVertexArray()
    {
        GL.BindVertexArray(VertexArrayHandle);
    }

    public void UnbindVertexArray()
    {
        GL.BindVertexArray(VertexArrayHandle.Zero);
    }
}