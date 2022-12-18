using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GameOpenGL;

public class InputSystem
{
    private readonly Game _game;

    public bool IsFocused => _game.IsFocused;
    
    public Vector2 MousePosition
    {
        get => _game.MousePosition;
        set => _game.MousePosition = value;
    }

    public InputSystem(Game game)
    {
        _game = game;
    }

    public bool IsKeyDown(Keys key) => _game.IsKeyDown(key);
}