using HallovEngine.Core;
using HallovEngine.Platform.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HallovEngine.Render
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

    public static partial class Rendering
    {
        public abstract class Shader
        {
            

            public abstract byte CompileShader();
            public abstract void SetVar(ShaderDataType type, string name, object var);
            

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
