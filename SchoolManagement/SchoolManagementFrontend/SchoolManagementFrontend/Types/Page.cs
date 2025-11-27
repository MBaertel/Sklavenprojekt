using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.Types
{
    public class Page
    {
        public string Title { get; }

        public string IconPath { get; }
        public object ViewModel { get; }

        public Page(string title,string iconPath, object viewModel)
        {
            Title = title;
            IconPath = iconPath;
            ViewModel = viewModel;
        }
    }
}
