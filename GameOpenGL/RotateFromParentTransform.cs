using OpenTK.Mathematics;

namespace GameOpenGL;

public class RotateFromParentTransform : Component
{
    public override void Update()
    {
        Vector3 local = Transform.LocalPosition;
        
        if (local.X == 0 || local.Y == 0)
        {
            Transform.LocalPosition += new Vector3(local.Y, local.X, 0).Normalized() * Time.DeltaTime * 5;
            return;
        }

        var x = 1;
        if (local.Y < 0)
        {
            x = -1;
        }
        
        float y = - (x * local.X) / local.Y;
        
        Transform.LocalPosition += new Vector3(x, y, 0).Normalized() * Time.DeltaTime * 5;
    }
}