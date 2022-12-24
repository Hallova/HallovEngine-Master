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
        internal class Dx11DeviceContext
        {
            internal ID3D11DeviceContext m_device_context;
            internal Dx11DeviceContext(ID3D11DeviceContext device_context)
            {
                m_device_context = device_context;
            }

            internal void clearRenderTargetColor(Dx11.Dx11SwapChain swap_chain, float red, float green, float blue)
            {
                m_device_context.ClearRenderTargetView(swap_chain.m_rtv, new Vortice.Mathematics.Color4
                    (red, green, blue, 1.0f));
            }

            internal bool release()
            {
                m_device_context.Release();
                return false;
            }
        }
    }
}
