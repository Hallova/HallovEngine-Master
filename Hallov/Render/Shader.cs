using HallovEngine.Core;
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
            public enum ShaderDataType : uint
            {
                Int,
                Float,

                Float2,
                Float3,
                Float4,

                Mat4,

                Sampler2D,

                Undefied,
            }

            public abstract byte CompileShader();

            
            public static Shader New(string frag, string vert)
            {
                return Hallov.ProvideShaderFromText(frag, vert);
            }
            public static ShaderDataType GetShaderDataType(Type type)
            {
                if(type == typeof(int))
                {
                    return ShaderDataType.Int;
                }

                return ShaderDataType.Undefied;
            }


            public abstract void Use();
        }
    }
}
