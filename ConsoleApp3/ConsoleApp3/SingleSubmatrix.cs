using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class SingleSubmatrix
    {
        public int Index;
        public List<SingleSubmatrixValues> Values;

        public SingleSubmatrix() {
            this.Index = 0;
            this.Values = new List<SingleSubmatrixValues> { };
        }
    }
}
