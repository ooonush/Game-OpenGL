using System.Drawing;
using GameOpenGL.Shaders;
using ObjRenderer;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GameOpenGL;

public class Game3D : Game
{
    public Game3D(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings) { }

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
        if (e.Key == Keys.P)
        {        
            GL.PointSize(15);
            GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Point);
        }
        if (e.Key == Keys.F)
        {
            GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Fill);
        }
    }

    protected override void OnLoad()
    {
        GL.Enable(EnableCap.DepthTest);
        
        string vertexShaderSource = File.ReadAllText("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Shaders/Source/shaderColor.vert");
        string fragmentShaderSource = File.ReadAllText("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Shaders/Source/shaderMaterial.frag");
        
        var shader = new ShaderProgram(vertexShaderSource, fragmentShaderSource);
        
        vertexShaderSource = File.ReadAllText("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Shaders/Source/shaderTexture.vert");
        fragmentShaderSource = File.ReadAllText("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Shaders/Source/shaderTexture.frag");
        
        var textureShader = new ShaderProgram(vertexShaderSource, fragmentShaderSource);
        
        var camera = Scene.CreateGameObject().AddComponent<Camera>();
        var inputSystem = new InputSystem(this);
        camera.AddComponent(new Move(inputSystem, 3));
        camera.AddComponent(new LookMouse(inputSystem));
        camera.Transform.Position = new Vector3(0, 0, 3);
        
        var pointLight = Scene.CreateGameObject(new Transform(2, 2, 4)).AddComponent<PointLight>();
        pointLight.Transform.Scale = new Vector3(0.15f);
        
        DirectionalLight directionalLight = Scene.CreateGameObject(new Transform(2, 2, -4)).AddComponent(new DirectionalLight());
        directionalLight.Transform.Forward = new Vector3(0, -1f, 0.5f);
        
        BoxRenderer bronzeBox = Scene.CreateGameObject(new Transform(2, 4, 0))
            .AddComponent(new BoxRenderer(shader, Material.Bronze));
        bronzeBox.Transform.Scale = new Vector3(2, 1, 2);
        
        SphereRenderer greenSphere = Scene.CreateGameObject()
            .AddComponent(new SphereRenderer(shader, Material.GreenPlastic));
        greenSphere.AddComponent(new AnimatedScale(time => (float)Math.Cos(time)));
        
        BoxRenderer goldBox = Scene.CreateGameObject(new Transform(4, 4, 0))
            .AddComponent(new BoxRenderer(shader, Material.Gold));
        goldBox.AddComponent(new AnimatedRotation(time => new Quaternion(time, 0, 0), 1));
        goldBox.AddComponent(new RotateFromParentTransform());
        
        var textureMaterial = new Material
        {
            Texture = Texture.LoadFromFile("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Textures/image.jpg")
        };
        
        TextureBoxRenderer alvenBox = Scene.CreateGameObject(new Transform(4, 0, 0))
            .AddComponent(new TextureBoxRenderer(textureShader, textureMaterial));
        alvenBox.AddComponent(new AnimatedScale(time => 3  * Math.Abs((float)Math.Cos(time)), 0.5f));

        alvenBox.AddComponent(new AnimatedMove(time => new Vector3(0, 3 * (float)Math.Sin(time), 0)));
        
        goldBox.Transform.Parent = greenSphere.Transform;
        
        BoxRenderer greenBox = Scene.CreateGameObject(new Transform(0.75f, 0, 0f))
            .AddComponent(new BoxRenderer(shader, Material.GreenPlastic));
        greenBox.Transform.Parent = alvenBox.Transform;
        greenBox.Transform.LocalScale = new Vector3(0.5f);
        
        PolygonMesh2D polygon = Scene.CreateGameObject(new Transform(3, 5, 3)).AddComponent(new PolygonMesh2D(shader, new Material(Color4.Red), 5));
        PolygonMesh2D circle = Scene.CreateGameObject(new Transform(5, 5, 3)).AddComponent(new PolygonMesh2D(shader, new Material(Color4.Red), 50));
        CylinderRenderer cylinder = Scene.CreateGameObject(new Transform(3, 3, 3)).AddComponent(new CylinderRenderer(shader, new Material(Color4.Red)));
        CylinderRenderer pentagon = Scene.CreateGameObject(new Transform(2, 4, 2)).AddComponent(new CylinderRenderer(shader, new Material(Color4.Yellow), 5));
        ConusRenderer conus = Scene.CreateGameObject(new Transform(3, 3, 5)).AddComponent(new ConusRenderer(shader, new Material(Color4.Blue)));
        ConusRenderer piramida = Scene.CreateGameObject(new Transform(3, 1, 5)).AddComponent(new ConusRenderer(shader, new Material(Color4.Blue), 4));
        TorusRenderer torus = Scene.CreateGameObject(new Transform(0, 1, 1)).AddComponent(new TorusRenderer(shader, new Material(Color4.Blue)));
        
        // Console.WriteLine(Environment.CurrentDirectory);
        // Mesh weaponMesh = ObjLoader.Load("C:/Users/ooonu/Desktop/untitled.obj");
        // var weaponMaterial = new Material
        // {
        //     Texture = Texture.LoadFromFile("C:/Users/ooonu/Desktop/spitfire_base_main_albedoTexture.png"),
        //     Ambient = new Vector3(0.6f),
        //     Diffuse = new Vector3(0.8f),
        //     Specular = new Vector3(1f)
        // };
        // ObjRenderer weapon = Scene.CreateGameObject(new Transform(4, 2, 4)).AddComponent(new ObjRenderer(textureShader, weaponMaterial, weaponMesh));
        
        base.OnLoad();
    }
    
    protected override void OnRenderFrame(FrameEventArgs args)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        
        base.OnRenderFrame(args);
        SwapBuffers();
    }
}