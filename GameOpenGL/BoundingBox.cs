using OpenTK.Mathematics;

namespace GameOpenGL
{
	public class BoundingBox
	{
		public static readonly Color4<Rgba> DrawColor = new(1f, 0f, 0f, 1f);

		// order: left-bottom-back, right-bottom-back, left-top-back, right-top-back
		//		  left-bottom-front, right-bottom-front, left-top-front, right-top-front
		// using OpenGL coordinates: x -> right, y -> up, z -> out of screen
		public readonly List<Vector4> Vertices;
		public readonly List<uint> Indices;

		public readonly Vector4 Center;

		public float Length => Vertices[7].Z - Vertices[0].Z;

		public float Width => Vertices[7].X - Vertices[0].X;

		public float Height => Vertices[7].Y - Vertices[0].Y;

		public BoundingBox(Vector4 min, Vector4 max)
		{
			// 12 pairs of vertices to make a cube
			Vertices = new List<Vector4>(new Vector4[8]);
			Indices = new List<uint>(24)
			{
				// from left-bottom-back
				0, 1,
				0, 2,
				0, 4,
				// from right-top-front
				7, 3,
				7, 6,
				7, 5,
				// from right-bottom-back
				1, 5,
				1, 3,
				// from left-top-back
				2, 3,
				2, 6,
				// from left-bottom-front
				4, 5,
				4, 6,
			};

			Vector4.Subtract(in max, in min, out Vector4 diag);

			var xd = new Vector4(diag.X, 0, 0, 1);
			var yd = new Vector4(0, diag.Y, 0, 1);
			var zd = new Vector4(0, 0, diag.Z, 1);

			Vertices[0] = min;

			Vector4.Add(in min, in xd, out Vector4 temp);
			Vertices[1] = temp;

            Vector4.Add(in min, in yd, out temp);
			Vertices[2] = temp;

            Vector4.Subtract(in max, in zd, out temp);
			Vertices[3] = temp;

            Vector4.Add(in min, in zd, out temp);
			Vertices[4] = temp;

            Vector4.Subtract(in max, in yd, out temp);
			Vertices[5] = temp;

            Vector4.Subtract(in max, in xd, out temp);
			Vertices[6] = temp;

			Vertices[7] = max;
			
			Vector4.Divide(in diag, 2, out temp);
            Vector4.Add(in min, in temp, out Center);
		}
	}
}