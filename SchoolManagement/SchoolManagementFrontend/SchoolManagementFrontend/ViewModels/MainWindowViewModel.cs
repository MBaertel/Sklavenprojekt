using Avalonia.Styling;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IPageRegistry _pageRegistry;
        private readonly IServiceProvider _serviceProvider;

        public IAuthenticator Authenticator => _authenticator;
        public ObservableCollection<IPageDescriptor> Pages { get; set; }

        private IPageDescriptor? _currentPage;
        public IPageDescriptor? CurrentPage
        {
            get => _currentPage;
            set 
            { 
                if(SetProperty(ref _currentPage, value))
                {
                    Navigate(value);
                }            
            } 
        }

        private object? _currentViewModel;
        public object? CurrentViewModel
        {
            get => _currentViewModel;
            private set => SetProperty(ref _currentViewModel, value);
        }

        private bool _isMenuOpen;
        public bool IsMenuOpen
        {
            get => _isMenuOpen;
            set => SetProperty(ref _isMenuOpen, value);
        }

        public ICommand ToggleMenu { get; }

        public MainWindowViewModel(IAuthenticator authenticator,IPageRegistry pageRegistry,IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _authenticator = authenticator;
            _pageRegistry = pageRegistry;
            _pageRegistry.PagesUpdated += OnPagesUpdated;

            Pages = new ObservableCollection<IPageDescriptor>(pageRegistry.Pages);

            ToggleMenu = new RelayCommand(() => IsMenuOpen = !IsMenuOpen);
        }

        private void OnPagesUpdated(object sender,IPageDescriptor e)
        {
            Pages.Clear();
            foreach (var page in _pageRegistry.Pages)
            {
                Pages.Add(page);
            }
        }

        private void Navigate(IPageDescriptor? page)
        {
            if (page is null) return;

            CurrentViewModel = _serviceProvider.GetRequiredService(page.ViewModelType);
        }
    }
}
