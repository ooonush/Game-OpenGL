using OpenTK.Mathematics;

namespace GameOpenGL;

public class PolygonCollider2D : Component
{
    private readonly PolygonMesh2D _mesh;
    
    public PolygonCollider2D(PolygonMesh2D mesh)
    {
        _mesh = mesh;
    }
    
    public override void LateUpdate()
    {

    }
}