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
using static PostmanCloneLibrary.Enums;

namespace PostmanCloneUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        private readonly IApiAccess api = new ApiAccess();

        public Dashboard()
        {
            InitializeComponent();
            httpVerbSelection.SelectedIndex = 0;
        }

        private async void callApi_Click(object sender, RoutedEventArgs e)
        {
            statusTextBlock.Text = "Calling API...";
            resultsText.Text = "";

            // Validate the API URL
            if (api.IsValidUrl(apiText.Text) == false)
            {
                statusTextBlock.Text = "Invalid URL";
                return;
            }

            HttpAction action;
            string selectedVerb = (httpVerbSelection.SelectedItem as ComboBoxItem)?.Content.ToString();

            switch (selectedVerb)
            {
                case "GET":
                    action = HttpAction.GET;
                    break;
                case "POST":
                    action = HttpAction.POST;
                    break;
                default:
                    statusTextBlock.Text = "Invalid HTTP Verb";
                    return;
            }

            try
            {
                resultsText.Text = await api.CallApiAsync(apiText.Text, bodyText.Text , action);
                callData.SelectedIndex = 1;


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