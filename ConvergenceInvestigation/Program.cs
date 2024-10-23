using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvergenceInvestigation
{
    class Program
    {
        static int iteration = 0;
        static double P = 0;
        static double F = 0;
        static double Pout = 101325;
        static double V = 0.1;
        static double R = 8.314;
        static double T = 273;
        static double m = 0.028;
        static double omega;
        static double S = 0.0001;
        static double gamma = 1.33;
        static double C;

        static double tau;
        static double dt = 10;
        static void Main(string[] args)
        {
            Console.WriteLine("Initialization");
            omega = S * Math.Sqrt(gamma / (m * R * T));
            C = R * T / V;
            tau = 1 / (C * omega);
            Console.WriteLine("Omega (mol/(Pa*sec)): " + omega);
            Console.WriteLine("C (Pa/mol)): " + C);
            Console.WriteLine("tau (sec)): " + tau);
            Console.WriteLine("dt (sec)): " +dt);
            Console.ReadKey();
            Console.WriteLine("Iteration, P, F, scaling");
            for (int i = 0; i < 1000; i++)
                ThirdSelfAjustmentIteration();
                //NewIteration();
            Console.ReadKey();
        }
        static void NewIteration()
        {
            F = omega * (Pout - P);
            double dP_dt = F * C;
            double relDelta = 0;
            if(P!=0)
                relDelta = Math.Abs(dP_dt * dt / P);

            double scaling = 1;
            if (relDelta >= 0.01)
                scaling = relDelta / 0.01;
            P += dP_dt * dt/scaling;
            Console.WriteLine(iteration + "," + P + "," + F + "," + scaling);
            iteration++;
        }
        static void SelfAjustmentIteration()
        {
            F = omega * (Pout - P);
            double dP_dt = F * C;
            double relDelta = 0;
            if (P != 0)
                relDelta = Math.Abs(dP_dt * dt / P);

            if (relDelta >= 0.01)
                dt/=2;
            P += dP_dt * dt;
            Console.WriteLine(iteration + "," + P + "," + F + "," + dt);
            iteration++;
        }
        static void SecondSelfAjustmentIteration()
        {
            F = omega * (Pout - P);
            double dP_dt = F * C;
            double relDelta = 0;
            double absDelta = dP_dt * dt;
            if (P != 0)
                relDelta = Math.Abs(absDelta / P);

            double scaling = 1;
            if (relDelta >= 0.01)
            {
                scaling = relDelta / 0.01;
                dt /= 2;
            }
            P += absDelta/scaling;
            Console.WriteLine(iteration + "," + P + "," + F + "," + dt);
            iteration++;
        }
        static double lastRelativeDelta = 1;
        static void ThirdSelfAjustmentIteration()
        {
            F = omega * (Pout - P);
            double dP_dt = F * C;
            double relDelta = 0;
            double absDelta = dP_dt * dt;
            if (P != 0)
                relDelta = Math.Abs(absDelta / P);

            double scaling = 1;
            if (relDelta >= 0.01)
            {
                scaling = relDelta / 0.01;
                dt /= 2;
            }
            else
            {
                if(relDelta < lastRelativeDelta && relDelta!=0)
                    dt *= 1.01;
            }
            P += absDelta / scaling;
            Console.WriteLine(iteration + "," + P + "," + F + "," + dt);
            iteration++;
            // if (iteration == 500)
            //    Pout *= 2;
            if (iteration == 900)
                ;
                lastRelativeDelta = relDelta;
        }
        static void AdvancedIteration()
        {
            double newP = (P+Pout*dt/tau) / (1+dt/tau);
            P = newP;
            F = omega * (Pout - P);
            Console.WriteLine(iteration + "," + P + "," + F + ",");
            iteration++;
        }
    }
}
