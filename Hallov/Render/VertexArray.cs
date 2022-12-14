using HallovEngine.Core;
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
                return Hallov.ProvideVertexArray();
                //return new OpenGL.GLVertexArray();
            }

            public abstract void AttribPointer(int index, int size, int type, bool normal, int stride, int offset);
            public abstract void EndConfig();

            public abstract void Draw(int count);
            public abstract void DrawIndexed(int lenght, uint type);
        }
    }
}
