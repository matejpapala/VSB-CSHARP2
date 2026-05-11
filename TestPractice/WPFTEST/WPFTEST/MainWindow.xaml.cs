using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WPFTEST
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void PotvrditButton_Click(object sender,  RoutedEventArgs e)
        {
             try
            {
                var vybranaPolozka = (ComboBoxItem)MestoComboBox.SelectedItem;
                string nazevMesta = vybranaPolozka.Content.ToString();

                string mestoMale = nazevMesta.ToLower();

                string url =  $"https://dataor.justice.cz/api/file/zp-full-{mestoMale}-2024.xml";
                string xmlData = await _httpClient.GetStringAsync(url);

                XDocument xdoc = XDocument.Parse(xmlData);

                List<Subjekt> seznamSubjektu = new List<Subjekt>();

                var vsechnaIco = xdoc.Descendants().Where(x => x.Name.LocalName == "ico");

                foreach(var icoUzel in vsechnaIco)
                {
                    var rodic = icoUzel.Parent;

                    if(rodic != null)
                    {
                        string nazev = rodic.Elements().FirstOrDefault(e => e.Name.LocalName == "nazev")?.Value;
                        string ico = icoUzel.Value;
                        string zapisDatum = rodic.Elements().FirstOrDefault(e => e.Name.LocalName == "zapisDatum")?.Value;

                        seznamSubjektu.Add(new Subjekt
                        {
                            Nazev = nazev,
                            ico = ico,
                            ZapisDatum = zapisDatum,
                        });

                    }
                }
                VysledkyDataGrid.ItemsSource = seznamSubjektu;

            }
            catch(Exception ex)
            {
                
            }
        }

        private void UpravitButton_Click(object sender, RoutedEventArgs e)
        {
            Button kliknuteTlacitko = (Button)sender;
            Subjekt vybranySubjekt = (Subjekt)kliknuteTlacitko.DataContext;

            EditDialog dialog = new EditDialog(vybranySubjekt);

            if(dialog.ShowDialog() == true)
            {
                VysledkyDataGrid.Items.Refresh();
            }
        }

    }
}