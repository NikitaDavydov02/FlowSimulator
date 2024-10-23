using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConvergenceInvestigation
{
    class System
    {
        double[] P;
        double[,] F;
        double[,] omega;
        double[] V;
        double[] C;

        double R = 8.314;
        double T = 298;
        double m = 0.018;
        double gamma = 1.33;
        //------------------------------
        public System()
        {
            /*P = new double[P_count];
            for(int i=0;i<P.Length;i++)
               P[i] = 101325;
            F = new double[F_count];
            for (int i = 0; i < F.Length; i++)
                F[i] = 0;
            PToFMatrixmatrix = new double[P_count,F_count];*/
        }
        public double CalculateMolarFlowResistanceCoeffitient(double diameterInMilimiters)
        {
            double omega = (3.1415*diameterInMilimiters* diameterInMilimiters/4000000) * Math.Sqrt(gamma / (m * R * T));
            return omega;
        }
        public void Initialize()
        {
            using(StreamReader sr = new StreamReader("input.txt"))
            {
                //Volumes initialization
                int volumeCount = Convert.ToInt32(sr.ReadLine());
                V = new double[volumeCount];
                
                C = new double[volumeCount];
                string[] volumes_string = sr.ReadLine().Split(' ');
                for (int i = 0; i < volumeCount; i++)
                {
                    V[i] = Convert.ToDouble(volumes_string[i]);
                    C[i] = R * T / V[i];
                }
                sr.ReadLine();

                //Pressure initialzation;
                P = new double[volumeCount];
                string[] pressure_string = sr.ReadLine().Split(' ');
                for (int i = 0; i < volumeCount; i++)
                {
                    P[i] = Convert.ToDouble(pressure_string[i]);
                }

                //Area Matrix
                omega = new double[volumeCount, volumeCount];
                F = new double[volumeCount, volumeCount];
                for (int i = 0; i < volumeCount; i++)
                {
                    //i-th volume connections
                    double[] diametes = new double[volumeCount];
                    string[] diameters_string = sr.ReadLine().Split(' ');
                    for (int j = i; j < volumeCount; j++)
                    {
                        diametes[j] = Convert.ToDouble(diameters_string[j]);
                        double o = CalculateMolarFlowResistanceCoeffitient(diametes[j]);
                        omega[i, j] = o;
                        omega[j, i] = o;
                        F[i, j] = 0;
                        F[j, i] = 0;
                    }
                }
            }
        }
    }
}
