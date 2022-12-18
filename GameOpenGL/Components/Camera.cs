using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace GameOpenGL;

public class Camera : Component
{
    private Color4<Rgba> _backgroundColor = Color4.Darkgray;
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
    
    public override void OnLoad()
    {
        GL.ClearColor(BackgroundColor);
    }

    public override void LateUpdate()
    {
        View = Matrix4.LookAt(Transform.Position,  Transform.Position + Transform.Forward, Transform.Up);
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
        
        var renderers = FindObjectsOfType<IRenderer>();
        foreach (IRenderer renderer in renderers)
        {
            renderer.Draw(Transform.Position, View, Projection);
        }
    }
}