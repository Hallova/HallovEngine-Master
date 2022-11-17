using HallovEngine.Core;


namespace Testing
{
    public sealed class Game : Layer
    {
        internal string _title = "hellow world";
        public override string Title { get => _title; set => _title = value; }

        public override void Init()
        {
           

        }
        public override void Update()
        {
            fps.GetFps();
            Hallov.Console.Log(fps.secondsElapsed.ToString(), ConsoleColor.DarkYellow, this.GetType());
        }

        public override void Render()
        {
            Interlocked.Increment(ref fps._frameCount);
        }

        public override void Destroy()
        {

        }
    }
}
