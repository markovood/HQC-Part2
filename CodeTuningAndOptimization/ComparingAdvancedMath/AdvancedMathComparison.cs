using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparingAdvancedMath
{
    public class AdvancedMathComparison
    {
        public static void Main()
        {
            Stopwatch sw = new Stopwatch();

            for (int i = 0; i < 10; i++)
            {
                sw.Start();
                double result = 0;
                for (int j = 0; j < 10000000; j++)
                {
                    result = Math.Sin(9f); // 9d, (double)9m, 
                }

                Console.WriteLine(sw.Elapsed);
                sw.Reset();
            }

            // square root: float --> 0.005ms, double --> 0.005ms, decimal --> 0.005ms
            // natural logarithm: float --> 0.28ms, double --> 0.28ms, decimal --> 0.28ms
            // sinus: float --> 0.005ms, double --> 0.005ms, decimal --> 0.005ms
        }
    }
}
