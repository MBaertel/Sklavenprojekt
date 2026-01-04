using Avalonia.Styling;
using CommunityToolkit.Mvvm.Input;
using SchoolManagementFrontend.Pages;
using SchoolManagementFrontend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchoolManagementFrontend.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IAuthenticator _authenticator;
        public IAuthenticator Authenticator => _authenticator;
        public ObservableCollection<IPageDescriptor> Pages { get; set; }

        private IPageDescriptor _currentPage;
        public IPageDescriptor CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        private bool _isMenuOpen;
        public bool IsMenuOpen
        {
            get => _isMenuOpen;
            set => SetProperty(ref _isMenuOpen, value);
        }

        public ICommand ToggleMenu { get; }

        public MainWindowViewModel(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
            ToggleMenu = new RelayCommand(() => IsMenuOpen = !IsMenuOpen);
        }
    }
}
