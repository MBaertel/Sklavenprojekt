using SchoolManagementDomain.Core.Models.Exams;
using SchoolManagementFrontend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.ViewModels.MainPages
{
    public class ExamsPageViewModel : ViewModelBase
    {
        private readonly IBackendService _backendService;
        public ObservableCollection<ClassExamViewModel> ClassExams { get; set; } = new();

        public ExamsPageViewModel(IBackendService backendService)
        {
            _backendService = backendService;
        }
    }
}
