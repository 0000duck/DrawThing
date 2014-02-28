using System;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace DrawThing
{
    class Window : GameWindow
    {
        public Window() : base(800, 600, new GraphicsMode(32,24,0,16))
        {
            p1 = new Vector3(-1.0f, 1.0f, -1.0f);
            p2 = new Vector3(0.0f, -1.0f, -1.0f);
            p3 = new Vector3(1.0f, 1.0f, -1.0f);

            p4 = new Vector3(-1.0f, 1.0f, -1.5f);
            p5 = new Vector3(0.0f, -1.0f, -1.5f);
            p6 = new Vector3(1.0f, 1.0f, -1.5f);
        }

        Vector3 p1, p2, p3;
        Vector3 p4, p5, p6;

        bool Rotate = false;
        
        Matrix4 Pers = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 2, 4f / 3f, .1f, 100f);
        Matrix4 LookAt = Matrix4.LookAt(new Vector3(-1f, 0.5f, 3f), Vector3.Zero, Vector3.UnitY);

        int FPSLimiter = 0;
        static int Vertices = 4;

        protected override void OnLoad(EventArgs e)
        {
            Matrix4 Ortho = Matrix4.CreateOrthographic(4f, 3f, 0.1f, 100f);
            this.VSync = VSyncMode.On;
            GL.Enable(EnableCap.DepthTest);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref Pers);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref LookAt);

            GL.Viewport(0, 0, this.Width, this.Height);

            //GL.LineWidth(1.5f);
            GL.PointSize(2f);
            GL.Enable(EnableCap.LineSmooth);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {

            if (this.Keyboard[Key.Escape])
            {
                this.Exit();
            }

            float dx = 0.05f;

            if (this.Keyboard[Key.Space]) Rotate = !Rotate;

            #region Cam Move
            if (this.Keyboard[Key.W])
            {
                MoveCamera(0, 0, -dx);
            }
            if (this.Keyboard[Key.S])
            {
                MoveCamera(0, 0, dx);
            }

            if (this.Keyboard[Key.A])
            {
                MoveCamera(dx, 0, 0);
            }

            if (this.Keyboard[Key.D])
            {
                MoveCamera(-dx, 0, 0);
            }
            #endregion

            #region Target Move
            if (this.Keyboard[Key.Left])
            {
                RotateCam(dx, 1);
            }
            if (this.Keyboard[Key.Right])
            {
                RotateCam(-dx, 1);
            }
            if (this.Keyboard[Key.Up])
            {
                RotateCam(-dx, 0);
            }
            if (this.Keyboard[Key.Down])
            {
                RotateCam(dx, 0);
            }
            #endregion

            if (this.Keyboard[Key.R])
            {
                ResetCamera();
            }
        }

        private void ResetCamera()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref Pers);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref LookAt);
        }

        /// <summary>
        /// Rotate by Angle (counter-clockwise) around Axis
        /// </summary>
        /// <param name="Angle">Counter-clockwise, in radians</param>
        /// <param name="Axis">0: X
        ///                    1: Y
        ///                    2: Z</param>
        private void RotateCam(float Angle, int Axis)
        {
            Matrix4 Rot;
            GL.MatrixMode(MatrixMode.Modelview);
            switch (Axis)
            {
                case 0:
                    // X Axis
                    Rot = Matrix4.CreateRotationX(Angle);
                    GL.MultMatrix(ref Rot); 
                    break;
                case 1:
                    // Y Axis
                    Rot = Matrix4.CreateRotationY(Angle);
                    GL.MultMatrix(ref Rot);
                    break;
                case 2:
                    Rot = Matrix4.CreateRotationZ(Angle);
                    GL.MultMatrix(ref Rot);
                    break;
                    
                default:
                    return;
            }
        }

        protected void MoveCamera(float x, float y, float z)
        {
            Matrix4 Translate = Matrix4.CreateTranslation(x, y, z);
            
            GL.MatrixMode(MatrixMode.Modelview);
            GL.MultMatrix(ref Translate);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            

            if (FPSLimiter == 2)
            {
                this.Title = Math.Round((1.0f / e.Time), 0).ToString();
                FPSLimiter = 0;
            }
            else FPSLimiter++;
            

            // render graphics
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            if (Rotate) { GL.Rotate(MathHelper.TwoPi, 0, 1, 0); }

            GL.ClearColor(Color.CornflowerBlue);
            
            // draw Axes
            GL.Begin(PrimitiveType.Lines);
            {
                GL.Color3(Color.White);
                GL.Vertex3(-5, 0, 0);
                GL.Vertex3(5, 0, 0);

                GL.Color3(Color.Blue);
                GL.Vertex3(0, -5, 0);
                GL.Vertex3(0, 5, 0);

                GL.Color3(Color.Green);
                GL.Vertex3(0, 0, -5);
                GL.Vertex3(0, 0, 5);
            }
            GL.End();

            // draw -Y plane
            GL.Begin(PrimitiveType.Quads);
            {
                GL.Color3(Color.Gray);
                GL.Vertex3(-5f, -5f, -5f);
                GL.Vertex3(5f, -5f, -5f);
                GL.Vertex3(5f, -5f, 5f);
                GL.Vertex3(-5f, -5f, 5f);
            } 
            GL.End();

            this.SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, this.Width, this.Height);
        }
    }
}
