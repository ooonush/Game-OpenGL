using OpenTK.Mathematics;

namespace GameOpenGL;

public class Transform : Component
{
    public Transform? Parent;
    
    public Vector3 LocalScale = Vector3.One;
    public Vector3 LocalPosition = Vector3.Zero;
    public Quaternion Rotation = Quaternion.Identity;

    public Vector3 Scale
    {
        get
        {
            if (Parent != null) return LocalScale * Parent.Scale;
            return LocalScale;
        }
        set
        {
            if (Parent != null) LocalScale = value / Parent.Scale ;
            else LocalScale = value;
        }
    }

    public Vector3 Position
    {
        get
        {
            if (Parent != null) return LocalPosition * Parent.Scale + Parent.Position;
            return LocalPosition;
        }
        set
        {
            if (Parent != null) LocalPosition = (value - Parent.Position) / Parent.Scale;
            else LocalPosition = value;
        }
    }

    public Vector3 Right
    {
        get => Rotation * Vector3.UnitX;
        set => Rotation = QuaternionExtensions.FromToRotation(Vector3.UnitX, value);
    }
    
    public Vector3 Up
    {
        get => Rotation * Vector3.UnitY;
        set => Rotation = QuaternionExtensions.FromToRotation(Vector3.UnitY, value);
    }
    
    public Vector3 Forward
    {
        get => Rotation * Vector3.UnitZ;
        set => Rotation = QuaternionExtensions.LookRotation(value, Vector3.UnitY);
    }
    
    public Transform() { }
    
    public Transform(float x, float y, float z) : this(new Vector3(x, y, z)) { }
    
    public Transform(Vector3 position)
    {
        Position = position;
    }
    
    public Matrix4 GetModelMatrix()
    {
        var scale = Matrix4.CreateScale(Transform.Scale);
        var rotation = Matrix4.CreateFromQuaternion(Transform.Rotation);
        var translation = Matrix4.CreateTranslation(Transform.Position);
        
        return scale * rotation * translation;
    }
}