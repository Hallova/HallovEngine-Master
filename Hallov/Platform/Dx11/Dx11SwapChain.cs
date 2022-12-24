using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using Vortice.Direct3D11;
using Vortice.DXGI;

namespace HallovEngine.Platform.Dx11
{
    public static partial class Dx11
    {
        internal class Dx11SwapChain
        {
            internal IDXGISwapChain m_swap_chain;
            internal Dx11GraphicsEngine engine;
            internal ID3D11RenderTargetView m_rtv;
            internal bool init(IntPtr hwnd, int w, int h)
            {
                SwapChainDescription desc = new();
                desc.BufferCount = 1;
                desc.BufferDescription.Width = w;
                desc.BufferDescription.Height = h;
                desc.BufferDescription.Format = Format.R8G8B8A8_UNorm;
                desc.BufferDescription.RefreshRate.Numerator = 60; // TODO(Hallov-a) : I need do change it :>
                desc.BufferDescription.RefreshRate.Denominator = 1;
                desc.BufferUsage = Usage.RenderTargetOutput;
                desc.OutputWindow = hwnd;
                desc.SampleDescription.Count = 1;
                desc.SampleDescription.Quality = 0;
                desc.Windowed = true;

                m_swap_chain = engine.m_dxgi_factory.CreateSwapChain(engine.m_d3d_device, desc);

                ID3D11Texture2D buffer;
                buffer = m_swap_chain.GetBuffer<ID3D11Texture2D>(0);
                m_rtv = engine.m_d3d_device.CreateRenderTargetView(buffer);
                buffer.Release();

                return true;
            }

            internal void present(int vsync)
            {
                m_swap_chain.Present(vsync);
            }

            internal bool release()
            {
                m_swap_chain.Release();
                return true;
            }
        }
    }
}
