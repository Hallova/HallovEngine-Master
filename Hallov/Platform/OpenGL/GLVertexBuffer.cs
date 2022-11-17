using HallovEngine.Render;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallovEngine.Platform.OpenGL
{
    public static partial class OpenGL
    {
        public sealed class GLVertexBuffer : Rendering.VertexBuffer
        {
            int program;

            public GLVertexBuffer()
            {
                program = GL.GenBuffer();
            }
            public override void BindBuffer()
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, program);
            }

            public override void BufferData(int size, IntPtr point, uint type)
            {
                GL.BufferData(BufferTarget.ArrayBuffer, size, point, BufferUsageHint.StaticDraw);
            }

            public override void BufferData(int size, float[] point, uint type)
            {
                GL.BufferData(BufferTarget.ArrayBuffer, size, point, BufferUsageHint.StaticDraw);
            }
        }
    }
}
