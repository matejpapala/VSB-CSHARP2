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

namespace WPFTEST
{
    /// <summary>
    /// Interaction logic for EditDialog.xaml
    /// </summary>
    public partial class EditDialog : Window
    {

        private Subjekt _upravovanySubjekt;

        public EditDialog(Subjekt subjekt)
        {
            InitializeComponent();
            _upravovanySubjekt = subjekt;

            NazevTextBox.Text = _upravovanySubjekt.Nazev;
            DatumTextBox.Text = _upravovanySubjekt.ZapisDatum;
            IcoTextBox.Text = _upravovanySubjekt.ico;
        }

        private void UlozitButton_Click(object sender, RoutedEventArgs e)
        {
            _upravovanySubjekt.Nazev = NazevTextBox.Text;
            _upravovanySubjekt.ico = IcoTextBox.Text;
            _upravovanySubjekt.ZapisDatum = DatumTextBox.Text;

            this.DialogResult = true;
            this.Close();
        }
    }
}
