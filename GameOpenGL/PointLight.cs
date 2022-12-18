using GameOpenGL.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace GameOpenGL;

public class PointLight : Component, IRenderer
{
    public readonly ShaderProgram ShaderProgram;
    
    public Vector3 Ambient = new(0.05f, 0.05f, 0.05f);
    public Vector3 Diffuse = new Vector3(0.8f, 0.8f, 0.8f);
    public Vector3 Specular = new Vector3(1.0f, 1.0f, 1.0f);
    public float Constant = 1;
    public float Linear = 0.09f;
    public float Quadratic = 0.032f;
    
    public readonly BoxMesh Mesh = new();
    
    public PointLight()
    {
        string vertexShaderSource = File.ReadAllText("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Shaders/Source/shaderLight.vert");
        string fragmentShaderSource = File.ReadAllText("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Shaders/Source/shaderLight.frag");
        
        ShaderProgram = new ShaderProgram(vertexShaderSource, fragmentShaderSource);
    }

    public void Draw(Vector3 viewPosition, Matrix4 view, Matrix4 projection)
    {
        ShaderProgram.UniformMatrix4f("model", true, Transform.GetModelMatrix());
        ShaderProgram.UniformMatrix4f("view", true, view);
        ShaderProgram.UniformMatrix4f("projection", true, projection);
        
        ShaderProgram.Use();
        Mesh.VertexArrayObject.BindAndDrawArrays(PrimitiveType.Triangles);
    }
}