// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using HallovEngine.Render;
using HallovEngine.Core;
using System.Diagnostics;
using System.Numerics;
using Testing;
using HallovEngine.Platform.OpenGL;
using HallovEngine;

namespace DrawTriangle;

public static unsafe class Program
{

    public static void Main()
    {
        //Hallov.Console.Assert(false, "Oh, i started");
        Hallov.Console.Log("hi i just started", ConsoleColor.DarkBlue, typeof(OpenGL));
        
       

        new Game().Run();
    }
}

