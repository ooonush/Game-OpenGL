using GameOpenGL.Shaders;
using OpenTK.Graphics.OpenGL;

namespace GameOpenGL
{
    public class MeshRenderer : Component
    {
        public readonly Material Material;
        public readonly Mesh Mesh;
        
        private readonly Camera _camera;
        private readonly VertexArrayObject _vao;
        private readonly ShaderProgram _shaderProgram;
        
        public MeshRenderer(Camera camera, Material material, Mesh mesh)
        {
            _camera = camera;
            Material = material;
            Mesh = mesh;
            
            var vertices = new List<float>();
            
            foreach (Vertex vertex in Mesh.Vertices)
            {
                vertices.Add(vertex.Coordinates.X);
                vertices.Add(vertex.Coordinates.Y);
                vertices.Add(vertex.Coordinates.Z);
                vertices.Add(vertex.TextureCoordinates.X);
                vertices.Add(vertex.TextureCoordinates.Y);
            }
            
            _vao = new VertexArrayObject(vertices.ToArray());
            _vao.BindVertexArray();
            
            string vertexShaderSource = File.ReadAllText("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Shaders/Source/shaderTexture.vert");
            string fragmentShaderSource = File.ReadAllText("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Shaders/Source/shaderTexture.frag");
            
            _shaderProgram = new ShaderProgram(vertexShaderSource, fragmentShaderSource);
        }
        
        public override void OnLoad()
        {
            _shaderProgram.Use();
            Material.Texture.Use(TextureUnit.Texture0);
        }
        
        public override void Render()
        {
            _vao.BindVertexArray();
            
            Material.Texture.Use(TextureUnit.Texture0);
            _shaderProgram.Use();
            
            _shaderProgram.SetMatrix4("model", true, Transform.GetModelMatrix());
            _shaderProgram.SetMatrix4("view", true, _camera.View);
            _shaderProgram.SetMatrix4("projection", true, _camera.Projection);
            
            _vao.BindAndDrawArrays(PrimitiveType.Triangles);
        }
    }
}