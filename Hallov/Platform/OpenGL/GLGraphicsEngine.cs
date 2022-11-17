using HallovEngine.Render;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace HallovEngine.Platform.OpenGL
{
    public partial class OpenGL
    {
        public unsafe sealed class GLGraphicsEngine : GraphicsEngine
        {
            internal Window* i_Window;

            public override bool IsRunning => !GLFW.WindowShouldClose(i_Window);

            public override uint Init()
            {
                GLFW.Init();
                GLFW.WindowHint(WindowHintInt.ContextVersionMajor, 3);
                GLFW.WindowHint(WindowHintInt.ContextVersionMinor, 3);
                GLFW.WindowHint(WindowHintOpenGlProfile.OpenGlProfile, OpenGlProfile.Core);

                //GLLoader.LoadBindings(new GLFWBindingsContext());

                i_Window = GLFW.CreateWindow(800, 600, "hi", null, null);


                if (i_Window is null)
                {
                    GLFW.DestroyWindow(i_Window);
                }

                GLFW.SetFramebufferSizeCallback(i_Window, framebuffer_size_callback);

                GLFW.MakeContextCurrent(i_Window);

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
            }
            #endregion

            public override uint Update()
            {

                return 0;
            }

            public override void PreRender()
            {
                GL.ClearColor(0.25f, 0.25f, 0.25f, 1.0f);
                GL.Clear(ClearBufferMask.ColorBufferBit);
            }

            public override uint Render()
            {
                GLFW.SwapBuffers(i_Window);
                GLFW.PollEvents();
                return 0;
            }

            public override void Destroy()
            {

            }
        }
    }
}
