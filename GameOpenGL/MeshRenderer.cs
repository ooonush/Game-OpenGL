// using GameOpenGL.Shaders;
// using OpenTK.Graphics.OpenGL;
//
// namespace GameOpenGL
// {
//     public class MeshRenderer : Component
//     {
//         public readonly Material Material;
//         public readonly BoxMesh Mesh;
//         
//         private readonly Camera _camera;
//         private readonly VertexArrayObject _vao;
//         public readonly ShaderProgram ShaderProgram;
//         
//         public MeshRenderer(Camera camera, Material material, BoxMesh mesh)
//         {
//             _camera = camera;
//             Material = material;
//             Mesh = mesh;
//             
//             _vao = new VertexArrayObject(Mesh.Vertices.ToArray());
//             _vao.BindVertexArray();
//             
//             string vertexShaderSource = File.ReadAllText("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Shaders/Source/shaderTexture.vert");
//             string fragmentShaderSource = File.ReadAllText("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Shaders/Source/shaderTexture.frag");
//             
//             ShaderProgram = new ShaderProgram(vertexShaderSource, fragmentShaderSource);
//         }
//         
//         public override void OnLoad()
//         {
//             ShaderProgram.Use();
//             Material.Texture.Use(TextureUnit.Texture0);
//         }
//         
//         public override void Render()
//         {
//             Shaders.ShaderProgram.UniformMatrix4f("model", true, Transform.GetModelMatrix());
//             Shaders.ShaderProgram.UniformMatrix4f("view", true, _camera.View);
//             Shaders.ShaderProgram.UniformMatrix4f("projection", true, _camera.Projection);
//             Shaders.ShaderProgram.Use();
//         
//             Mesh.VertexArrayObject.BindAndDrawArrays(PrimitiveType.Triangles);
//         }
//     }
// }