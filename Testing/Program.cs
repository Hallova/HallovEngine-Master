// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using HallovEngine.Render;
using HallovEngine.Core;
using System.Diagnostics;
using System.Numerics;
using Testing;
using HallovEngine.Platform.OpenGL;
using HallovEngine;
using System.Management;
using Microsoft.Win32;

namespace DrawTriangle;

public static unsafe class Program
{

    public static void Main()
    {
        // Hallov.Console.Log("hi i just started", ConsoleColor.DarkBlue, typeof(OpenGL));

        foreach (var item in Directory.GetFiles(@""))
        {
            string text = File.ReadAllText(item);

            text.Replace("HallovEngine", "Hallov");

            File.WriteAllText(item, text);
        }


        //new DxGame().Run();
    }

}

