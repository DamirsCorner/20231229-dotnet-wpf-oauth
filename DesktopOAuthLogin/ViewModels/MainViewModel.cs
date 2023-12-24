using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopOAuthLogin.Services;

namespace DesktopOAuthLogin.ViewModels;

public partial class MainViewModel(IOAuthService oAuthService) : ObservableObject
{
    [ObservableProperty]
    private string accessToken = string.Empty;

    [RelayCommand]
    private async Task Login()
    {
        try
        {
            var response = await oAuthService.Login();
            this.AccessToken = response.AccessToken;
        }
        catch (TaskCanceledException) { }
    }
}
