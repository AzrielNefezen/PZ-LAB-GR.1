using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_PZ_Grupa1
{
    class config__codes
    {
        List<string> codes_name = new List<string> { };
        public void add_to_name(string a)
        {
            codes_name.Add(a);
        }
    }
}
