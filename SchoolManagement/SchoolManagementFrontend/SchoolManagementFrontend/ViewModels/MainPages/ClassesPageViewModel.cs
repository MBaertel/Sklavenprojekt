using SchoolManagementDomain.Core.Models.Classes;
using SchoolManagementDomain.Core.Models.Students;
using SchoolManagementFrontend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.ViewModels
{
    internal class ClassesPageViewModel
    {
        private readonly IBackendService _backendService;

        public ObservableCollection<Class> Classes { get; set; }

        public ClassesPageViewModel(IBackendService backendService)
        {
            _backendService = backendService;
        }

        public async Task Initialize()
        {
            var classes = await _backendService.GetClasses();
            foreach (var item in classes)
            {
                Classes.Add(item);
            }
        }
    }
}
