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
using SchoolManagementFrontend.ViewModels.MainPages;
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
            services.AddSingleton<IBackendService, MockBackendService>();

            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<ExamsPageViewModel>();

            Services = services.BuildServiceProvider();

            using(var sp = services.BuildServiceProvider())
            {
                var pageRegistry = sp.GetRequiredService<IPageRegistry>();
            }
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
                // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
                DisableAvaloniaDataAnnotationValidation();
                desktop.MainWindow = new MainWindow();
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