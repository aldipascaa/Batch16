using System.Windows;
using System.Windows.Controls;

namespace ReDominoWPF.Pages
{
    public partial class LandingPage : Page
    {
        public LandingPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PlayerNameTextBox.Focus();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            string playerName = PlayerNameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(playerName))
            {
                MessageBox.Show("Please enter a player name.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int initialDominos = 7;
            if (Dominos5.IsChecked == true) initialDominos = 5;
            else if (Dominos6.IsChecked == true) initialDominos = 6;

            NavigationService.Navigate(new GamePage(playerName, initialDominos));
        }
    }
}