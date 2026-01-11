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
using SchoolManagementFrontend.ViewModels;
using SchoolManagementFrontend.Views;
using System;
using System.Linq;

namespace SchoolManagementFrontend
{
    public partial class App : Application
    {
        public IServiceProvider Services { get; private set; }
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
            services.AddScoped<IAuthenticator, DesktopAuthService>();
#endif
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<ExamsPageViewModel>();

            Services = services.BuildServiceProvider();

            using(var sp = Services.CreateScope())
            {
                var pageRegistry = sp.ServiceProvider.GetRequiredService<IPageRegistry>();

                pageRegistry.RegisterPage(new ExamsPageDescriptor());
            }
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
                // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
                DisableAvaloniaDataAnnotationValidation();
                var vm = Services.GetRequiredService<MainWindowViewModel>();
                var mainView = new MainView()
                {
                    DataContext = vm
                };

                desktop.MainWindow = mainView;
                
                mainView.CheckAuthOnLaunch();
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