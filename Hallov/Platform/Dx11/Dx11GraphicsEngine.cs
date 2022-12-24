using HallovEngine.Render;
using SharpGen.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortice.Direct3D;
using Vortice.Direct3D11;
using Vortice.DXGI;


using System.Runtime.InteropServices;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace HallovEngine.Platform.Dx11
{
    public static partial class Dx11
    {
        internal static Dx11GraphicsEngine GetGraphicsEngine()
        {
            return (Dx11GraphicsEngine)GraphicsEngine.engine;
        }

        internal static void Init(out ID3D11Device m_d3d_device, out FeatureLevel m_feature_level, out ID3D11DeviceContext m_imn_context,
            out IDXGIDevice m_dxgi_device, out IDXGIAdapter m_dxgi_adapter, out IDXGIFactory m_dxgi_factory)
        {
            DriverType[] driver_types =
            {
                DriverType.Hardware,
                DriverType.Warp,
                DriverType.Reference
            };
            

            int num_driver_types = driver_types.Length;

            FeatureLevel[] feature_levels =
            {
                FeatureLevel.Level_11_0
            };

            int num_feature_levels = feature_levels.Length;

            
            {
                // I had to do this UNCANY code
                // please, forgive me for next three lines :<
                m_d3d_device = new ID3D11Device(IntPtr.Zero);
                m_feature_level = FeatureLevel.Level_9_1;
                m_imn_context = new ID3D11DeviceContext(IntPtr.Zero);
            }
            
            


            Result res = Result.Ok;
            for (int driver_type_index = 0; driver_type_index < num_driver_types;)
            {
                 res = D3D11.D3D11CreateDevice(null, driver_types[driver_type_index],
                    DeviceCreationFlags.None, feature_levels,  out m_d3d_device, out m_feature_level, out m_imn_context);
                if (res.Success)
                {
                    HV_LOG_INFO(false, $"The DriverType '{driver_types[driver_type_index].ToString()}' is Correct");
                    break;
                }
                    


                HV_LOG_INFO(false, $"The DriverType {driver_types[driver_type_index].ToString()} is incorrect");

                ++driver_type_index;
            }
            

            m_dxgi_device = m_d3d_device.QueryInterface<IDXGIDevice>();
            m_dxgi_adapter = m_dxgi_device.GetParent<IDXGIAdapter>();
            m_dxgi_factory = m_dxgi_adapter.GetParent<IDXGIFactory>();

            if (res.Failure)
            {
                return;
            }
        }

        public unsafe class Dx11GraphicsEngine : GraphicsEngine
        {
            internal Window* i_Window;

            public override bool IsRunning => !GLFW.WindowShouldClose(i_Window);

            private GLFWCallbacks.FramebufferSizeCallback resizeCallback;

            //DXstaff
            public ID3D11Device m_d3d_device; 
            public FeatureLevel m_feature_level; 
            public ID3D11DeviceContext m_imn_context;

            public IDXGIDevice m_dxgi_device;
            public IDXGIAdapter m_dxgi_adapter;
            public IDXGIFactory m_dxgi_factory;

            public IntPtr hwnd;


            internal Dx11.Dx11SwapChain m_swap_chain;
            internal Dx11.Dx11DeviceContext m_imm_device_context;

            // don't rember a time when i writed that comment :

            // later
            // I genuinely don't know how GLFW this might be the wrong function name.

            public override uint Init()
            {
                //GLFW staff
                {
                    GLFW.Init();
                    GLFW.WindowHint(WindowHintInt.ContextVersionMajor, 3);
                    GLFW.WindowHint(WindowHintInt.ContextVersionMinor, 3);
                    //GLFW.WindowHint(WindowHintOpenGlProfile.OpenGlProfile, OpenGlProfile.Core);
                    GLFW.WindowHint(WindowHintClientApi.ClientApi, ClientApi.NoApi);
                    //GLLoader.LoadBindings(new GLFWBindingsContext());

                    resizeCallback = framebuffer_size_callback;

                    i_Window = GLFW.CreateWindow(800, 600, "hi", null, null);

                    if (i_Window is null)
                    {
                        GLFW.DestroyWindow(i_Window);
                    }

                    GLFW.SetFramebufferSizeCallback(i_Window, resizeCallback);
                    

                    GLFW.MakeContextCurrent(i_Window);

                    GLFW.SwapInterval(0);

                    hwnd = GLFW.GetWin32Window(i_Window);
                }

                //DX11 staff
                {
                    Dx11.Init(out m_d3d_device, out m_feature_level, out m_imn_context,
                    out m_dxgi_device, out m_dxgi_adapter, out m_dxgi_factory);

                    m_swap_chain = new Dx11SwapChain();
                    m_swap_chain.engine = this;
                    GLFW.GetWindowSize(i_Window, out int w, out int h);

                    m_swap_chain.init(hwnd, w, h);

                    m_imm_device_context = new Dx11DeviceContext(m_imn_context);
                }
           
                return 0;
            }

            public void CreateSwapChainAndDevice()
            {

            }

            #region GLFW_Callbacks
            void framebuffer_size_callback(Window* window, int width, int height)
            {
                // make sure the viewport matches the new window dimensions; note that width and 
                // height will be significantly larger than specified on retina displays.
                //GL.Viewport(0, 0, width, height);
                GC.Collect();
            }
            #endregion

            public override uint Update()
            {
                //GC.Collect();
                try
                {

                    GLFW.PollEvents();

                }
                finally
                {

                }

                return 0;
            }

            public override void PreRender()
            {

                //GL.ClearColor(0, 0.3f, 0.4f, 1);
                //GL.Clear(ClearBufferMask.ColorBufferBit);

                
                    m_imm_device_context.clearRenderTargetColor(m_swap_chain, 0.22f, 0.22f, 0.22f);
            }

            public override uint Render()
            {
                
                try
                {
                    lock (this)
                    {
                        //GLFW.SwapBuffers(i_Window);
                    }


                }
                catch (System.ExecutionEngineException ex)
                {
                    HV_LOG_ERROR(true, ex.Message);
                }
                m_swap_chain.present(0);

                return 0;
            }

            public override void Destroy()
            {
                m_dxgi_device.Release();
                m_dxgi_adapter.Release();
                m_dxgi_factory.Release();

                m_d3d_device.Release();
                //m_imn_context.Release();

                m_swap_chain.release();
                m_imm_device_context.release();
            }

            public override void ChangeTitle(string ty)
            {
                GLFW.SetWindowTitle(i_Window, ty);
            }

            public override float[] GetWinSize()
            {
                GLFW.GetWindowSize(i_Window, out int w, out int h);
                return new float[2] { w, h };
            }
        }
    }
}
