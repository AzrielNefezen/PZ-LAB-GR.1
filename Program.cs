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

            string reg_mes = "(?<=configuration )(.*?)(?=- )";

            string lines_mes;

            lines_mes = File.ReadAllText("Ni-odd.mes");

            //Console.WriteLine(lines_mes);
            int i = 0;
            foreach (Match match in Regex.Matches(lines_mes, reg_mes))
            {
                list_mes.Add(new config__mes(match.ToString()));
            }

            reg_mes = "(?<=parameters :)(.*?)(?=)(.*)";

            foreach (Match match in Regex.Matches(lines_mes, reg_mes))
            {
                list_mes[i].assign_parameters(match.ToString());
                i++;
            }

            reg_mes = "(?<=submatrix )(.*?)(?=- )(-    [0-9])";
            i = 0;

            //foreach (Match match in Regex.Matches(lines_mes, reg_mes))
            //{
            //    list_mes[i].
            //    i++;
            //}


            Console.ReadKey();
        }
    }
}
