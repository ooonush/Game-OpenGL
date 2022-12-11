using OpenTK.Graphics.OpenGL4;

namespace GameOpenGL.Shaders;

public class VertexShader : Shader
{
    public VertexShader(string shaderSource) : base(ShaderType.VertexShader, shaderSource)
    {
    }
}

public class FragmentShader : Shader
{
    public FragmentShader(string shaderSource) : base(ShaderType.FragmentShader, shaderSource)
    {
    }
}

public abstract class Shader
{
    public readonly ShaderType ShaderType;
    public readonly int Handle;

    protected Shader(ShaderType shaderType, string shaderSource)
    {
        ShaderType = shaderType;

        Handle = GL.CreateShader(shaderType);
        
        GL.ShaderSource(Handle, shaderSource);
        GL.CompileShader(Handle);
        
        GL.GetShader(Handle, ShaderParameter.CompileStatus, out int compileCode);
        
        if (compileCode == (int)All.True) return;
        string? infoLog = GL.GetShaderInfoLog(Handle);
        throw new Exception(infoLog);
    }
}