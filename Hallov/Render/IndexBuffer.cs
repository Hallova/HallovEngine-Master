using HallovEngine.Platform.OpenGL;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallovEngine.Render
{
    public static partial class Rendering
    {
        public abstract class IndexBuffer
        {
            

            public static IndexBuffer New()
            {
                return new OpenGL.GLIndexBuffer();
            }

            public abstract void BindBuffer();

            public abstract void BufferData(int size, IntPtr point, uint type);
            public abstract void BufferData(int size, uint[] point, uint type);
        }
    }
}
