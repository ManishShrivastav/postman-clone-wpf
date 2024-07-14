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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void callApi_Click(object sender, RoutedEventArgs e)
        {
            // Validate the API URL

            try
            {
                statusTextBlock.Text = "Calling API...";

                // Sample code - replace with the actual API call
                await Task.Delay(2000);

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