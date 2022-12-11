// using OpenTK.Graphics.OpenGL;
//
// namespace GameOpenGL
// {
//     public class ArrayObject : IDisposable
//     {
//         private const int ErrorCode = -1;
//
//         public int ArrayId { private set; get; } = 0;
//
//         private bool _active = false;
//
//         private readonly List<int> _attribsList;
//         private readonly List<BufferObject> _buffersList;
//
//         public ArrayObject()
//         {
//             _attribsList = new List<int>();
//             _buffersList = new List<BufferObject>();
//             ArrayId = GL.GenVertexArray();
//         }
//
//         public void Activate()
//         {
//             _active = true;
//             GL.BindVertexArray(ArrayId);
//         }
//
//         public void Deactivate()
//         {
//             _active = false;
//             GL.BindVertexArray(0);
//         }
//
//         public bool IsActive()
//         {
//             return _active;
//         }
//
//         public void AttachBuffer(BufferObject buffer)
//         {
//             if (IsActive() != true)
//                 Activate();
//
//             buffer.Activate();
//             _buffersList.Add(buffer);
//         }
//
//         public void AttribPointer(int index, int elementsPerVertex, AttribType type, int stride, int offset)
//         {
//             _attribsList.Add(index);
//             GL.EnableVertexAttribArray(index);
//             GL.VertexAttribPointer(index, elementsPerVertex, (VertexAttribPointerType)type, false, stride, offset);
//         }
//
//         public void Draw(int start, int count)
//         {
//             Activate();
//             GL.DrawArrays(PrimitiveType.Triangles, start, count);
//         }
//
//         public void DrawElements(int start, int count, ElementType type)
//         {
//             Activate();
//             GL.DrawElements(PrimitiveType.Triangles, count, (DrawElementsType) type, start);
//         }
//
//         public void DisableAttribAll()
//         {
//             foreach(int attrib in _attribsList)
//                 GL.DisableVertexAttribArray(attrib);
//         }
//
//         public void Delete()
//         {
//             if (ArrayId == ErrorCode)
//                 return;
//
//             Deactivate();
//             GL.DeleteVertexArray(ArrayId);
//
//             foreach (BufferObject buffer in _buffersList)
//                 buffer.Dispose();
//
//             ArrayId = ErrorCode;
//         }
//
//         public void Dispose()
//         {
//             Delete();
//             GC.SuppressFinalize(this);
//         }
//     }
// }
//
