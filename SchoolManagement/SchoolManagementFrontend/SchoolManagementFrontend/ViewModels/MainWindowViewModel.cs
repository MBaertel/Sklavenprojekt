using SchoolManagementFrontend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly LoginViewModel _loginViewModel;
        private readonly MainPageViewModel _mainPageViewModel;

        private ViewModelBase _currentView;
        public ViewModelBase CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public MainWindowViewModel(IAuthenticator authenticator,MainPageViewModel mainVm,LoginViewModel loginVm)
        {
            this._authenticator = authenticator;
            this._loginViewModel = loginVm;
            this._mainPageViewModel = mainVm;

            _loginViewModel.LoginSuccessful += OnLoginSucceeded;
            Initialize();
        }

        private bool _overlayVisible = false;
        public bool OverlayVisible
        {
            get => _overlayVisible;
            set => SetProperty(ref _overlayVisible, value);
        }

        private void OnLoginSucceeded(object? sender, bool e)
        {
            CurrentView = _mainPageViewModel;
        }

        private async void Initialize()
        {
            CurrentView = await UserIsLoggedIn() ? _mainPageViewModel : _loginViewModel;
        }

        private async Task<bool> UserIsLoggedIn()
        {
            return await _authenticator.HasStoredCredentials();
        }
    }
}
