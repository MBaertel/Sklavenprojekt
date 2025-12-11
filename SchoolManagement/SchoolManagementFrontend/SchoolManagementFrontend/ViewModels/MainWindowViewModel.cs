using Avalonia.Styling;
using CommunityToolkit.Mvvm.Input;
using SchoolManagementFrontend.Pages;
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

        public MainWindowViewModel()
        {
            ToggleMenu = new RelayCommand(() => IsMenuOpen = !IsMenuOpen);
        }
    }
}
