using SchoolManagementDomain.Core.Models.Exams;
using SchoolManagementDomain.Core.Models.Students;
using SchoolManagementDomain.Core.Models.Teachers;
using SchoolManagementFrontend.Services.Interface;
using SchoolManagementFrontend.ViewModels.MainPages.ExamsPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.ViewModels.MainPages.StudentsPage
{
    public class StudentsPageViewModel : ViewModelBase
    {
        private readonly IBackendService _backendService;

        public ObservableCollection<ClassGroupViewModel> Classes { get; } = new();
        public StudentsPageViewModel(IBackendService backendService)
        {
            _backendService = backendService;
            Load();
        }

        private async void Load()
        {
            var classes = await _backendService.GetClasses(teacherId: Guid.Parse("95744a52-7107-49be-836e-72505ff04e05"));
            foreach (var item in classes)
            {
                Classes.Add(new ClassGroupViewModel(_backendService, item));
            }
        }
    }
}
