using HallovEngine.Core;
using HallovEngine.Render;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Platform.Windows;


namespace Testing
{
    public sealed class DxGame : Layer
    {
        internal string _title = "HallovEngine";
        public override string Title { get => _title; set => _title = value; }

        public override void Init()
        {

            

        }
        public override void Update()
        {
            var a = i_graphicsEngine.GetWinSize();


            //Hallov.Console.Messages.HV_LOG_WARNING(false, fps.GetFps().ToString());
            _title = $"HallovEngine({Hallov.Api.ToString()}) Fps: {fps.GetFps()}";
        }

        float h;
        public override void Render()
        {
            h = (float)fps.secondsElapsed * 240;
            
            Console.Title = h.ToString();



            fps.Increse();
        }

        public override void Destroy()
        {

        }
    }
}
