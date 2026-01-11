using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using SchoolManagementFrontend.ViewModels;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.Views
{
    public partial class MainView : Window
    {
        public MainView()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public async void CheckAuthOnLaunch()
        {
            await Task.Delay(50);
            var vm = DataContext as MainWindowViewModel;
            var auth = vm?.Authenticator;

            if (auth == null)
                return;

            if(await auth.HasStoredCredentials() || await auth.TryRefresh())
                return;

            await Dispatcher.UIThread.InvokeAsync(async () =>
            {
                var loginVm = new LoginViewModel(auth);
                var loginWindow = new LoginWindow
                {
                    DataContext = loginVm,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };

                loginVm.CloseAction = loginWindow.Close;

                await loginWindow.ShowDialog(this);
            });
        }
    }
}