using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace firmyWPF
{
    /// <summary>
    /// Interaction logic for CompanyDialog.xaml
    /// </summary>
    public partial class CompanyDialog : Window
    {
        public CompanyDialog(string nazev, string dic, string obec)
        {
            InitializeComponent();
            NazevTextBox.Text = nazev;
            DICTextBox.Text = dic;
            ObecTextBox.Text = obec;
        }

        public void CompanyPageButtonClick(object sender, RoutedEventArgs e)
        {
            string nazev = NazevTextBox.Text.Trim();
            string dic = DICTextBox.Text.Trim();

            if (string.IsNullOrEmpty(dic) || string.IsNullOrEmpty(nazev))
            {
                return;
            }
            string obec = ObecTextBox.Text.Trim();
            string poznamka = PoznamkaTextBox.Text.Trim();
            using(var connection = new SqliteConnection("Data Source=db.db"))
            {
                connection.Execute(@"INSERT INTO Company (Nazev, Dic, Poznamka, Obec) VALUES (@Nazev, @Dic, @Poznamka, @Obec)", new {
                    Nazev = nazev,
                    Dic = dic,
                    Poznamka = poznamka,
                    Obec = obec
                });
            }
            MessageBox.Show($"{nazev} ulozeno");
            Close();
        }
    }
}
