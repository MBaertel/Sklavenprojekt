using Avalonia.Styling;
using CommunityToolkit.Mvvm.Input;
using SchoolManagementFrontend.Types;
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
        public ObservableCollection<Page> Pages { get; set; }

        private Page _currentPage;
        public Page CurrentPage
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

            Pages = new ObservableCollection<Page>()
            {
                SettingsViewModel.PAGE
            };

            CurrentPage = Pages[0];
        }
    }
}
