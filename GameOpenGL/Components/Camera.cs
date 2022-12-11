using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace GameOpenGL;

public class Camera : Component
{
    private Color4 _backgroundColor = Color4.Blue;
    public Color4 BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            _backgroundColor = value;
            GL.ClearColor(BackgroundColor);
        }
    }

    public override void OnLoad()
    {
        GL.ClearColor(BackgroundColor);
    }

    public override void OnResize(ResizeEventArgs e)
    {
        GL.Viewport(0, 0, e.Width, e.Height);
        
        // Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI/4,
        //     e.Width / (float)e.Height, 1, 64.0f);
        //
        // Console.WriteLine(e.Width + "x" + e.Height);
        //
        // GL.MatrixMode(MatrixMode.Projection);
        // GL.LoadMatrix(ref projection);
    }

    public override void Render()
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);

        // Matrix4 modelview = Matrix4.LookAt(Transform.Position, Vector3.UnitZ, Vector3.UnitY);
        // GL.MatrixMode(MatrixMode.Modelview);
        // GL.LoadMatrix(ref modelview);
    }
}