using GameOpenGL.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GameOpenGL;

public class Game2 : Game
{
    public Game2(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) 
        : base(gameWindowSettings, nativeWindowSettings)
    {
    }

    protected override void OnKeyDown(KeyboardKeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (e.Key == Keys.E)
        {
            CursorState = CursorState == CursorState.Grabbed ? CursorState.Normal : CursorState.Grabbed;
        }

        if (e.Key == Keys.L)
        {
            GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Line);
        }
        GL.PointSize(15);
        if (e.Key == Keys.P)
        {
            GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Point);
        }
        if (e.Key == Keys.F)
        {
            GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Fill);
        }
    }
    
    protected override void OnLoad()
    {
        base.OnLoad();
        GL.Enable(EnableCap.DepthTest);
        
        string vertexShaderSource = File.ReadAllText("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Shaders/Source/shaderColor.vert");
        string fragmentShaderSource = File.ReadAllText("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Shaders/Source/shaderMaterial.frag");
        
        var shader = new ShaderProgram(vertexShaderSource, fragmentShaderSource);
        
        vertexShaderSource = File.ReadAllText("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Shaders/Source/shaderTexture.vert");
        fragmentShaderSource = File.ReadAllText("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Shaders/Source/shaderTexture.frag");
        
        var textureShader = new ShaderProgram(vertexShaderSource, fragmentShaderSource);

        Material gold = Material.Gold;

        Scene.CreateGameObject().AddComponent(new SphereRenderer(shader, gold));
        
        var inputSystem = new InputSystem(this);
        Scene.CreateGameObject()
            .AddComponent(new Camera())
            .AddComponent(new Move(inputSystem, 2))
            .AddComponent(new LookMouse(inputSystem));
        
        Scene.CreateGameObject().AddComponent(new DirectionalLight());
        Scene.CreateGameObject(new Transform(2, 2, 2)).AddComponent(new PointLight());
        
        
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        base.OnRenderFrame(args);
        
        SwapBuffers();
    }
}