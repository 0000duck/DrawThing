using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrawThing.Shapes
{
    class IntegralVolume
    {
        Cylinder[] Subdivisions;

        public IntegralVolume(int subdiv) // Other features planned, currently only models one function
        {
            Subdivisions = new Cylinder[subdiv];

        }

        private void InitSubdivisions(float[,] FunctionValues)
        {
            
        }

        private float[,] GenerateFunctionValues(int n)
        {
            float[,] FuncVal = new float[2, n];

            for (int x = 0; x < 1; x++)
            {
                for (int y = 0; y < n; y++) { 
}
            }

            return FuncVal;
        }

        private float F(float x)
        {
            Point output = new Point(X, 0f, 0f);

            if (X <= 2) output.y = X;
            else if (X <= 5) output.y = (2 * X - 2);
            else if (X <= 6) output.y = (-2 * X + 18);

            return output;
        }
    }
}
