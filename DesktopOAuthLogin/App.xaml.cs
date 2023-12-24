using System.Windows;
using DesktopOAuthLogin.Services;
using DesktopOAuthLogin.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace DesktopOAuthLogin;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static new App Current => (App)Application.Current;

    public IServiceProvider Services { get; }

    public App()
    {
        this.Services = ConfigureServices();
        this.InitializeComponent();
    }

    private static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddTransient<IOAuthService, OAuthService>();
        services.AddTransient<MainViewModel>();

        return services.BuildServiceProvider();
    }
}
