﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Data.SQLite;
using Microsoft.Win32;
using System.IO;

namespace Projekt_PZ_Grupa1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string sciezka;
        string text;
        string[] text_m;
        string lines_mes;
        string[] lines_out;
        String[] splitted;
        List<SingleSubmatrix> SubMatrices = new List<SingleSubmatrix> { };
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_out_Click(object sender, RoutedEventArgs e)
        {
            var out1 = new Microsoft.Win32.OpenFileDialog();
            out1.DefaultExt = ".out";
            out1.Filter = "Plik out (*.out) | *.out";
            var result_out = out1.ShowDialog();
            if (result_out == true)
            {
                sciezka = out1.FileName;
                text = System.IO.File.ReadAllText(sciezka);
                splitted = Regex.Split(text, "OBS.G-CAL.G");
                splitted[0] = "";
                splitted = Regex.Split(splitted[1], "ITERATION");

                lines_out = splitted[0].Split(new char[] { Convert.ToChar("\n") }, StringSplitOptions.RemoveEmptyEntries);
                label_out.Content = "Zaladowano plik .out";
            }
            if (sciezka != null)
            {
                SQLiteConnection sqlite;
                sqlite = new SQLiteConnection("Data Source=PZdb.db;Version=3;New=True;Compress=True;");
                sqlite.Open();
                SQLiteCommand myCommand = new SQLiteCommand("DELETE FROM Amplitudes;");
                myCommand.Connection = sqlite;
                myCommand.ExecuteNonQuery();
                myCommand = new SQLiteCommand("DELETE FROM SQLITE_SEQUENCE WHERE NAME='Amplitudes'");
                myCommand.Connection = sqlite;
                myCommand.ExecuteNonQuery();
                myCommand = new SQLiteCommand("DELETE FROM Submatrices;");
                myCommand.Connection = sqlite;
                myCommand.ExecuteNonQuery();
                myCommand = new SQLiteCommand("DELETE FROM SQLITE_SEQUENCE WHERE NAME='Submatrices'");
                myCommand.Connection = sqlite;
                myCommand.ExecuteNonQuery();


                MatchCollection SubmatrixIndex = Regex.Matches(text, "(?<=EIGENVECTORS,SUBMATRIX( )+)([0-9])+");

                MatchCollection SingleCoordinates = Regex.Matches(text, "(?<=\\()( )+([0-9]+,(( )?|[0-9]?)+[0-9]+)");

                MatchCollection ConfigurationList = Regex.Matches(text, "(( )?[0-9]+[A-Za-z](,|;|)?)+");

                splitted = Regex.Split(text, "EIGENVECTORS,SUBMATRIX  1");
                String joined = "";
                splitted[0] = "";


                MatchCollection Values = Regex.Matches(splitted[1], "(?<=\\))( )+(-)?([0-9]+\\.[0-9]+)");


                SubMatrices.Add(new SingleSubmatrix());
                int i = 1;
                int currentElement = 0;
                int lastIndex = 1;
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
                foreach (Match index in SubmatrixIndex)
                {
                    SingleSubmatrix temp = new SingleSubmatrix();
                    temp.Index = Convert.ToInt32(index.ToString());

                    SubMatrices.Add(temp);
                    myCommand = new SQLiteCommand("INSERT INTO Submatrices (Indexxxx)  VALUES(" + temp.Index + ");");
                    myCommand.Connection = sqlite;
                    myCommand.ExecuteNonQuery();
                    //string queryString = "INSERT INTO Amplitudes (SubmatrixIndexX, SubmatrixIndexY, Value, SubmatrixID) VALUES";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("INSERT INTO Amplitudes (SubmatrixIndexX, SubmatrixIndexY, Value, SubmatrixID) VALUES");
                    for (; currentElement <= SingleCoordinates.Count; currentElement++)
                    {
                        {
                            
                                Match matched = currentElement < SingleCoordinates.Count? SingleCoordinates[currentElement]: SingleCoordinates[currentElement-1];
                                string CoordinatesText = currentElement < SingleCoordinates.Count ? matched.ToString().Replace(" ", ""): "0";
                                if (currentElement < SingleCoordinates.Count && Convert.ToInt32(CoordinatesText.Split(',')[0]) >= lastIndex )
                                {

                                    SingleSubmatrixValues tempSingleSubmatrixValues = new SingleSubmatrixValues(Convert.ToInt32(CoordinatesText.Split(',')[0]), Convert.ToInt32(CoordinatesText.Split(',')[1]), Convert.ToDouble(Values[currentElement].ToString().Trim()));

                                    SubMatrices[Convert.ToInt32(index.ToString())].Values.Add(tempSingleSubmatrixValues);
                                //TODO Change to StringBuilder.Append();
                                //queryString = String.Concat(queryString, "(" + tempSingleSubmatrixValues.IndexX + ", " + tempSingleSubmatrixValues.IndexY + ", " + tempSingleSubmatrixValues.Value + ", " + SubMatrices[Convert.ToInt32(index.ToString())].Index + "),");
                                    sb.Append("(" + tempSingleSubmatrixValues.IndexX + ", " + tempSingleSubmatrixValues.IndexY + ", " + tempSingleSubmatrixValues.Value + ", " + SubMatrices[Convert.ToInt32(index.ToString())].Index + "),");
                                    lastIndex = Convert.ToInt32(CoordinatesText.Split(',')[0]);
                                }
                            


                            else
                            {
                                string queryString = sb.ToString();
                                if (queryString == "INSERT INTO Amplitudes (SubmatrixIndexX, SubmatrixIndexY, Value, SubmatrixID) VALUES")
                                {
                                    break;
                                }
                                queryString = queryString.Remove(queryString.Length - 1);
                                myCommand = new SQLiteCommand(queryString);
                                myCommand.Connection = sqlite;
                                myCommand.ExecuteNonQuery();
                                lastIndex = Convert.ToInt32(CoordinatesText.Split(',')[0]);
                                break;
                            }
                        }
                    }
                    i = 0;
                }

                var a = SubMatrices;
            }
        }

        private void button_mes_Click(object sender, RoutedEventArgs e)
        {
            List<config__mes> list_mes = new List<config__mes> { };
            string reg_mes = "(?<=configuration )(.*?)(?=- )";

            var mes = new Microsoft.Win32.OpenFileDialog();
            mes.DefaultExt = ".mes";
            mes.Filter = "Plik mes (*.mes) | *.mes";
            var result_mes = mes.ShowDialog();
            if (result_mes == true)
            {
                sciezka = mes.FileName;
                text_m = System.IO.File.ReadAllLines(sciezka);
                label_mes.Content = "Zaladowano plik .mes";
                lines_mes = System.IO.File.ReadAllText(sciezka);
            }
            if (sciezka != null)
            {
                int i = 0;

                SQLiteConnection sqlite;
                sqlite = new SQLiteConnection("Data Source=PZdb.db;Version=3;New=True;Compress=True;");
                sqlite.Open();
                SQLiteCommand myCommand = new SQLiteCommand("DELETE FROM Configuration;");
                myCommand.Connection = sqlite;
                myCommand.ExecuteNonQuery();
                myCommand = new SQLiteCommand("DELETE FROM SQLITE_SEQUENCE WHERE NAME='Configuration'");
                myCommand.Connection = sqlite;
                myCommand.ExecuteNonQuery();
                myCommand = new SQLiteCommand("DELETE FROM submatrix_mes;");
                myCommand.Connection = sqlite;
                myCommand.ExecuteNonQuery();
                myCommand = new SQLiteCommand("DELETE FROM SQLITE_SEQUENCE WHERE NAME='submatrix_mes'");
                myCommand.Connection = sqlite;
                myCommand.ExecuteNonQuery();



                list_mes.Add(new config__mes());
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("INSERT INTO Configuration (Config_name, Min, Max)  VALUES");

                foreach (Match match in Regex.Matches(lines_mes, reg_mes))
                {
                    sb.Append("('" + match.ToString() + "', 0, 0),");
                    list_mes.Add(new config__mes(match.ToString()));
                }
                string queryString = sb.ToString();
                queryString = queryString.Remove(queryString.Length - 1);
                myCommand = new SQLiteCommand(queryString);
                myCommand.Connection = sqlite;
                myCommand.ExecuteNonQuery();
                int counter = 0;
                reg_mes = "(?<=submatrix( )*)([0-9]+)(?= )";

                System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
                sb2.Append("INSERT INTO submatrix_mes (configID, term_max, term_min) VALUES");

                foreach (Match match in Regex.Matches(lines_mes, reg_mes))
                {
                    if (match.ToString() == "1")
                    {
                        counter++;
                    }
                    sb2.Append("(" + counter + ", 1, 1),");

                    list_mes[counter].add_submatrix();
                }
                string queryString2 = sb2.ToString();
                queryString2 = queryString2.Remove(queryString2.Length - 1);
                myCommand = new SQLiteCommand(queryString2);
                myCommand.Connection = sqlite;
                myCommand.ExecuteNonQuery();
                reg_mes = "(?<=terms( )*)(.*?)(?=)(.*)";

                i = 1;
                counter = 1;
                sqlite.Close();
                int x = 1;
                foreach (Match match in Regex.Matches(lines_mes, reg_mes))
                {
                    list_mes[counter].assign_subm_parameters(i, match.ToString(), x);
                    i++;
                    x++;
                    if ((i - 1) == list_mes[counter].subm_lenght())
                    {
                        counter++;
                        i = 1;
                    }
                }
                sqlite.Open();
                i = 1;

                reg_mes = "(?<=parameters :)(.*?)(?=)(.*)";

                foreach (Match match in Regex.Matches(lines_mes, reg_mes))
                {
                    list_mes[i].assign_parameters(match.ToString(), i);
                    i++;
                }

                sqlite.Close();
            }      
    }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            if(lines_mes==null || text == null)
            {
                MessageBox.Show("Najpierw wybierz pliki.");
            }
            else
            {
                for (int i = 0; i < lines_out.Length; i++)
                {
                    wyniki_out.Items.Add(lines_out[i]);
                }
            }
        }

        private void wyniki_out_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<String> out_act = new List<String> { };
            string line = wyniki_out.SelectedItem.ToString();
            string reg_out = "([0-9a-zA-Z.;,])+";
            foreach (Match match in Regex.Matches(line, reg_out))
            {
                out_act.Add(match.ToString());
            }
            if (out_act.Count>=10)
            {
                try
                {
                    wyniki_mes.Text = out_act[0] + " " + out_act[1];
                    int IndexX = Convert.ToInt32(out_act[0]);
                    int IndexY = Convert.ToInt32(out_act[1]);
                    SQLiteConnection sqlite;
                    sqlite = new SQLiteConnection("Data Source=PZdb.db;Version=3;New=True;Compress=True;");
                    sqlite.Open();
                    SQLiteCommand myCommand = new SQLiteCommand("SELECT Value FROM Amplitudes WHERE SubmatrixIndexX = " + IndexY + " AND SubmatrixID =" + IndexX + " ORDER BY ABS(Value) DESC LIMIT 10;");
                    myCommand.Connection = sqlite;
                    var reader = myCommand.ExecuteReader();
                    var list = new List<double>();
                    while (reader.Read()) {
                        var result = reader.GetDouble(0);
                        list.Add(result);
                    }
                    double[] rzecz = list.ToArray();
                    wyniki_mes.Text = "Podmacierz nr "+ IndexX + ", poziom nr " + IndexY +":\n" ;
                    foreach(double d in rzecz)
                    {
                        wyniki_mes.Text += d.ToString() + "\n";
                    }
                    //SubMatrices[IndexX].Values[IndexY -1].Value;
                    //wyniki_mes.Text = value.ToString();
                }
                catch (System.FormatException er)
                {
                    wyniki_mes.Text = "wybrano nieprawidlowa linie";
                }
            }
            else
            {
                wyniki_mes.Text = "0";
                
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "DefaultOutputName.txt";
            save.Filter = "Text File | *.txt";
            if (save.ShowDialog() == true)
                File.WriteAllText(save.FileName, wyniki_mes.Text);
        }
    }
}
