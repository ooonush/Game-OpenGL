// using System.Diagnostics;
// using GameOpenGL.Shaders;
// using OpenTK.Graphics.OpenGL;
// using OpenTK.Mathematics;
// using OpenTK.Windowing.Common;
// using OpenTK.Windowing.Desktop;
//
// namespace GameOpenGL;
//
// public class GameTextures : Game
// {
//     private readonly Stopwatch _timer = new();
//
//     private readonly float[] _vertices =
//     {
//         -0.5f, -0.5f, -0.5f, 0.0f, 0.0f,
//         0.5f, -0.5f, -0.5f, 1.0f, 0.0f,
//         0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
//         0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
//         -0.5f, 0.5f, -0.5f, 0.0f, 1.0f,
//         -0.5f, -0.5f, -0.5f, 0.0f, 0.0f,
//
//         -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
//         0.5f, -0.5f, 0.5f, 1.0f, 0.0f,
//         0.5f, 0.5f, 0.5f, 1.0f, 1.0f,
//         0.5f, 0.5f, 0.5f, 1.0f, 1.0f,
//         -0.5f, 0.5f, 0.5f, 0.0f, 1.0f,
//         -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
//
//         -0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
//         -0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
//         -0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
//         -0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
//         -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
//         -0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
//
//         0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
//         0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
//         0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
//         0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
//         0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
//         0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
//
//         -0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
//         0.5f, -0.5f, -0.5f, 1.0f, 1.0f,
//         0.5f, -0.5f, 0.5f, 1.0f, 0.0f,
//         0.5f, -0.5f, 0.5f, 1.0f, 0.0f,
//         -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
//         -0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
//
//         -0.5f, 0.5f, -0.5f, 0.0f, 1.0f,
//         0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
//         0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
//         0.5f, 0.5f, 0.5f, 1.0f, 0.0f,
//         -0.5f, 0.5f, 0.5f, 0.0f, 0.0f,
//         -0.5f, 0.5f, -0.5f, 0.0f, 1.0f
//     };
//     
//     private readonly uint[] _indices =
//     {
//         0, 1, 3,
//         1, 2, 3
//     };
//
//     private ShaderProgram? _shaderProgram;
//     private Texture? _texture;
//     private Matrix4 _model;
//     private Matrix4 _view;
//     private Matrix4 _projection;
//     private double _time = 0;
//     private VertexArrayObject _vao;
//
//     public GameTextures(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) 
//         : base(gameWindowSettings, nativeWindowSettings) { }
//
//     protected override void OnLoad()
//     {
//         base.OnLoad();
//         
//         GL.Enable(EnableCap.DepthTest);
//
//         _timer.Start();
//
//         _vao = new VertexArrayObject(_vertices);
//         _vao.BindVertexArray();
//         
//
//         GL.BindBuffer(BufferTargetARB.ArrayBuffer, _vao.VertexBufferObject);
//         
//         string vertexShaderSource = File.ReadAllText("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Shaders/Source/shaderTexture.vert");
//         string fragmentShaderSource = File.ReadAllText("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Shaders/Source/shaderTexture.frag");
//         _shaderProgram = new ShaderProgram(vertexShaderSource, fragmentShaderSource);
//         _shaderProgram.Use();
//         
//         
//         // Note that we're translating the scene in the reverse direction of where we want to move.
//         _view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
//         _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), (float)Size.X / Size.Y, 0.1f, 100.0f);
//         
//         // Because there's now 5 floats between the start of the first vertex and the start of the second,
//         // we modify the stride from 3 * sizeof(float) to 5 * sizeof(float).
//         // This will now pass the new vertex array to the buffer.
//         const uint vertexLocation = 0;
//         GL.EnableVertexAttribArray(vertexLocation);
//         GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
//         
//         // Next, we also setup texture coordinates. It works in much the same way.
//         // We add an offset of 3, since the texture coordinates comes after the position data.
//         // We also change the amount of data to 2 because there's only 2 floats for texture coordinates.
//         const uint texCoordLocation = 1;
//         GL.EnableVertexAttribArray(texCoordLocation);
//         GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
//         
//         _texture = Texture.LoadFromFile("C:/Users/ooonu/RiderProjects/ConsoleApp1/GameOpenGL/Textures/image.jpg");
//         _texture.Use(TextureUnit.Texture0);
//     }
//
//     protected override void OnRenderFrame(FrameEventArgs args)
//     {
//         base.OnRenderFrame(args);
//         
//         GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
//
//         _vao.BindVertexArray();
//         
//         _texture.Use(TextureUnit.Texture0);
//         _shaderProgram?.Use();
//         
//         _time += 4.0 * args.Time;
//         _model = Matrix4.Identity * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(_time));
//
//         _shaderProgram?.SetMatrix4("model", true, _model);
//         _shaderProgram?.SetMatrix4("view", true, _view);
//         _shaderProgram?.SetMatrix4("projection", true, _projection);
//         
//         _vao.BindAndDrawArrays(PrimitiveType.Triangles);
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