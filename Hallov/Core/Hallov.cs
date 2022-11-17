//#define SHOTYPES

using HallovEngine.Platform.OpenGL;
using HallovEngine.Render;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HallovEngine.Core
{
    public static partial class Hallov
    {
        public static GraphicsEngine ProvideGrapicsEngine()
        {
            return new OpenGL.GLGraphicsEngine();
        }

        public static class Console
        {
            
            public static class Messages
            {
                public static void HV_LOG_ERROR(bool v, string message, [Optional] Type shooter)
                {
                    Console.Log(message, ConsoleColor.DarkRed, shooter);
                }

                public static void HV_LOG_WARNING(bool v, string message, [Optional] Type shooter)
                {
                    Console.Log(message, ConsoleColor.DarkYellow, shooter);
                }

                public static void HV_LOG(bool v, string message, [Optional] Type shooter)
                {
                    Console.Log(message, ConsoleColor.DarkGray, shooter);
                }
            }
            
            public static void Assert(bool condition, string message = "ah, i failed it. Sorry")
            {
#if DEBUG
                Debug.Assert(condition, message);
#else
                ConsoleLog(message, ConsoleColor.Red);
#endif
            }

            public static void Log(string message, ConsoleColor color, Type shooter)
            {
                ConsoleLog($"[{DateTime.Now.TimeOfDay.ToString().Remove(8)}] " + 
                    (shooter is null ? "Hallov" :
#if SHOTYPES
                    shooter.Namespace
#else
                    shooter.FullName
#endif

                    ).ToUpper().Replace("ENGINE", string.Empty) + " : " + message, color);
            }

            public static void ConsoleLog(string message, uint color = 15)
            {
                System.Console.ForegroundColor = (ConsoleColor)color;
                System.Console.WriteLine(message);
                System.Console.ForegroundColor = ConsoleColor.White;
            }

            public static void ConsoleLog(string message, ConsoleColor color)
            {
                ConsoleLog(message, (uint)color);
            }
        }
    }
}
