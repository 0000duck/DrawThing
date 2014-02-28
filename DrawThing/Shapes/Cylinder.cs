using System;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace DrawThing
{
    // Cylinder => Disk
    class Cylinder : Circle 
    {
        protected Vector3 Height;

        /// <summary>
        /// Initialize a solid cylinder
        /// </summary>
        /// <param name="nVertices">The number of points per circle that describe the circle</param>
        /// <param name="position">The center point of the rear circle</param>
        /// <param name="radius">The radius of both circles</param>
        /// <param name="Height">The distance between the front and back circles</param>
        public Cylinder(int nVertices, Vector3 position, float radius, float Height)
            : base(nVertices, position, radius)
        {
            this.Height = new Vector3(0f, 0f, Height);
            PrepVerticesArray();
            CalcVertices();
        }

        protected override void CalcVertices()
        {
            // Even indices (0, 2, 4, 6...) are back, Odd are front
            // Using the circle generator code...

            float theta = 0;
            float dtheta = (2 * (float)Math.PI) / Vertices.Length;
            Vector3 CurrentPoint = Vector3.Zero;

            for (int i = 0; i < Vertices.Length; i++)
            {
                CurrentPoint.X = (float)(Radius * Math.Round(Math.Cos(theta), 4));
                CurrentPoint.Y = (float)(Radius * Math.Round(Math.Sin(theta), 4));

                Vector3.Add(ref CurrentPoint, ref Vertices[i], out Vertices[i]);
                theta += dtheta;
            }
        }

        public override void Draw()
        {
            GL.Color3(_Color);
            // Draw the rear cap circle
            GL.Begin(PrimitiveType.Polygon);
            {
                for (int i = 0; i < Vertices.Length; i++)
                {
                    GL.Vertex3(Vertices[i]);
                }
            }
            GL.End();

            // Draw the front cap circle
            GL.Begin(PrimitiveType.Polygon);
            {
                for (int i = 0; i < Vertices.Length; i++)
                {
                    GL.Vertex3(Vector3.Add(Vertices[i], Height));
                }
            }
            GL.End();

            // Draw the walls
            GL.Begin(PrimitiveType.TriangleStrip);
            {

                for (int i = 0; i < Vertices.Length; i++)
                {
                    GL.Vertex3(Vertices[i]);
                    GL.Vertex3(Vector3.Add(Vertices[i], Height));
                }
                // Weird gap on one side, check the numbers of vertices
                // being drawn.
            }
            GL.End();
        }

        public void ChangeHeight(float H)
        {
            Height.Z += H;
        }
    }
}
