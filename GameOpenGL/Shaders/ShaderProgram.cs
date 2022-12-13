using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace GameOpenGL.Shaders;

public sealed class ShaderProgram : IDisposable
{
    ~ShaderProgram()
    {
        GL.DeleteProgram(Handle);
    }

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

    public void Dispose()
    {
        if (!_disposedValue)
        {
            GL.DeleteProgram(Handle);
            _disposedValue = true;
        }
        
        GC.SuppressFinalize(this);
    }

    public void SetMatrix4(string name, bool transpose, Matrix4 matrix)
    {
        int location = GL.GetUniformLocation(Handle, name);

        GL.UniformMatrix4f(location, transpose, matrix);
    }
}