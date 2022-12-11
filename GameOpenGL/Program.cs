using System.Drawing;
using GameOpenGL;
using GameOpenGL.Shaders;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

var nativeWinSettings = new NativeWindowSettings()
{
    Size = new Vector2i(800, 600),
    // Location = new Vector2i(370, 300),
    // WindowBorder = WindowBorder.Resizable,
    // WindowState = WindowState.Normal,
    //
    // Flags = ContextFlags.Default,
    // APIVersion = new Version(3, 3),
    // Profile = ContextProfile.Compatability,
    // API = ContextAPI.OpenGL,
    //
    // NumberOfSamples = 0
};

using var game = new TestGame2(GameWindowSettings.Default, nativeWinSettings);

GameObject camera = game.Scene.CreateGameObject(new Transform(0, 0, 2));
camera.AddComponent<Camera>();

// GameObject polygonGameObject = game.Scene.CreateGameObject();
// polygonGameObject.Transform.Scale *= 0.2;
//
// PolygonMesh2D polygonMesh2D = polygonGameObject.AddComponent(new PolygonMesh2D(50, Color.Red));
// polygonGameObject.AddComponent(new Move(polygonMesh2D, new Vector3(0.5f, 0, 0)));



game.Run();