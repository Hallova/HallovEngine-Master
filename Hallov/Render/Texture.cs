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
        public abstract class Texture
        {
            public static Texture CreateFromFile(string path)
            {
                return OpenGL.GLTexture.LoadFromFile(path);
            }

            public abstract void Use(int unit);
        }
    }
}
