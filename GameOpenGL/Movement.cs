using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GameOpenGL;

public class Move : Component
{
    private readonly InputSystem _inputSystem;
    public readonly float Speed;
    
    private readonly Vector3 _front = Vector3.UnitZ;
    private readonly Vector3 _up = Vector3.UnitY;
    
    public Move(InputSystem inputSystem, float speed)
    {
        _inputSystem = inputSystem;
        Speed = speed;
    }

    public override void Update()
    {
        Vector3 lookDirection = Transform.Forward;
        
        Vector3 position = Transform.Position;
        if (_inputSystem.IsKeyDown(Keys.W))
        {
            position += lookDirection * Speed * Time.DeltaTime; //Forward 
        }
        if (_inputSystem.IsKeyDown(Keys.S))
        {
            position -= lookDirection * Speed * Time.DeltaTime; //Backwards
        }
        
        Vector3 right = Vector3.Normalize(Vector3.Cross(lookDirection, _up));
        if (_inputSystem.IsKeyDown(Keys.A))
        {
            position -= right * Speed * Time.DeltaTime; //Left
        }
        if (_inputSystem.IsKeyDown(Keys.D))
        {
            position += right * Speed * Time.DeltaTime; //Right
        }
        if (_inputSystem.IsKeyDown(Keys.Space))
        {
            position += _up * Speed * Time.DeltaTime; //Up 
        }
        if (_inputSystem.IsKeyDown(Keys.LeftShift))
        {
            position -= _up * Speed * Time.DeltaTime; //Down
        }
        
        Transform.Position = position;
    }
}