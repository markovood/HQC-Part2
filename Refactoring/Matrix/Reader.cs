using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrix
{
    public class Reader : IReadable
    {
        public int ReadSize()
        {
            Console.WriteLine("Enter a positive number ");
            string input = Console.ReadLine();
            int n = 0;
            while (!int.TryParse(input, out n) ||
                    n < 1 ||
                    n > 100)
            {
                Console.WriteLine("You haven't entered a correct positive number");
                input = Console.ReadLine();
            }

            return n;
        }
    }
}
