using PostmanCloneLibrary;
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

namespace PostmanCloneUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        private readonly ApiAccess api = new ApiAccess();

        public Dashboard()
        {
            InitializeComponent();
        }

        private async void callApi_Click(object sender, RoutedEventArgs e)
        {
            // Validate the API URL
            if(api.IsValidUrl(apiText.Text) == false)
            {
                statusTextBlock.Text = "Invalid URL";
                return;
            }

            try
            {
                statusTextBlock.Text = "Calling API...";

                resultsText.Text = await api.CallApiAsync(apiText.Text);

                statusTextBlock.Text = "Ready";
            }
            catch (Exception ex)
            {
                resultsText.Text = "Error: " + ex.Message;
                statusTextBlock.Text = "Error";
            }
        }
    }
}