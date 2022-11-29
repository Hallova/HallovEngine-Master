using HallovEngine.Core;
using HallovEngine.Render;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Platform.Windows;


namespace Testing
{
    public sealed class dxGame : Layer
    {
        internal string _title = "hellow world";
        public override string Title { get => _title; set => _title = value; }

       
        public override void Init()
        {

        }


        public override void Update()
        {
            //fps.GetFps();
            Hallov.Console.Messages.HV_LOG_WARNING(false, fps.GetFps().ToString());
        }

        float h;
        public override void Render()
        {
            h = (float)fps.secondsElapsed * 240;
           
            Console.Title = h.ToString();

   
            Interlocked.Increment(ref fps._frameCount);
        }

        public override void Destroy()
        {

        }
    }
}
