using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Projekt_PZ_Grupa1
{
    class config__mes
    {
        Array config_name;
        List<submatrix__mes> subm = new List<submatrix__mes> { };
        int par_max;
        int par_min;
        public config__mes(string a)
        {
            Array splitted = a.Split(' ');
            config_name = splitted;
            subm.Add(new submatrix__mes());
        }
        public config__mes()
        {
        }
        public void assign_submatrix(string a)
        {
            Array splitted = a.Split('-');

        }

        public void add_submatrix()
        {
            subm.Add(new submatrix__mes());
        }

        public void assign_subm_parameters(int a, string b, int x)
        {
            subm[a].assign_parameters(b, x);
        }

        public int subm_lenght()
        {
            return subm.Count() - 1;
        }

        public void assign_parameters(string a, int b)
        {
            SQLiteConnection sqlite;
            sqlite = new SQLiteConnection("Data Source=PZdb.db;Version=3;New=True;Compress=True;");
            sqlite.Open();
            a = a.Replace(" ", "");
            par_max = Convert.ToInt32(a.Split('-')[1]);
            par_min = Convert.ToInt32(a.Split('-')[0]);
            SQLiteCommand myCommand = new SQLiteCommand("UPDATE Configuration SET Max=" + par_max + ", Min=" + par_min + " WHERE ConfigID=" + b + " ;");
            myCommand.Connection = sqlite;
            myCommand.ExecuteNonQuery();
            sqlite.Close();
        }

    }
}
