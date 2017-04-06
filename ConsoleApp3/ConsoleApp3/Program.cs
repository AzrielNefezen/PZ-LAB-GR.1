using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = File.ReadAllText("Ni-odd.out");
            
            MatchCollection SubmatrixIndex = Regex.Matches(text, "(?<=EIGENVECTORS,SUBMATRIX( )+)([0-9])+");
            
            MatchCollection SingleCoordinates = Regex.Matches(text, "(?<=\\()( )+([0-9]+,( )+[0-9]+)");

            MatchCollection Values = Regex.Matches(text, "(?<=\\))( )+(-)?([0-9]+\\.[0-9]+)");

            List<SingleSubmatrix> SubMatrices = new List<SingleSubmatrix> { };

            SubMatrices.Add(new SingleSubmatrix());
            int i = 1;
            int currentElement = 0;
            int lastIndex = 1;
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
            foreach (Match index in SubmatrixIndex)
            {

                SubMatrices.Add(new SingleSubmatrix());

                for (; currentElement < SingleCoordinates.Count;)
                {
                    {
                        Match matched = SingleCoordinates[currentElement]; 
                        string CoordinatesText = matched.ToString().Replace(" ", "");
                        if (Convert.ToInt32(CoordinatesText.Split(',')[0]) >= lastIndex)
                        {



                            SubMatrices[Convert.ToInt32(index.ToString())].Values.Add(new SingleSubmatrixValues(Convert.ToInt32(CoordinatesText.Split(',')[0]), Convert.ToInt32(CoordinatesText.Split(',')[1]), Convert.ToDouble(Values[currentElement].ToString().Trim())));
                            currentElement++;
                            lastIndex = Convert.ToInt32(CoordinatesText.Split(',')[0]);
                        }


                        else
                        {
                            currentElement++;
                            lastIndex = Convert.ToInt32(CoordinatesText.Split(',')[0]);
                            break;
                        }
                    }
                 }
                 i= 0;
            }

            var a = SubMatrices;

        }
    }
}
