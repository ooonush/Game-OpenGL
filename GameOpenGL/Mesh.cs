using OpenTK.Mathematics;

namespace GameOpenGL;

public class Mesh
{
    public readonly Vertex[] Vertices;
    
    public Mesh(Vertex[] vertices)
    {
        Vertices = vertices;
    }
    
    public Mesh(IReadOnlyList<float> vertices)
    {
        Vertices = new Vertex[vertices.Count / 5];
        for (var i = 0; i < Vertices.Length; i++)
        {
            int index = i * 5;
            Vertices[i] = new Vertex(
                vertices[index], 
                vertices[index + 1], 
                vertices[index + 2], 
                vertices[index + 3], 
                vertices[index + 4]);
        }
    }
}