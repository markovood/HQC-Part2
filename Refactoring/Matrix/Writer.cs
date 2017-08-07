using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrix
{
    public class Writer : IWritable
    {
        public void Write(string data)
        {
            Console.Write(data);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }
    }
}
