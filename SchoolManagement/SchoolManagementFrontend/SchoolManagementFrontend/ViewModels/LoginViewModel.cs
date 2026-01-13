using CommunityToolkit.Mvvm.Input;
using SchoolManagementFrontend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchoolManagementFrontend.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticator _authService;

        private string _errorMsg;
        public string ErrorMsg
        {
            get => _errorMsg; 
            set => SetProperty(ref _errorMsg, value);
        }

        public event EventHandler<bool> LoginSuccessful;

        public ICommand LoginCommand { get; }

        public Action CloseAction { get; set; }

        public LoginViewModel(IAuthenticator authService)
        {
            _authService = authService;
            LoginCommand = new RelayCommand(async () => await Login());
        }

        private async Task Login()
        {
            var success = await _authService.Login();
            if(!success)
            {
                ErrorMsg = "Login Failed";
                return;
            }
            else
            {
                LoginSuccessful?.Invoke(this, true);
            }

            CloseAction?.Invoke();
        }
    }
}
