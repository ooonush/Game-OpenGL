using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace GameOpenGL;

public class Rotator : Component
{
    public override void Update()
    {
        // Transform.Rotation.ToAxisAngle(out Vector3 axis, out float angle);
        GL.Rotate(1, 0, 1, 0);
        base.Update();
    }
}