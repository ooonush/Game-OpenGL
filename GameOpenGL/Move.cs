using OpenTK.Mathematics;

namespace GameOpenGL;

public class Move : Component
{
    public readonly PolygonMesh2D Mesh;
    public Vector3 Velocity { get; private set; }

    public Move(PolygonMesh2D mesh, Vector3 velocity)
    {
        Mesh = mesh;
        Velocity = velocity;
    }

    public override void Update()
    {
        foreach (Vector3 point in Mesh.Points)
        {
            if (point.X <= -1 && Velocity.X < 0) Velocity = new Vector3(-Velocity.X, Velocity.Y, Velocity.Z);
            if (point.Y <= -1 && Velocity.Y < 0) Velocity = new Vector3(Velocity.X, -Velocity.Y, Velocity.Z);

            if (point.X >= 1 && Velocity.X > 0) Velocity = new Vector3(-Velocity.X, Velocity.Y, Velocity.Z);
            if (point.Y >= 1 && Velocity.Y > 0) Velocity = new Vector3(Velocity.X, -Velocity.Y, Velocity.Z);
        }

        Mesh.Transform.Position += Velocity * DeltaTime;
    }
}