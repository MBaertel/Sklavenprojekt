using SchoolManagementFrontend.Services.Interface;
using SchoolManagementFrontend.ViewModels.Overlays;
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

        private IOverlay _currentOverlay;
        public IOverlay CurrentOverlay
        {
            get => _currentOverlay;
            set => SetProperty(ref _currentOverlay, value);
        }

        public MainWindowViewModel(IAuthenticator authenticator,MainPageViewModel mainVm,LoginViewModel loginVm)
        {
            this._authenticator = authenticator;
            this._loginViewModel = loginVm;
            this._mainPageViewModel = mainVm;

            _loginViewModel.LoginSuccessful += OnLoginSucceeded;
            Initialize();
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

        public void ShowOverlay(IOverlay overlay)
        {
            // Close any existing overlay
            CurrentOverlay?.Close();

            CurrentOverlay = overlay;

            overlay.RequestClose += () =>
            {
                overlay.RequestClose -= () => { }; // unsubscribe
                overlay.OnClosed();
                CurrentOverlay = null;
            };
        }

        public void CloseOverlay()
        {
            CurrentOverlay?.OnClosed();
            CurrentOverlay = null;
        }

    }
}
