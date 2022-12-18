using OpenTK.Mathematics;

namespace GameOpenGL;

public class LookMouse : Component
{
    private readonly InputSystem _inputSystem;
    private Vector2 _lastPos;
    
    private bool _firstMove = true;
    
    public float Sensitivity = 0.05f;
    
    private float _pitch;

    public float Pitch
    {
        get => MathHelper.RadiansToDegrees(_pitch);
        set
        {
            // We clamp the pitch value between -89 and 89 to prevent the camera from going upside down, and a bunch
            // of weird "bugs" when you are using euler angles for rotation.
            // If you want to read more about this you can try researching a topic called gimbal lock
            float angle = MathHelper.Clamp(value, -89f, 89f);
            _pitch = MathHelper.DegreesToRadians(angle);
            UpdateVectors();
        }
    }

    // Rotation around the Y axis (radians)
    private float _yaw = -MathHelper.PiOver2; // Without this, you would be started rotated 90 degrees right.
    
    public float Yaw
    {
        get => MathHelper.RadiansToDegrees(_yaw);
        set
        {
            _yaw = MathHelper.DegreesToRadians(value);
            UpdateVectors();
        }
    }
    
    private Vector3 _front;

    public LookMouse(InputSystem inputSystem)
    {
        _inputSystem = inputSystem;
    }

    private void UpdateVectors()
    {
        // First, the front matrix is calculated using some basic trigonometry.
        _front.X = MathF.Cos(_pitch) * MathF.Cos(_yaw);
        _front.Y = MathF.Sin(_pitch);
        _front.Z = MathF.Cos(_pitch) * MathF.Sin(_yaw);

        
        // We need to make sure the vectors are all normalized, as otherwise we would get some funky results.
        Transform.Forward = Vector3.Normalize(_front);
    }

    public override void Update()
    {
        Vector2 mouse = _inputSystem.MousePosition;
        
        if (_firstMove)
        {
            _lastPos = new Vector2(mouse.X, mouse.Y);
            _firstMove = false;
        }
        else
        {
            float deltaX = mouse.X - _lastPos.X;
            float deltaY = mouse.Y - _lastPos.Y;
            _lastPos = new Vector2(mouse.X, mouse.Y);
            // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
            Yaw += deltaX * Sensitivity;
            Pitch -= deltaY * Sensitivity; // Reversed since y-coordinates range from bottom to top
        }
    }
}