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
    private readonly Dictionary<string, int> _uniformLocations;

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
        
        var numberOfUniforms = 0;
        GL.GetProgrami(Handle, ProgramPropertyARB.ActiveUniforms, ref numberOfUniforms);
        var uniformMaxLength = 0;
        GL.GetProgrami(Handle, ProgramPropertyARB.ActiveUniformMaxLength, ref uniformMaxLength);
        
        // Next, allocate the dictionary to hold the locations.
        _uniformLocations = new Dictionary<string, int>();
        
        // Loop over all the uniforms,
        for (uint i = 0; i < numberOfUniforms; i++)
        {
            // get the name of this uniform,
            string key = GL.GetActiveUniform(Handle, i, uniformMaxLength, new int[1], new int[1], new UniformType[1]);
            
            // get the location,
            int location = GL.GetUniformLocation(Handle, key);
            
            // and then add it to the dictionary.
            _uniformLocations.Add(key, location);
        }
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

    public void UniformMatrix4f(string name, bool transpose, Matrix4 matrix)
    {
        Use();
        GL.UniformMatrix4f(_uniformLocations[name], transpose, matrix);
        GL.UseProgram(ProgramHandle.Zero);
    }
    
    public void Uniform3f(string name, Vector3 vector)
    {
        Use();
        GL.Uniform3f(_uniformLocations[name], in vector);
        GL.UseProgram(ProgramHandle.Zero);
    }
    
    public void Uniform4f(string name, Vector4 vector)
    {
        Use();
        GL.Uniform4f(_uniformLocations[name], in vector);
        GL.UseProgram(ProgramHandle.Zero);
    }

    public uint GetAttribLocation(string name)
    {
        return (uint)GL.GetAttribLocation(Handle, name);
    }

    public void Uniform1f(string name, float value)
    {
        Use();
        GL.Uniform1f(_uniformLocations[name], in value);
        GL.UseProgram(ProgramHandle.Zero);
    }
    
    public void Uniform1i(string name, int value)
    {
        Use();
        GL.Uniform1i(_uniformLocations[name], in value);
        GL.UseProgram(ProgramHandle.Zero);
    }
}