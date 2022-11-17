using HallovEngine.Core;
using HallovEngine.Render;
using OpenTK.Graphics.OpenGL4;

namespace Testing
{
    public sealed class Game : Layer
    {
        internal string _title = "hellow world";
        public override string Title { get => _title; set => _title = value; }

        Rendering.VertexBuffer vertexBuffer;
        Rendering.VertexArray VertexArray;
        Rendering.Shader shader;

        private readonly float[] _vertices =
        {
            -0.5f, -0.5f, 0.0f, // Bottom-left vertex
             0.5f, -0.5f, 0.0f, // Bottom-right vertex
             0.0f,  0.5f, 0.0f  // Top vertex
        }; 

        public override void Init()
        {
            vertexBuffer = Rendering.VertexBuffer.New();
            vertexBuffer.BindBuffer();
            vertexBuffer.BufferData(_vertices.Length * sizeof(float), _vertices, 2);
            //OpenTK.Graphics.OpenGL4.GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
            
            VertexArray = Rendering.VertexArray.New();

            VertexArray.AttribPointer(0, 3, 5126, false, 3 * sizeof(float), 0);
            //GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            shader = Rendering.Shader.New(@"#version 330

out vec4 outputColor;

void main()
{
    outputColor = vec4(1.0, 1.0, 0.0, 1.0);
}", @"#version 330 core


layout(location = 0) in vec3 aPosition;

void main(void)
{
    gl_Position = vec4(aPosition, 1.0);
}");
            shader.Use();
        }
        public override void Update()
        {
            fps.GetFps();
            Hallov.Console.Log(fps.secondsElapsed.ToString(), ConsoleColor.DarkYellow, this.GetType());
        }
        
        public override void Render()
        {
            shader.Use();

            VertexArray.Draw(3);

            Interlocked.Increment(ref fps._frameCount);
        }

        public override void Destroy()
        {

        }
    }
}
