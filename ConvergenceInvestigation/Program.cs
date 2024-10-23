using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConvergenceInvestigation
{
    class Program
    {
        static int iteration = 0;
        static double dt = 10;
        static SimulationSystem system;
        static StreamWriter sw;
        static void Main(string[] args)
        {
            sw = new StreamWriter("output.csv");
            try
            {
                system = new SimulationSystem();
                system.Initialize();

                string line = "Iteration, dt,";
                line += system.GetFirstExtendedLogString();
                sw.WriteLine(line);

                while (iteration < 10000)
                    NewIteration();

                Console.WriteLine("Done!");
            }
            catch
            {
                sw.Close();
            }
            Console.ReadKey();
        }
        static void NewIteration()
        {
            system.UpdateFlows();
            system.Iteration(iteration, ref dt);
            iteration++;
            FileLog();
        }
        static void FileLog()
        {
            string line = iteration +"," + dt +",";
            line += system.GetExtendedLogString();
            sw.WriteLine(line);
        }
        /*static void NewIteration()
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
        }*/
    }
}
