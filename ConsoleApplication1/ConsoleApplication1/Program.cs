using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<config__mes> list_mes = new List<config__mes> { };
            //List<config__codes> list_codes = new List<config__codes> { };

            string reg_mes = "(?<=configuration )(.*?)(?=- )";
            string lines_mes;

            lines_mes = File.ReadAllText("Ni-odd.mes");

            //Console.WriteLine(lines_mes);
            int i = 0;
            
            foreach (Match match in Regex.Matches(lines_mes, reg_mes))
            {
                list_mes.Add(new config__mes(match.ToString()));

            }

            int counter = -1;

            reg_mes = "(?<=submatrix( )*)([0-9]+)(?= )";

            foreach (Match match in Regex.Matches(lines_mes, reg_mes))
            {
                //Console.WriteLine(match2.ToString());
                if (match.ToString() == "1")
                {
                    counter++;
                }
                list_mes[counter].add_submatrix();
            }

            reg_mes = "(?<=terms( )*)(.*?)(?=)(.*)";

            i = 1;
            counter = 0;

            foreach (Match match in Regex.Matches(lines_mes, reg_mes))
            {
                list_mes[counter].assign_subm_parameters(i, match.ToString());
                i++;
                if((i-1)==list_mes[counter].subm_lenght())
                {
                    counter++;
                    i = 1;
                }
            }

            i = 0;

            reg_mes = "(?<=parameters :)(.*?)(?=)(.*)";

            foreach (Match match in Regex.Matches(lines_mes, reg_mes))
            {
                list_mes[i].assign_parameters(match.ToString());
                i++;
            }

            //reg_mes = "([a-z0-9])+( )+([a-z0-9])+ ([a-z0-9])*( )+- ([a-z0-9])+ ([a-z0-9])+ ([a-z0-9])*";

            //i = 0;

            //foreach (Match match in Regex.Matches(lines_mes, reg_mes))
            //{
            //    reg_mes = "[0-9a-z-]+";
                
            //    list_codes.Add(new config__codes());
            //    foreach (Match match2 in Regex.Matches(match.ToString(), reg_mes))
            //    {
            //        list_codes[i].add_to_name(match2.ToString());
            //    }
            //    i++;
            //}

            Console.ReadKey();
        }
    }
}
