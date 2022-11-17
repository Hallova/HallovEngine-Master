using HallovEngine.Platform.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HallovEngine.Render
{
    public static partial class Rendering
    {
        public abstract class Shader
        {
            public static Shader New(string frag, string vert)
            {
                return new OpenGL.GLShader(vert, frag);
            }

            public abstract void Use();
        }
    }
}
