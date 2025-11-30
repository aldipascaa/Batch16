using System.Windows;

namespace ReDominoWPF;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainFrame.Navigate(new Pages.LandingPage());
    }
}