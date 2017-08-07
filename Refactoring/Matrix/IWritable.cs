using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrix
{
    public interface IWritable
    {
        void Write(string msg);
        void WriteLine();
    }
}
