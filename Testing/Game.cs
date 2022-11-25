using HallovEngine.Core;
using HallovEngine.Render;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Platform.Windows;


namespace Testing
{
    public sealed class Game : Layer
    {
        internal string _title = "hellow world";
        public override string Title { get => _title; set => _title = value; }

        Rendering.VertexBuffer vertexBuffer;
        Rendering.VertexArray VertexArray;
        Rendering.IndexBuffer indexBuffer;

        Rendering.Texture texture;

        Rendering.Shader shader;

        private readonly float[] _vertices =
         {
            // Position         Texture coordinates
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
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

            //position
            VertexArray.AttribPointer(0, 3, 5126, false, 5 * sizeof(float), 0);


            //texcoords
            VertexArray.AttribPointer(1, 2, 5126, false, 5 * sizeof(float), 3 * sizeof(float));


            //GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            VertexArray.EndConfig();

            indexBuffer = Rendering.IndexBuffer.New();
            indexBuffer.BindBuffer();
            indexBuffer.BufferData(_indices.Length * sizeof(int), _indices, 5125);

            shader = Rendering.Shader.New(@"
#version 330

out vec4 outputColor;

in vec2 texCoord;

uniform sampler2D texture0;

void main()
{
 
    outputColor = texture(texture0, texCoord);
}", 

@"
#version 330 core

layout(location = 0) in vec3 aPosition;

layout(location = 1) in vec2 aTexCoord;

out vec2 texCoord;

void main(void)
{
    texCoord = aTexCoord;

    gl_Position = vec4(aPosition, 1.0);
}");
            shader.Use();

            texture = Rendering.Texture.CreateFromFile(@"D:\Dev\HallovEngine-Master\Testing\Decals\Graffiti\decalgraffiti001b_cs.png");
            texture.Use((int)TextureUnit.Texture0);

        }
        public override void Update()
        {
            //fps.GetFps();
            Hallov.Console.Messages.HV_LOG_WARNING(false, fps.GetFps().ToString());
            //Hallov.Console.Log(fps.secondsElapsed.ToString(), ConsoleColor.DarkYellow, this.GetType());
        }

        public override void Render()
        {
            shader.Use();
            texture.Use((int)TextureUnit.Texture0);

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
