using OpenTK.Mathematics;

namespace GameOpenGL;

public class Vertex
{
    public Vector3 Coordinates;
    public Vector2 TextureCoordinates;
    
    public Vertex(float x, float y, float z, float textX, float textY)
    {
        Coordinates = new Vector3(x, y, z);
        TextureCoordinates = new Vector2(textX, textY);
    }
}