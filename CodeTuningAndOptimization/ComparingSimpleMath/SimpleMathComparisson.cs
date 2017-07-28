using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparingSimpleMath
{
    public class SimpleMathComparisson
    {
        public static void Main()
        {
            Stopwatch sw = new Stopwatch();
            
            for (int i = 0; i < 10; i++)
            {
                sw.Start();
                int result = 0;
                for (int j = 0; j < 1000000000; j++)
                {
                    result = 1/1;
                }

                Console.WriteLine(sw.Elapsed);
                sw.Reset();
            }
            // adding: int --> 0.48ms, long --> 0.50ms, float --> 0.49ms, double --> 0.49ms, decimal --> 0.49ms
            // subtracting: int --> 0.48ms, long --> 0.49ms, float --> 0.49ms, double --> 0.49ms, decimal --> 0.49ms
            // incrementing: int --> 0.49ms, long --> 0.49ms, float --> 0.49ms, double --> 0.49ms, decimal --> 41.7s
            // multiplying: int --> 0.48ms, long --> 0.48ms, float --> 0.49ms, double --> 0.49ms, decimal --> 0.49ms
            // dividing: int --> 0.48ms, long --> 0.49ms, float --> 0.49ms, double --> 0.49ms, decimal --> 0.49ms
        }
    }
}
