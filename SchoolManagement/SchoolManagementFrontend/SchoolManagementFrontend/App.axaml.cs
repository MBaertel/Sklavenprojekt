using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagementFrontend.Pages;
using SchoolManagementFrontend.Services;
using SchoolManagementFrontend.Services.Interface;
using SchoolManagementFrontend.Services.Mock;
using SchoolManagementFrontend.ViewModels;
using SchoolManagementFrontend.ViewModels.MainPages.ExamsPage;
using SchoolManagementFrontend.ViewModels.MainPages.StudentsPage;
using System;
using System.Linq;

namespace SchoolManagementFrontend
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }

        public static MainWindow MainWindow { get; private set; }
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            Configure();
        }

        private void Configure()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IPageRegistry,PageRegistry>();
#if DEBUG
            services.AddSingleton<IBackendService, MockBackendService>();
            services.AddSingleton<ITokenStore, MockTokenStore>();
            services.AddScoped<IAuthenticator, MockAuthService>();
            services.AddScoped<IOverlayService, OverlayService>();
#endif
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MainPageViewModel>();
            services.AddSingleton<ExamsPageViewModel>();
            services.AddSingleton<StudentsPageViewModel>();
            services.AddSingleton<LoginViewModel>();

            Services = services.BuildServiceProvider();

            var pageRegistry = Services.GetRequiredService<IPageRegistry>();
            pageRegistry.RegisterPage(new ExamsPageDescriptor());
            pageRegistry.RegisterPage(new StudentsPageDescriptor());
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                DisableAvaloniaDataAnnotationValidation();
                var vm = Services.GetRequiredService<MainWindowViewModel>();
                var mainView = new MainWindow()
                {
                    DataContext = vm
                };

                desktop.MainWindow = mainView;
                MainWindow = mainView;
            }
            else if(ApplicationLifetime is ISingleViewApplicationLifetime singleView)
            {
                DisableAvaloniaDataAnnotationValidation();
                var vm = Services.GetRequiredService<MainWindowViewModel>();
                var mainView = new MainWindow()
                {
                    DataContext = vm
                };

                singleView.MainView = mainView;
                MainWindow = mainView;
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void DisableAvaloniaDataAnnotationValidation()
        {
            // Get an array of plugins to remove
            var dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            // remove each entry found
            foreach (var plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }
        }
    }
}