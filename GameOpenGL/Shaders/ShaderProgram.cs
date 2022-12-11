using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace GameOpenGL.Shaders;

public class ShaderProgram : IDisposable
{
    public readonly ProgramHandle Handle;
    private bool _disposedValue = false;

    public ShaderProgram(string vertexShaderSource, string fragmentShaderSource)
    {
        var vertexShader = new VertexShader(vertexShaderSource);
        var fragmentShader = new FragmentShader(fragmentShaderSource);
        
        Handle = GL.CreateProgram();
        GL.AttachShader(Handle, vertexShader.Handle);
        GL.AttachShader(Handle, fragmentShader.Handle);
        
        GL.LinkProgram(Handle);
        var linkStatusCode = 0;
        GL.GetProgrami(Handle, ProgramPropertyARB.LinkStatus, ref linkStatusCode);
        if (linkStatusCode != (int)All.True)
        {
            GL.GetProgramInfoLog(Handle, out string? infoLog);
            throw new Exception(infoLog);
        }
        
        DetachAndDeleteShader(vertexShader);
        DetachAndDeleteShader(fragmentShader);
    }
    
    public void Use() => GL.UseProgram(Handle);

    public void Deactivate() => GL.UseProgram(ProgramHandle.Zero);

    public void Delete() => GL.DeleteProgram(Handle);

    private void DetachAndDeleteShader(Shader shader)
    {
        GL.DetachShader(Handle, shader.Handle);
        GL.DeleteShader(shader.Handle);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue) return;
        
        GL.DeleteProgram(Handle);
        _disposedValue = true;
    }
    
    ~ShaderProgram()
    {
        GL.DeleteProgram(Handle);
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}