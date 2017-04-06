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

namespace Projekt_PZ_Grupa1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string sciezka;
        string[] lines_out;
        string[] lines_mes;
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
                lines_out = System.IO.File.ReadAllLines(sciezka);
                label_out.Content = "Zaladowano plik .out";
            }
        }

        private void button_mes_Click(object sender, RoutedEventArgs e)
        {
            var mes = new Microsoft.Win32.OpenFileDialog();
            mes.DefaultExt = ".mes";
            mes.Filter = "Plik mes (*.mes) | *.mes";
            var result_mes = mes.ShowDialog();
            if (result_mes == true)
            {
                sciezka = mes.FileName;
                lines_mes = System.IO.File.ReadAllLines(sciezka);
                label_mes.Content = "Zaladowano plik .mes";
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            if(lines_mes==null || lines_out == null)
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
            if (out_act.Count>=16)
            {
                wyniki_mes.Text = out_act[14] + " " + out_act[15];
            }
            else
            {
                wyniki_mes.Text = "Nieprawidlowa linia";
            }
        }
    }
}