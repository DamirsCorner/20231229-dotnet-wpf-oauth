using System.Windows;
using DesktopOAuthLogin.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace DesktopOAuthLogin;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
        this.DataContext = App.Current.Services.GetRequiredService<MainViewModel>();
    }
}
