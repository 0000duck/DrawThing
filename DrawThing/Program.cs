using System;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace DrawThing
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Window gem = new Window())
            {
                gem.Run(60.0);
            }
        }
    }
}
