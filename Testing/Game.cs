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
        Rendering.IndexBuffer indexBuffer;

        Rendering.Shader shader;

        private readonly float[] _vertices =
         {
             0.5f,  0.5f, 0.0f, // top right
             0.5f, -0.5f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, // top left
        };

        // Then, we create a new array: indices.
        // This array controls how the EBO will use those vertices to create triangles
        private readonly uint[] _indices =
        {
            // Note that indices start at 0!
            0, 1, 3, // The first triangle will be the top-right half of the triangle
            1, 2, 3  // Then the second will be the bottom-left half of the triangle
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
            VertexArray.EndConfig();

            indexBuffer = Rendering.IndexBuffer.New();
            indexBuffer.BindBuffer();
            indexBuffer.BufferData(_indices.Length * sizeof(int), _indices, 5125);

            shader = Rendering.Shader.New(@"
#version 330 

out vec4 outputColor;

void main()
{
    outputColor = vec4(0.8f,0.9f, 0.4f, 1.0f);
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
            //Hallov.Console.Log(fps.secondsElapsed.ToString(), ConsoleColor.DarkYellow, this.GetType());
        }

        public override void Render()
        {
            shader.Use();

            vertexBuffer.BindBuffer();
            //VertexArray.Draw(3);
            VertexArray.DrawIndexed(_indices.Length, 5125);
            Interlocked.Increment(ref fps._frameCount);
        }

        public override void Destroy()
        {

        }
    }
}
