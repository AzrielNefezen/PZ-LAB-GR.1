using System.Data.SQLite;

namespace Projekt_PZ_Grupa1
{
    internal class submatrix__mes
    {

        int term_min;
        int term_max;

        public void assign_min(string a)
        {
            term_min = int.Parse(a);
        }

        public void assign_parameters(string a, int b)
        {
            SQLiteConnection sqlite;
            sqlite = new SQLiteConnection("Data Source=PZdb.db;Version=3;New=True;Compress=True;");
            sqlite.Open();
            a = a.Replace(" ", "");
            term_max = int.Parse(a.Split('-')[1]);
            term_min = int.Parse(a.Split('-')[0]);
            SQLiteCommand myCommand = new SQLiteCommand("UPDATE submatrix_mes SET term_max =" + term_max + ", term_min=" + term_min + " WHERE submID=" + b + " ;");
            myCommand.Connection = sqlite;
            myCommand.ExecuteNonQuery();
            sqlite.Close();
        }
    }
}