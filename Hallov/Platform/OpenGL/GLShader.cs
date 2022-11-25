using HallovEngine.Render;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;


namespace HallovEngine.Platform.OpenGL
{
    public static partial class OpenGL
    {

        public class GLShader : Rendering.Shader
        {
            public   int                      Handle;
            private  Dictionary<string, int> _uniformLocations;

            private string VertexShader;   private bool VertexAttached;
            private string FragmentShader; private bool FragmentAttached;


            public GLShader(string vert, string frag)
            {

                CreateShaderFromText(vert, frag);
            }
           

            public override byte CompileShader()
            {
                string ShaderSource = "";

                Handle = GL.CreateProgram();

                int vertexShader = 0;
                int fragmentShader = 0;

                if (VertexAttached)
                {
                    ShaderSource = VertexShader;
                    vertexShader = GL.CreateShader(ShaderType.VertexShader);

                    // Now, bind the GLSL source code
                    GL.ShaderSource(vertexShader, ShaderSource);

                    // And then compile
                    CompileShader(vertexShader);

                    GL.AttachShader(Handle, vertexShader);
                }

                
                if (FragmentAttached)
                {
                    ShaderSource = VertexShader;
                    vertexShader = GL.CreateShader(ShaderType.FragmentShader);

                    // Now, bind the GLSL source code
                    GL.ShaderSource(fragmentShader, ShaderSource);

                    // And then compile
                    CompileShader(fragmentShader);

                    GL.AttachShader(Handle, fragmentShader);
                }

                 LinkProgram(Handle);

                // When the shader program is linked, it no longer needs the individual shaders attached to it; the compiled code is copied into the shader program.
                // Detach them, and then delete them.
                if (VertexAttached)
                {
                    GL.DetachShader(Handle, vertexShader);
                    GL.DeleteShader(vertexShader);
                }

                if (FragmentAttached)
                {
                    GL.DetachShader(Handle, fragmentShader);
                    GL.DeleteShader(fragmentShader);
                }
               


                GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);
                _uniformLocations = new Dictionary<string, int>();
                for (var i = 0; i < numberOfUniforms; i++)
                {
                    var key = GL.GetActiveUniform(Handle, i, out _, out ActiveUniformType d);
                    var location = GL.GetUniformLocation(Handle, key);
                    _uniformLocations.Add(key, location);
                }

                return 0;
            }

            private void CreateShaderFromText(string vert, string frag)
            {
                var shaderSource = vert;
                // GL.CreateShader will create an empty shader (obviously). The ShaderType enum denotes which type of shader will be created.
                var vertexShader = GL.CreateShader(ShaderType.VertexShader);

                // Now, bind the GLSL source code
                GL.ShaderSource(vertexShader, shaderSource);

                // And then compile
                CompileShader(vertexShader);

                // We do the same for the fragment shader.
                shaderSource = frag;
                var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
                GL.ShaderSource(fragmentShader, shaderSource);
                CompileShader(fragmentShader);

                // These two shaders must then be merged into a shader program, which can then be used by OpenGL.
                // To do this, create a program...
                Handle = GL.CreateProgram();

                // Attach both shaders...
                GL.AttachShader(Handle, vertexShader);
                GL.AttachShader(Handle, fragmentShader);

                // And then link them together.
                LinkProgram(Handle);

                // When the shader program is linked, it no longer needs the individual shaders attached to it; the compiled code is copied into the shader program.
                // Detach them, and then delete them.
                GL.DetachShader(Handle, vertexShader);
                GL.DetachShader(Handle, fragmentShader);
                GL.DeleteShader(fragmentShader);
                GL.DeleteShader(vertexShader);

                // The shader is now ready to go, but first, we're going to cache all the shader uniform locations.
                // Querying this from the shader is very slow, so we do it once on initialization and reuse those values
                // later.

                // First, we have to get the number of active uniforms in the shader.
                GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

                // Next, allocate the dictionary to hold the locations.
                _uniformLocations = new Dictionary<string, int>();

                // Loop over all the uniforms,
                for (var i = 0; i < numberOfUniforms; i++)
                {
                    // get the name of this uniform,
                    var key = GL.GetActiveUniform(Handle, i, out _, out ActiveUniformType d);

                    //if(d == )
                    // get the location,
                    var location = GL.GetUniformLocation(Handle, key);

                    // and then add it to the dictionary.
                    _uniformLocations.Add(key, location);
                }
            }

            private static void CompileShader(int shader)
            {
                
                GL.CompileShader(shader);

               
                GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
                if (code != (int)All.True)
                {
                    
                    var infoLog = GL.GetShaderInfoLog(shader);
                    throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
                }
            }

            private static void LinkProgram(int program)
            {
                // We link the program
                GL.LinkProgram(program);

                // Check for linking errors
                GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
                if (code != (int)All.True)
                {
                    // We can use `GL.GetProgramInfoLog(program)` to get information about the error.
                    throw new Exception($"Error occurred whilst linking Program({program}), : {GL.GetProgramInfoLog(program)}");                   
                }
            }

            // A wrapper function that enables the shader program.
            public override void Use()
            {
                GL.UseProgram(Handle);
            }

            // The shader sources provided with this project use hardcoded layout(location)-s. If you want to do it dynamically,
            // you can omit the layout(location=X) lines in the vertex shader, and use this in VertexAttribPointer instead of the hardcoded values.
            public int GetAttribLocation(string attribName)
            {
                return GL.GetAttribLocation(Handle, attribName);
            }

            #region Sets
            /// <summary>
            /// Set a uniform int on this shader.
            /// </summary>
            /// <param name="name">The name of the uniform</param>
            /// <param name="data">The data to set</param>
            public void SetInt(string name, int data)
            {
                GL.UseProgram(Handle);
                GL.Uniform1(_uniformLocations[name], data);
            }

            /// <summary>
            /// Set a uniform float on this shader.
            /// </summary>
            /// <param name="name">The name of the uniform</param>
            /// <param name="data">The data to set</param>
            public void SetFloat(string name, float data)
            {
                GL.UseProgram(Handle);
                GL.Uniform1(_uniformLocations[name], data);
            }

            /// <summary>
            /// Set a uniform Matrix4 on this shader
            /// </summary>
            /// <param name="name">The name of the uniform</param>
            /// <param name="data">The data to set</param>
            /// <remarks>
            ///   <para>
            ///   The matrix is transposed before being sent to the shader.
            ///   </para>
            /// </remarks>
            public void SetMatrix4(string name, Matrix4 data)
            {
                GL.UseProgram(Handle);
                GL.UniformMatrix4(_uniformLocations[name], true, ref data);
            }

            /// <summary>
            /// Set a uniform Vector3 on this shader.
            /// </summary>
            /// <param name="name">The name of the uniform</param>
            /// <param name="data">The data to set</param>
            public void SetVector3(string name, Vector3 data)
            {
                GL.UseProgram(Handle);
                GL.Uniform3(_uniformLocations[name], data);
            }

          
            #endregion
        }
    }
}