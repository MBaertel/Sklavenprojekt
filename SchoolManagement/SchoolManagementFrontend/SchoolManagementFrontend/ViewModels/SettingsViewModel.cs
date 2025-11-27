using SchoolManagementFrontend.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public static Page PAGE => new Page("Settings", "/Assets/Icons/settingsIcon.svg", new SettingsViewModel());

        private bool _darkModeEnabled;
        public bool DarkModeEnabled
        {
            get => _darkModeEnabled;
            set => SetProperty(ref _darkModeEnabled, value);
        }
    }
}
