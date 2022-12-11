using OpenTK.Mathematics;

namespace GameOpenGL;

public class Transform : Component
{
    public Vector3 Position = Vector3.Zero;
    public Vector3d Scale = new(1, 1, 1);
    
    public Transform() { }

    public Transform(float x, float y, float z) : this(new Vector3(x, y, z)) { }

    public Transform(Vector3 position)
    {
        Position = position;
    }
}