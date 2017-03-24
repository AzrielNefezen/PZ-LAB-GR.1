using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class config__mes
    {
        Array config_name;
        List<submatrix__mes> subm = new List<submatrix__mes>{ };
        int par_max;
        int par_min;
        public config__mes(string a)
        {
            Array splitted= a.Split(' ');
            config_name = splitted;
            subm.Add(new submatrix__mes());
        }
        public void assign_submatrix(string a)
        {
            Array splitted = a.Split('-');

        }
        
        public void add_submatrix()
        {
            subm.Add(new submatrix__mes());
        }

        public void assign_subm_parameters(int a,string b)
        {
            subm[a].assign_parameters(b);
        }

        public int subm_lenght()
        {
            return subm.Count() - 1;
        }
            
        public void assign_parameters(string a)
        {
            a = a.Replace(" ","");
            par_max = Convert.ToInt32(a.Split('-')[1]);
            par_min = Convert.ToInt32(a.Split('-')[0]);
        }

    }
}
