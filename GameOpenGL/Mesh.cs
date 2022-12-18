using System.Collections.Generic;
using GameOpenGL;
using OpenTK;
using OpenTK.Mathematics;

namespace ObjRenderer
{
    public class Mesh
    {
        public readonly List<Vector3> verts = new List<Vector3>();
        public readonly List<Vector3> norms = new();
        public readonly List<Vector3> textCoords = new();
        
        public readonly List<Vector4> vertices = new();
        public readonly List<Vector3> textureVertices = new();
        public readonly List<Vector3> normals = new();
        public readonly List<uint> vertexIndices = new();
        public readonly List<uint> textureIndices = new();
        public readonly List<uint> normalIndices = new();
        
        public Mesh()
        {
            
        }
        
        public Mesh(List<Vector4> vertices, List<Vector3> textureVertices, List<Vector3> normals,
            List<uint> vertexIndices, List<uint> textureIndices, List<uint> normalIndices, List<Vector3> verts, List<Vector3> norms)
        {
            this.vertices = vertices;
            this.textureVertices = textureVertices;
            this.normals = normals;
            this.vertexIndices = vertexIndices;
            this.textureIndices = textureIndices;
            this.normalIndices = normalIndices;
            this.verts = verts;
            this.norms = norms;
        }
    }
}