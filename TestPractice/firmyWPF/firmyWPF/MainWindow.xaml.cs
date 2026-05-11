using firmyWPF.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace firmyWPF
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

        public async void OnSubmitClick(object sender, RoutedEventArgs e)
        {
            ErrorText.Text = "";
            string ico = IcoTextBox.Text.Trim();

            if(string.IsNullOrEmpty(ico))
            {
                ErrorText.Text = "Zadejte ico";
                return;
            }
            string url = @"https://ares.gov.cz/ekonomicke-subjekty-v-be/rest/ekonomicke-subjekty/61989100";
            var response = await _httpClient.GetAsync(url);
            if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorText.Text = "Firma neexistuje";
                return;
            }

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<JsonModel>(responseString);

            var dialog = new CompanyDialog(
                json.obchodniJmeno,
                json.dic,
                json.sidlo?.nazevObce
                )
            {
                Owner = this
            };
            dialog.ShowDialog();
        }
    }
}