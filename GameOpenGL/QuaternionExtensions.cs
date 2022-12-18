using OpenTK.Mathematics;

namespace GameOpenGL;

public static class Vector3Helper
{
    public static float Angle(Vector3 from, Vector3 to)
    {
        var num = (float) Math.Sqrt(from.SqrMagnitude() * (double) to.SqrMagnitude());
        return num < 1.0000000036274937E-15 ? 0.0f : (float) Math.Acos(Math.Clamp(Vector3.Dot(from, to) / num, -1f, 1f)) * 57.29578f;
    }

    public static float SqrMagnitude(this Vector3 vector3)
    {
        return (float) (vector3.X * (double) vector3.X + vector3.Y * (double) vector3.Y + vector3.Z * (double) vector3.Z);
    }
}

public static class QuaternionExtensions
{
    public static Quaternion FromToRotation(Vector3 aFrom, Vector3 aTo)
    {
        Vector3 axis = Vector3.Cross(aFrom, aTo);
        float angle = Vector3Helper.Angle(aFrom, aTo);
        return Quaternion.FromAxisAngle(axis, angle);
    }

    public static void LookRotation(this Quaternion quaternion, Vector3 forward, Vector3 up)
    {
        Quaternion newQuaternion = LookRotation(forward, up);
        quaternion.X = newQuaternion.X;
        quaternion.Y = newQuaternion.Y;
        quaternion.Z = newQuaternion.Z;
        quaternion.W = newQuaternion.W;
    }
    
    public static Quaternion LookRotation(Vector3 forward)
    {
        var quaternion = new Quaternion();
        quaternion.LookRotation(forward, Vector3.UnitY);
        return quaternion;
    }

    public static Quaternion LookRotation(Vector3 forward, Vector3 up)
    {
        forward.Normalize();
 
        Vector3 vector = Vector3.Normalize(forward);
        Vector3 vector2 = Vector3.Normalize(Vector3.Cross(up, vector));
        Vector3 vector3 = Vector3.Cross(vector, vector2);
        float m00 = vector2.X;
        float m01 = vector2.Y;
        float m02 = vector2.Z;
        float m10 = vector3.X;
        float m11 = vector3.Y;
        float m12 = vector3.Z;
        float m20 = vector.X;
        float m21 = vector.Y;
        float m22 = vector.Z;

        float num8 = (m00 + m11) + m22;
        var quaternion = new Quaternion();
        if (num8 > 0f)
        {
            var num = (float)Math.Sqrt(num8 + 1f);
            quaternion.W = num * 0.5f;
            num = 0.5f / num;
            quaternion.X = (m12 - m21) * num;
            quaternion.Y = (m20 - m02) * num;
            quaternion.Z = (m01 - m10) * num;
            return quaternion;
        }
        if ((m00 >= m11) && (m00 >= m22))
        {
            var num7 = (float)Math.Sqrt(((1f + m00) - m11) - m22);
            float num4 = 0.5f / num7;
            quaternion.X = 0.5f * num7;
            quaternion.Y = (m01 + m10) * num4;
            quaternion.Z = (m02 + m20) * num4;
            quaternion.W = (m12 - m21) * num4;
            return quaternion;
        }
        if (m11 > m22)
        {
            var num6 = (float)Math.Sqrt(((1f + m11) - m00) - m22);
            float num3 = 0.5f / num6;
            quaternion.X = (m10+ m01) * num3;
            quaternion.Y = 0.5f * num6;
            quaternion.Z = (m21 + m12) * num3;
            quaternion.W = (m20 - m02) * num3;
            return quaternion; 
        }
        var num5 = (float)Math.Sqrt(((1f + m22) - m00) - m11);
        float num2 = 0.5f / num5;
        quaternion.X = (m20 + m02) * num2;
        quaternion.Y = (m21 + m12) * num2;
        quaternion.Z = 0.5f * num5;
        quaternion.W = (m01 - m10) * num2;

        return quaternion;
    }
}