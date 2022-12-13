using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace GameOpenGL;

public class VertexBufferObject
{
    public readonly BufferTargetARB Target;
    public readonly BufferHandle BufferHandle;

    public VertexBufferObject(float[] vertices, BufferTargetARB target, BufferUsageARB usage = BufferUsageARB.StaticDraw)
    {
        Target = target;
        BufferHandle = GL.GenBuffer();
        
        GL.BindBuffer(target, BufferHandle);
        GL.BufferData(target, vertices, usage);
        
        GL.BindBuffer(target, BufferHandle.Zero);
    }

    public void BindBuffer()
    {
        GL.BindBuffer(Target, BufferHandle);
    }
    
    public void UnbindBuffer()
    {
        GL.BindBuffer(Target, BufferHandle.Zero);
    }
}