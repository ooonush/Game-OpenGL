using OpenTK.Mathematics;

namespace GameOpenGL;

public abstract class Animated<T> : Component
{
    public readonly Func<float, T> Func;
    public float Scale;

    protected Animated(Func<float, T> func, float scale = 1)
    {
        Func = func;
        Scale = scale;
    }
}

public class AnimatedRotation : Animated<Quaternion>
{
    public AnimatedRotation(Func<float, Quaternion> func, float scale = 1) : base(func, scale) { }
    
    public override void Update()
    {
        Transform.Rotation = Func(Time.TimeInSeconds);
    }
}

public class AnimatedScale : Animated<float>
{
    public AnimatedScale(Func<float, float> func, float scale = 1) : base(func, scale) { }

    public override void Update()
    {
        Transform.LocalScale = new Vector3(Func(Time.TimeInSeconds * Scale));
    }
}

public class AnimatedMove : Animated<Vector3>
{
    private Vector3 _startPosition;

    public AnimatedMove(Func<float, Vector3> func, float scale = 1) : base(func, scale) { }

    public override void OnLoad()
    {
        _startPosition = Transform.Position;
    }

    public override void Update()
    {
        Transform.LocalPosition = _startPosition + Func(Time.TimeInSeconds * Scale);
    }
}