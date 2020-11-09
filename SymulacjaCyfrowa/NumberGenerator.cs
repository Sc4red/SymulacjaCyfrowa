using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSimulation
{
    class NumberGenerator
    {
  
        private const int WspQ = 127773;
        private const int WspR = 2836;
        private const int Range = 2147483647;

        private double _time = 10000000;

        

       public double Distribution()
       // Rozklad jednosyajny
        {
            var h = (int)((_time) / WspQ);                                              //H =ziarno / 127773
            (_time) = 16807 * ((_time) - WspQ * h) - WspR * h;                      //X = 16807 * ((ziarno - (127773 *H)) - (2836*H)
            if ((_time) < 0) (_time) += Range;                                       // (ziarno<0) ziarno+2147483647
            return (double)_time / (double)Range;                                       // ziarno/Range
        }


        public int Designate(double start, double end)
        // Liczba z zakresu
        {
            var b = start + (end - start) * Distribution();

            return (int)Math.Round(b);
            // return (int)b;
        }

        public double Gaussian(double mean, double stdDev)
        {
         
            double u1 = 1.0 - Distribution(); //uniform(0,1] random doubles
            double u2 = 1.0 - Distribution();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                   Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            return mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
        }

        public double Wyk(double lambda)
        {
         
           var x = Distribution();
           var a = Math.Log(x);

           var ans = -lambda * a;
    
            return ans;
        }

        public double Wyk(double lambda, out double x)
        {

            x = Distribution(); // Rozklad jednostajny z zakresu 0-1
         
            var a = Math.Log(x);
            var ans = -(lambda) * a;

            return ans;
        }

    }
}
