using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

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
    public readonly ShaderHandle Handle;

    protected Shader(ShaderType shaderType, string shaderSource)
    {
        ShaderType = shaderType;

        Handle = GL.CreateShader(shaderType);
        
        GL.ShaderSource(Handle, shaderSource);
        GL.CompileShader(Handle);
        
        var compileCode = 0;
        GL.GetShaderi(Handle, ShaderParameterName.CompileStatus, ref compileCode);
        
        if (compileCode == (int)All.True) return;
        GL.GetShaderInfoLog(Handle, out string? infoLog);
        throw new Exception(infoLog);
    }
}