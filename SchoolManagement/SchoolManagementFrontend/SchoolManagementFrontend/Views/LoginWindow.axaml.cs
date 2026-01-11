using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SchoolManagementFrontend.ViewModels;

namespace SchoolManagementFrontend;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        AvaloniaXamlLoader.Load(this);

        if (DataContext is LoginViewModel vm)
            vm.CloseAction = Close;
    }
}