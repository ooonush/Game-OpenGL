// using GameOpenGL.Shaders;
// using OpenTK.Graphics.OpenGL;
// using OpenTK.Windowing.Common;
// using OpenTK.Windowing.Desktop;
//
// namespace GameOpenGL;
//
// public class TestGame2 : Game
// {
//     private readonly float[] _vertices = {
//         0.5f,  0.5f, 0.0f,  // top right
//         0.5f, -0.5f, 0.0f,  // bottom right
//         -0.5f, -0.5f, 0.0f,  // bottom left
//         -0.5f,  0.5f, 0.0f   // top left
//     };
//     
//     private readonly uint[] _indices = {  // note that we start from 0!
//         0, 1, 3,   // first triangle
//         1, 2, 3    // second triangle
//     };
//
//     private ShaderProgram? _shaderProgram;
//     private VertexArrayObject? _objectVAO;
//     private ElementsBufferObject? _elements;
//
//     public TestGame2(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) 
//         : base(gameWindowSettings, nativeWindowSettings) { }
//
//     protected override void OnLoad()
//     {
//         base.OnLoad();
//
//         _objectVAO = new VertexArrayObject(_vertices);
//         _elements = new ElementsBufferObject(_objectVAO, _indices);
//
//         string vertexShaderSource = File.ReadAllText("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Shaders/Source/shader.vert");
//         string fragmentShaderSource = File.ReadAllText("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Shaders/Source/shader.frag");
//         _shaderProgram = new ShaderProgram(vertexShaderSource, fragmentShaderSource);
//         _shaderProgram.Use();
//     }
//
//     protected override void OnRenderFrame(FrameEventArgs args)
//     {
//         base.OnRenderFrame(args);
//         _shaderProgram?.Use();
//         
//         //_objectVAO.DrawArrays(PrimitiveType.Triangles);
//         _elements?.BindVertexArrayAndDraw(PrimitiveType.Triangles);
//         
//         SwapBuffers();
//     }
//
//     protected override void OnUnload()
//     {
//         base.OnUnload();
//         
//         _shaderProgram?.Dispose();
//     }
// }