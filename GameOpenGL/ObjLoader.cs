using System.Globalization;
using OpenTK.Mathematics;

namespace ObjRenderer
{
	public static class ObjLoader
	{
		public static Mesh Load(string path)
		{
			if (!File.Exists(path))
			{
				throw new FileNotFoundException("Unable to open \"" + path + "\", does not exist.");
			}
			
			var mesh = new Mesh();

			using(var streamReader = new StreamReader(path))
			{
				while(!streamReader.EndOfStream)
				{
					var words = new List<string>(streamReader.ReadLine()?.ToLower().Split(' ') ?? Array.Empty<string>());
					words.RemoveAll(s => s == string.Empty);
					
					if(words.Count == 0)
						continue;
					
					string type = words[0];
					words.RemoveAt(0);
					
					switch(type)
					{
						// vertex
						case "v":
							mesh.vertices.Add(new Vector4(
								ParseFloat(words[0]), 
								ParseFloat(words[1]),
								ParseFloat(words[2]), 
								words.Count < 4 ? 1 : ParseFloat(words[3])));
							break;

						case "vt":
							mesh.textureVertices.Add(new Vector3(
								ParseFloat(words[0]), 
								ParseFloat(words[1]),
								words.Count < 3 ? 0 : ParseFloat(words[2])));
							break;

						case "vn":
							mesh.normals.Add(new Vector3(
								ParseFloat(words[0]), 
								ParseFloat(words[1]), 
								ParseFloat(words[2])));
							break;
						
						// face
						case "f":
							foreach(string w in words)
							{
								string[] comps = w.Split('/');
								
								// subtract 1: indices start from 1, not 0
								mesh.vertexIndices.Add(uint.Parse(comps[0]) - 1);
								
								if(comps.Length > 1 && comps[1].Length != 0)
									mesh.textureIndices.Add(uint.Parse(comps[1]) - 1);
								else
								{
									mesh.textureIndices.Add(0);
								}
								if(comps.Length > 2)
									mesh.normalIndices.Add(uint.Parse(comps[2]) - 1);
								else
								{
									mesh.normalIndices.Add(0);
								}
							}
							
							int startIndex = mesh.vertexIndices.Count - words.Count;
							Vector4 vertex1 = mesh.vertices[(int)mesh.vertexIndices[startIndex]];
							Vector4 vertex2 = mesh.vertices[(int)mesh.vertexIndices[startIndex + 1]];
							Vector4 vertex3 = mesh.vertices[(int)mesh.vertexIndices[startIndex + 2]];
							mesh.verts.Add(vertex1.Xyz);
							mesh.verts.Add(vertex2.Xyz);
							mesh.verts.Add(vertex3.Xyz);
							
							Vector3 normal1 = mesh.normals[(int)mesh.normalIndices[startIndex]];
							Vector3 normal2 = mesh.normals[(int)mesh.normalIndices[startIndex + 1]];
							Vector3 normal3 = mesh.normals[(int)mesh.normalIndices[startIndex + 2]];
							mesh.norms.Add(normal1);
							mesh.norms.Add(normal2);
							mesh.norms.Add(normal3);
							
							Vector3 textCoord1 = mesh.textureVertices[(int)mesh.textureIndices[startIndex]];
							Vector3 textCoord2 = mesh.textureVertices[(int)mesh.textureIndices[startIndex + 1]];
							Vector3 textCoord3 = mesh.textureVertices[(int)mesh.textureIndices[startIndex + 2]];
							mesh.textCoords.Add(textCoord1);
							mesh.textCoords.Add(textCoord2);
							mesh.textCoords.Add(textCoord3);
							
							for (var j = 3; j < words.Count; j++) // if count > 3
							{
								Vector4 vertex4 = mesh.vertices[(int)mesh.vertexIndices[startIndex + j]];
								mesh.verts.Add(vertex1.Xyz);
								mesh.verts.Add(vertex3.Xyz);
								mesh.verts.Add(vertex4.Xyz);
							
								Vector3 normal4 = mesh.normals[(int)mesh.normalIndices[startIndex + j]];
								mesh.norms.Add(normal1);
								mesh.norms.Add(normal3);
								mesh.norms.Add(normal4);
							
								Vector3 textCoord4 = mesh.textureVertices[(int)mesh.textureIndices[startIndex + j]];
								mesh.textCoords.Add(textCoord1);
								mesh.textCoords.Add(textCoord3);
								mesh.textCoords.Add(textCoord4);
							}

							break;
					}
				}
			}

			return mesh;
		}

		private static float ParseFloat(string text)
		{
			return float.Parse(text, NumberStyles.Any, CultureInfo.InvariantCulture);
		}
	}
}