using OpenTK.Mathematics;

namespace GameOpenGL;

public class Material
{
    public Vector3 Ambient;
    public Vector3 Diffuse;
    public Vector3 Specular;
    public Texture? Texture;
    
    public float Shininess = 32f;
    
    public Material(Color4<Rgba> color)
    {
        SetColor(color);
    }
    
    public Material(Vector3 ambient, Vector3 diffuse, Vector3 specular, float shininess = 32f)
    {
        Ambient = ambient;
        Diffuse = diffuse;
        Specular = specular;
        Shininess = shininess;
    }
    
    public Material() : this(Color4.White) { }
    
    public void SetColor(Color4<Rgba> color)
    {
        Ambient = new Vector3(color.X, color.Y, color.Z);
        Diffuse = new Vector3(color.X, color.Y, color.Z);
        Specular = new Vector3(0.5f * color.X, 0.5f * color.Y, 0.5f * color.Z);
    }
    
    public static Material Bronze => new(
        new Vector3(0.2125f, 0.1275f, 0.054f),
        new Vector3(0.714f, 0.4284f, 0.18144f),
        new Vector3(0.393548f, 0.271906f, 0.166721f),
        0.2f);

    public static Material Gold => new(
        new Vector3(0.24725f, 0.1995f, 0.0745f),
        new Vector3(0.75164f, 0.60648f, 0.22648f),
        new Vector3(0.628281f, 0.555802f, 0.366065f),
        0.4f);

    public static Material GreenPlastic => new(
        new Vector3(0, 0, 0),
        new Vector3(0.1f, 0.35f, 0.1f),
        new Vector3(0.3f),
        0.25f);
}