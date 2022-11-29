using HallovEngine.Render;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Vortice.DXGI;

namespace HallovEngine.Platform.OpenGL
{
    public partial class OpenGL
    {
        public unsafe sealed class GLGraphicsEngine : GraphicsEngine
        {
            internal Window* i_Window;

            public override bool IsRunning => !GLFW.WindowShouldClose(i_Window);

            private GLFWCallbacks.FramebufferSizeCallback resizeCallback;

            // later
            // I genuinely don't know how GLFW this might be the wrong function name.

            public override uint Init()
            {
                GLFW.Init();
                GLFW.WindowHint(WindowHintInt.ContextVersionMajor, 3);
                GLFW.WindowHint(WindowHintInt.ContextVersionMinor, 3);
                GLFW.WindowHint(WindowHintOpenGlProfile.OpenGlProfile, OpenGlProfile.Core);

                //GLLoader.LoadBindings(new GLFWBindingsContext());

                resizeCallback = framebuffer_size_callback;

                i_Window = GLFW.CreateWindow(800, 600, "hi", null, null);

                if (i_Window is null)
                {
                    GLFW.DestroyWindow(i_Window);
                }

                GLFW.SetFramebufferSizeCallback(i_Window, resizeCallback);
                //= framebuffer_size_callback;

                GLFW.MakeContextCurrent(i_Window);

                GLFW.SwapInterval(0);

                //GLFW.CreateWindowSurface(new VkHandle())
                GL.LoadBindings(new GLFWBindingsContext());

                return 0;
            }

            #region GLFW_Callbacks
            void framebuffer_size_callback(Window* window, int width, int height)
            {
                // make sure the viewport matches the new window dimensions; note that width and 
                // height will be significantly larger than specified on retina displays.
                GL.Viewport(0, 0, width, height);
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

                GL.ClearColor(0, 0.3f, 0.4f, 1);
                GL.Clear(ClearBufferMask.ColorBufferBit);
            }

            public override uint Render()
            {
                try
                {
                    lock (this)
                    {
                        GLFW.SwapBuffers(i_Window);
                    }


                }
                catch (System.ExecutionEngineException ex)
                {
                    HV_LOG_ERROR(true, ex.Message);
                }

                return 0;
            }

            public override void Destroy()
            {

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
