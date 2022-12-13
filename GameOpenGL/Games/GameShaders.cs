using System.Diagnostics;
using GameOpenGL.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace GameOpenGL;

public class TestGame3 : Game
{
    private readonly Stopwatch _timer = new();
    
    private readonly float[] _vertices =
    {
        // positions        // colors
        0.5f, -0.5f, 0.0f,  1.0f, 0.0f, 0.0f,   // bottom right
        -0.5f, -0.5f, 0.0f,  0.0f, 1.0f, 0.0f,   // bottom left
        0.0f,  0.5f, 0.0f,  0.0f, 0.0f, 1.0f    // top 
    };

    private ShaderProgram? _shaderProgram;
    private VertexArrayObject? _objectVAO;

    public TestGame3(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) 
        : base(gameWindowSettings, nativeWindowSettings) { }

    protected override void OnLoad()
    {
        base.OnLoad();
        
        _timer.Start();
        
        _objectVAO = new VertexArrayObject(_vertices);
        
        string vertexShaderSource = File.ReadAllText("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Shaders/Source/shader1.vert");
        string fragmentShaderSource = File.ReadAllText("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Shaders/Source/shader1.frag");
        _shaderProgram = new ShaderProgram(vertexShaderSource, fragmentShaderSource);
        _shaderProgram.Use();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
        
        if (_shaderProgram == null) return;
        double timeValue = _timer.Elapsed.TotalSeconds;
        float greenValue = (float)Math.Sin(timeValue) / (2.0f + 0.5f);
        
        int vertexColorLocation = GL.GetUniformLocation(_shaderProgram.Handle, "ourColor");
        GL.Uniform4f(vertexColorLocation, 0.0f, greenValue, 0.0f, 1.0f);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        _shaderProgram?.Use();
        
        _objectVAO?.BindAndDrawArrays(PrimitiveType.Triangles);
        
        SwapBuffers();
    }

    protected override void OnUnload()
    {
        base.OnUnload();
        
        _shaderProgram?.Dispose();
    }
}