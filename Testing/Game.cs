using HallovEngine.Core;
using HallovEngine.Render;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
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
           // vertexBuffer.BindBuffer();
            //vertexBuffer.BufferData(_vertices.Length * sizeof(float), _vertices, 35044);
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
            indexBuffer.BufferData(_indices.Length * sizeof(uint), _indices, 5125);

            shader = Rendering.Shader.New(
                File.ReadAllText(@"D:\Dev\HallovEngine-Master\Testing\frag.shader"), 
                File.ReadAllText(@"D:\Dev\HallovEngine-Master\Testing\vert.shader"));
            shader.Use();

            texture = Rendering.Texture.CreateFromFile(@"D:\Dev\HallovEngine-Master\Testing\Decals\Graffiti\decalgraffiti043a_cs.png");
            texture.Use((int)TextureUnit.Texture0);
            var a = i_graphicsEngine.GetWinSize();

        }
        public override void Update()
        {
            var a = i_graphicsEngine.GetWinSize();

            
            Hallov.Console.Messages.HV_LOG_WARNING(false, fps.GetFps().ToString());
        }

       // float h;
        public override void Render()
        {
           
            //shader.SetVar(ShaderDataType.Mat4, "model", Matrix4.Identity * Matrix4.CreateRotationZ(h / 57.295779513f));

           // h += (float)fps.secondsElapsed * 5;
            shader.Use();
            texture.Use((int)TextureUnit.Texture0);

          
            VertexArray.DrawIndexed(_indices.Length, 5125);

            fps.Increse();
        }

        public override void Destroy()
        {

        }
    }
}
