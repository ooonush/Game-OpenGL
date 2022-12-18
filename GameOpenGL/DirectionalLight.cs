using OpenTK.Mathematics;

namespace GameOpenGL;

public class DirectionalLight : Component
{
    public Vector3 Direction => Transform.Forward;
    public Vector3 Ambient = new(0.05f);
    public Vector3 Diffuse = new(0.4f);
    public Vector3 Specular = new(0.5f);
}