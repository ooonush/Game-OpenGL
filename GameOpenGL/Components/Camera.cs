using System.Drawing;
using GameOpenGL.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GameOpenGL;

public class Camera : Component
{
    private Color4<Rgba> _backgroundColor = Color4.Blue;
    public Color4<Rgba> BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            _backgroundColor = value;
            GL.ClearColor(BackgroundColor);
        }
    }

    public Matrix4 View;
    public Matrix4 Projection;
    
    private const float Speed = 1.5f;

    private readonly Vector3 _front = new(0.0f, 0.0f, -1.0f);
    private readonly Vector3 _up = new(0.0f, 1.0f,  0.0f);
    
    public override void OnLoad()
    {
        GL.ClearColor(BackgroundColor);
    }

    public override void Update()
    {
        KeyboardState input = GameObject.KeyboardState;
        
        if (input.IsKeyDown(Keys.W))
        {
            Transform.Position += _front * Speed; //Forward 
        }

        if (input.IsKeyDown(Keys.S))
        {
            Transform.Position -= _front * Speed; //Backwards
        }

        if (input.IsKeyDown(Keys.A))
        {
            Transform.Position -= Vector3.Normalize(Vector3.Cross(_front, _up)) * Speed; //Left
        }

        if (input.IsKeyDown(Keys.D))
        {
            Transform.Position += Vector3.Normalize(Vector3.Cross(_front, _up)) * Speed; //Right
        }
        
        if (input.IsKeyDown(Keys.Space))
        {
            Transform.Position += _up * Speed; //Up 
        }
        
        if (input.IsKeyDown(Keys.LeftShift))
        {
            Transform.Position -= _up * Speed; //Down
        }
        
        View = Matrix4.LookAt(Transform.Position, Vector3.Zero, Vector3.UnitY);
    }

    public override void OnResize(ResizeEventArgs e)
    {
        GL.Viewport(0, 0, e.Width, e.Height);

        float fovy = MathHelper.DegreesToRadians(45.0f);
        float aspect = (float)e.Width / e.Height;
        Projection = Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, 0.1f, 100.0f);
    }

    public override void Render()
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
    }
}