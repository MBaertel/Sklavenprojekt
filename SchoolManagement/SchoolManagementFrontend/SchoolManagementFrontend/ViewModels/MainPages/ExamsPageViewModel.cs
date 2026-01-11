using SchoolManagementDomain.Core.Models.Exams;
using SchoolManagementFrontend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.ViewModels
{
    public class ExamsPageViewModel : ViewModelBase
    {
        private readonly IBackendService _backendService;
        public ObservableCollection<ClassExamViewModel> ClassExams { get; set; } = new();

        public ExamsPageViewModel(IBackendService backendService)
        {
            _backendService = backendService;
            Load();
        }

        public async Task Load()
        {
            var exams = await _backendService.GetClassExams(teacherId: Guid.Parse("16d60e87-d96f-47fd-a725-d5ef2f39b258"));
            ClassExams.Clear();
            foreach (var exam in exams)
            {
                var vm = new ClassExamViewModel(exam,_backendService);
                ClassExams.Add(vm);
            }
        }
    }
}
