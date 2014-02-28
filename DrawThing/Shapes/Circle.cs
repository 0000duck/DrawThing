using System;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace DrawThing
{    
    class Circle
    {
        protected Vector3   Position;
        protected Vector3[] Vertices;
        protected float Radius;
        protected Color _Color = Color.FromArgb(66, 12, 200);
        public Color Color
        {
            get { return _Color; }
            set { _Color = value; }
        }

        public Circle(int nVerticies, Vector3 position, float radius)
        {
            this.Position = position;
            this.Vertices = new Vector3[nVerticies];
            this.Radius = radius;

            PrepVerticesArray();
            CalcVertices();
        }

        protected void PrepVerticesArray()
        {
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i] = Position;
            }
        }

        protected virtual void CalcVertices()
        {

            float theta = 0;
            float dtheta = (2 * (float)Math.PI) / Vertices.Length;

            double x, y;

            for (int i = 0; i < Vertices.Length; i++)
            {
                x = Radius * Math.Round(Math.Cos(theta), 4);
                y = Radius * Math.Round(Math.Sin(theta), 4);

                Vertices[i] = Vector3.Add(new Vector3((float)x, (float)y, 0f), Vertices[i]);

                theta += dtheta;
            }
        }

        public virtual void Draw()
        {
            GL.Begin(PrimitiveType.Polygon);
            GL.Color4(_Color);
            for (int i = 0; i < Vertices.Length; i++)
            {
                GL.Vertex3(Vertices[i]);
            }
            GL.End();
        }
    }
    
}
