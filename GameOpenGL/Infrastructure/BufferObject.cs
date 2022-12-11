// using System.Runtime.InteropServices;
// using OpenTK.Graphics.OpenGL;
//
// namespace GameOpenGL
// {
//     public enum BufferType
//     {
//         ArrayBuffer = BufferTarget.ArrayBuffer,
//         ElementBuffer = BufferTarget.ElementArrayBuffer
//     }
//
//     public enum BufferHint
//     {
//         StaticDraw = BufferUsageHint.StaticDraw
//     }
//     
//     public sealed class BufferObject : IDisposable
//     {
//         private const int ErrorCode = -1;
//
//         public int BufferId { private set; get; } = 0;
//         private readonly BufferTarget _type;
//         private bool _active;
//
//         public BufferObject(BufferType type)
//         {
//             _type = (BufferTarget) type;
//             BufferId = GL.GenBuffer();
//         }
//
//         public void SetData<T>(T[] data, BufferHint hint)where T : struct
//         {
//             Activate();
//             GL.BufferData(_type, (nint)(data.Length * Marshal.SizeOf(typeof(T))), data, (BufferUsageHint) hint);
//         }
//
//         public void Activate()
//         {
//             _active = true;
//             GL.BindBuffer( _type, BufferId);
//         }
//
//         public void Deactivate()
//         {
//             _active = false;
//             GL.BindBuffer(_type, 0);
//         }
//
//         public bool IsActive()
//         {
//             return _active;
//         }
//
//         public void Delete()
//         {
//             if (BufferId == ErrorCode)
//                 return;
//
//             Deactivate();
//             GL.DeleteBuffer(BufferId);
//
//             BufferId = ErrorCode;
//         }
//
//         public void Dispose()
//         {
//             Delete();
//             GC.SuppressFinalize(this);
//         }
//     }
// }
