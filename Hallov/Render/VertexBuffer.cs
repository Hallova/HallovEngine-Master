
using HallovEngine.Core;
using HallovEngine.Platform.OpenGL;

namespace HallovEngine.Render
{
    public static partial class Rendering
    {
        public abstract class VertexBuffer
        {
            public static VertexBuffer New()
            {
                return Hallov.ProvideVertexBuffer();
            }

            public abstract void BindBuffer();

            public abstract void BufferData(int size, IntPtr point, uint type);
            public abstract void BufferData(int size, float[] point, uint type);
            //public abstract void BufferData(int size, IntPtr point, uint type);


        }
    }
}
