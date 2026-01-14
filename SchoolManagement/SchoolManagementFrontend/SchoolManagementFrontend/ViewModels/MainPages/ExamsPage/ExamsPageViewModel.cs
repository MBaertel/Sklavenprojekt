using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using SchoolManagementDomain.Core.Models.Classes;
using SchoolManagementDomain.Core.Models.Exams;
using SchoolManagementDomain.Core.Models.Subjects;
using SchoolManagementFrontend.Services.Interface;
using SchoolManagementFrontend.Services.Mock;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        private IndividualExamViewModel _selectedIndividualExam;
        public IndividualExamViewModel SelectedIndividualExam
        {
            get => _selectedIndividualExam;
            set
            {
                SetProperty(ref _selectedIndividualExam, value);
                if(value != null) value.LoadImages();
                SelectedExamVisible = value == null? false : true;
            }
        }

        private bool _selectedExamVisible = false;
        public bool SelectedExamVisible
        {
            get => _selectedExamVisible;
            set => SetProperty(ref _selectedExamVisible, value);
        }

        public ICommand? DeselectCommand { get; }

        public ExamsPageViewModel(IBackendService backendService)
        {
            _backendService = backendService;
            _backendService.NewObject += () => Load();
            DeselectCommand = new RelayCommand(() => SelectedIndividualExam=null);
            Load();
        }

        public async Task Load()
        {
            var exams = await _backendService.GetClassExams(teacherId: MockDataProvider.Teachers.First().Id);
            ClassGroups.Clear();

            var allExams = exams.Select(x => new ClassExamViewModel(x, _backendService));

            var grouped = allExams
                .GroupBy(vm => vm.Class)
                .OrderBy(g => g.Key.Name)
                .Select(classGroup =>
                    new ClassGroupViewModel(
                        classGroup.Key,
                        classGroup
                            .GroupBy(vm => vm.Subject)
                            .OrderBy(g => g.Key.BaseSubject.Name)
                            .Select(subjectGroup =>
                                new SubjectGroupViewModel(
                                    subjectGroup.Key,
                                    subjectGroup
                                        .OrderBy(vm => vm.Date),
                                    _backendService
                                )
                            )
                    )
                );


            foreach (var item in grouped)
            {
                ClassGroups.Add(item);
            }
        }
    }
}
