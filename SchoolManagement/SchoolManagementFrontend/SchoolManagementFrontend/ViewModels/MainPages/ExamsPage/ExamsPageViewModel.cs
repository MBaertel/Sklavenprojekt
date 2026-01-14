using Avalonia.Controls;
using SchoolManagementDomain.Core.Models.Classes;
using SchoolManagementDomain.Core.Models.Exams;
using SchoolManagementFrontend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.ViewModels.MainPages.ExamsPage
{
    public class ExamsPageViewModel : ViewModelBase
    {
        private readonly IBackendService _backendService;
        public ObservableCollection<ClassGroupViewModel> ClassGroups { get; set; } = new();

        private ClassExamViewModel _selectedExam;

        public ClassExamViewModel SelectedExam
        {
            get => _selectedExam;
            set
            {
                SetProperty(ref _selectedExam, value);
                if (value != null) value.Load();
            }
        }

        public ExamsPageViewModel(IBackendService backendService)
        {
            _backendService = backendService;
            Load();
        }

        public async Task Load()
        {
            var exams = await _backendService.GetClassExams(teacherId: Guid.Parse("95744a52-7107-49be-836e-72505ff04e05"));
            ClassGroups.Clear();

            var allExams = exams.Select(x => new ClassExamViewModel(x, _backendService));

            var grouped = allExams
                .GroupBy(e => e.ClassName) // Group by class
                .Select(classGroup => new ClassGroupViewModel(
                    classGroup.Key,
                    classGroup
                        .GroupBy(e => e.SubjectName)
                        .OrderBy(x => x.Key)// Then group by subject
                        .Select(subjectGroup => new SubjectGroupViewModel(subjectGroup.Key,subjectGroup))
                ))
                .OrderBy(x => x.ClassName);
            ClassGroups = new ObservableCollection<ClassGroupViewModel>(grouped);
        }
    }
}
