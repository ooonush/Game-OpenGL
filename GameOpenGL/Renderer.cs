using GameOpenGL.Shaders;
using OpenTK.Mathematics;

namespace GameOpenGL;

public interface IRenderer
{
    void Draw(Vector3 viewPosition, Matrix4 view, Matrix4 projection);
}

public abstract class Renderer : Component, IRenderer
{
    public readonly Material Material;
    public readonly ShaderProgram ShaderProgram;
    
    protected Renderer(ShaderProgram shader, Material material)
    {
        ShaderProgram = shader;
        Material = material;
    }
    
    public virtual void Draw(Vector3 viewPosition, Matrix4 view, Matrix4 projection)
    {
        ShaderProgram.Uniform3f("viewPos", viewPosition);
        
        ShaderProgram.Uniform3f("material.ambient", Material.Ambient);
        ShaderProgram.Uniform3f("material.diffuse", Material.Diffuse);
        ShaderProgram.Uniform3f("material.specular", Material.Specular);
        ShaderProgram.Uniform1f("material.shininess", Material.Shininess);
        
        ShaderProgram.UniformMatrix4f("model", true, Transform.GetModelMatrix());
        ShaderProgram.UniformMatrix4f("view", true, view);
        ShaderProgram.UniformMatrix4f("projection", true, projection);
        
        var pointLights = FindObjectsOfType<PointLight>();
        // Point lights
        ShaderProgram.Uniform1i("pointLightsCount", pointLights.Length);
        for (var i = 0; i < pointLights.Length; i++)
        {
            PointLight pointLight = pointLights[i];
            
            ShaderProgram.Uniform3f($"pointLights[{i}].position", pointLight.Transform.Position);
            ShaderProgram.Uniform3f($"pointLights[{i}].ambient", pointLight.Ambient);
            ShaderProgram.Uniform3f($"pointLights[{i}].diffuse", pointLight.Diffuse);
            ShaderProgram.Uniform3f($"pointLights[{i}].specular", pointLight.Specular);
            
            ShaderProgram.Uniform1f($"pointLights[{i}].constant", pointLight.Constant);
            ShaderProgram.Uniform1f($"pointLights[{i}].linear", pointLight.Linear);
            ShaderProgram.Uniform1f($"pointLights[{i}].quadratic", pointLight.Quadratic);
        }
        
        var dirLight = FindObjectOfType<DirectionalLight>();
        
        if (dirLight == null) return;
        
        ShaderProgram.Uniform3f("dirLight.direction", dirLight.Direction);
        ShaderProgram.Uniform3f("dirLight.ambient", dirLight.Ambient);
        ShaderProgram.Uniform3f("dirLight.diffuse", dirLight.Diffuse);
        ShaderProgram.Uniform3f("dirLight.specular", dirLight.Specular);
    }
}