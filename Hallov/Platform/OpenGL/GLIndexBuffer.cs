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
        public sealed class GLIndexBuffer : Rendering.IndexBuffer
        {
            int program;

            public GLIndexBuffer()
            {
                program = GL.GenBuffer();
                HV_LOG(false, "IndexBuffer Creted : " + program, this.GetType());
            }

            public override void BindBuffer()
            {
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, program);
            }

            public override void BufferData(int size, IntPtr point, uint type)
            {
                throw new NotImplementedException();
            }

            public override void BufferData(int size, uint[] point, uint type)
            {
                GL.BufferData(BufferTarget.ElementArrayBuffer, size, point, BufferUsageHint.StaticDraw);
                //GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);
            }
        }
    }
}
