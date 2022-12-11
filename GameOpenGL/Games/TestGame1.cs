using GameOpenGL.Shaders;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace GameOpenGL;

public class TestGame1 : Game
{
    private readonly float[] _vertices = {
        -0.5f, -0.5f, 0.0f, //Bottom-left vertex
        0.5f, -0.5f, 0.0f, //Bottom-right vertex
        0.0f,  0.5f, 0.0f  //Top vertex
    };

    private ShaderProgram _shaderProgram;
    private BufferHandle _vertexBufferObject;
    private VertexArrayHandle _vertexArrayObject;

    public TestGame1(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) 
        : base(gameWindowSettings, nativeWindowSettings)
    {
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        
        _vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTargetARB.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTargetARB.ArrayBuffer, _vertices, BufferUsageARB.StaticDraw);
        
        _vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(_vertexArrayObject);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
        
        string vertexShaderSource = File.ReadAllText("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Shaders/Source/shader.vert");
        string fragmentShaderSource = File.ReadAllText("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Shaders/Source/shader.frag");
        _shaderProgram = new ShaderProgram(vertexShaderSource, fragmentShaderSource);
        _shaderProgram.Use();
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        _shaderProgram.Use();
        
        GL.BindVertexArray(_vertexArrayObject);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        
        SwapBuffers();
    }

    protected override void OnUnload()
    {
        base.OnUnload();
        
        _shaderProgram.Dispose();
    }
}