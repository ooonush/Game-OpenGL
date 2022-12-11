using OpenTK.Graphics.OpenGL4;

namespace GameOpenGL.Shaders;

public class ShaderProgram : IDisposable
{
    public readonly int Handle;
    private bool _disposedValue = false;

    public ShaderProgram(string vertexShaderSource, string fragmentShaderSource)
    {
        var vertexShader = new VertexShader(vertexShaderSource);
        var fragmentShader = new FragmentShader(fragmentShaderSource);
        
        Handle = GL.CreateProgram();
        GL.AttachShader(Handle, vertexShader.Handle);
        GL.AttachShader(Handle, fragmentShader.Handle);
        
        GL.LinkProgram(Handle);
        
        GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int linkStatusCode);
        if (linkStatusCode != (int)All.True)
        {
            string? infoLog = GL.GetProgramInfoLog(Handle);
            throw new Exception(infoLog);
        }
        
        DetachAndDeleteShader(vertexShader);
        DetachAndDeleteShader(fragmentShader);
    }
    
    public void Use() => GL.UseProgram(Handle);

    public void Deactivate() => GL.UseProgram(0);

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