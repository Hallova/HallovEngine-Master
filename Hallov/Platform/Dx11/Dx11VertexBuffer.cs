using HallovEngine.Core;
using HallovEngine.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortice.Direct3D11;

namespace HallovEngine.Platform.Dx11
{
    public static partial class Dx11
    {
        public class Dx11VertexBuffer : Rendering.VertexBuffer
        {
            public int m_size_vertex;
            public int m_size_list;

            public ID3D11Buffer m_buffer = null;
            public ID3D11InputLayout m_inputLayout = null;


            public override void Load(object list_vertices, int vertex_size, int vertices_size)
            {
                if(m_buffer != null)
                {
                    m_buffer.Release();
                    m_buffer.Dispose();
                    m_buffer = null;
                }

                BufferDescription buff_desc = new BufferDescription();
                buff_desc.Usage = ResourceUsage.Default;
                buff_desc.ByteWidth = vertex_size * vertices_size;
                buff_desc.BindFlags = BindFlags.VertexBuffer;
                buff_desc.CPUAccessFlags = 0;
                buff_desc.MiscFlags = 0;

                SubresourceData init_data = new();
                init_data.DataPointer = (IntPtr)list_vertices;

                m_size_list = vertices_size;
                m_size_vertex = vertex_size;

                var res = Dx11.GetGraphicsEngine().m_d3d_device.CreateBuffer(buff_desc, init_data, out m_buffer);
                if (res.Failure)
                {
                    throw new Exception("oh hell naw men");
                }
            }

            public override void Release()
            {
                
            }
        }
    }
}
