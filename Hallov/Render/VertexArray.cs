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
        public abstract class VertexArray
        {
            public static VertexArray New()
            {
                return new OpenGL.GLVertexArray();
            }

            public abstract void AttribPointer(int index, int size, int type, bool normal, int stride, int offset);

            public abstract void Draw(int count);
        }
    }
}
