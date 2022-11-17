using HallovEngine.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallovEngine.Core
{
    public abstract class Layer
    {
        public static class fps
        {
            public static DateTime _lastCheckTime = DateTime.Now;
            public static long _frameCount = 0;
            public static double secondsElapsed;

            public static double GetFps()
            {

                secondsElapsed = (DateTime.Now - _lastCheckTime).TotalSeconds;
                long count = Interlocked.Exchange(ref _frameCount, 0);
                double fps = count / secondsElapsed;
                _lastCheckTime = DateTime.Now;

               


                return fps;
            }
        }

        internal static GraphicsEngine i_graphicsEngine;
        public abstract string Title { get; set; }

        public uint Run()
        {
            i_graphicsEngine = Hallov.ProvideGrapicsEngine();
            //Hallov.Console.Assert(false, "LOLO");

            i_graphicsEngine.Init();
            this.Init();
            
            while (i_graphicsEngine.IsRunning)
            {
                i_graphicsEngine.Update();
                this.Update();

                i_graphicsEngine.PreRender();
                this.Render();
                i_graphicsEngine.Render();
            }
            i_graphicsEngine.Destroy();
            this.Destroy();
            

            return 0;
        }

        public abstract void Init();
        public abstract void Update();
        public abstract void Render();
        public abstract void Destroy();
    }
}
