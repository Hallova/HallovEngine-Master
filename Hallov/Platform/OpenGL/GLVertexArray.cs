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
        public class GLVertexArray : Rendering.VertexArray
        {
            int array;
            public GLVertexArray()
            {
                array = GL.GenVertexArray();
                GL.BindVertexArray(array);
            }

            public override void AttribPointer(int index, int size, int type, bool normal, int stride, int offset)
            {
                GL.VertexAttribPointer(index, size, (VertexAttribPointerType)type, normal, stride, offset);
                //GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
                
            }

            public override void Draw(int count)
            {
                GL.BindVertexArray(array);
                GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            }

            public override void EndConfig()
            {
                GL.EnableVertexAttribArray(0);
            }
        }
    }
}
