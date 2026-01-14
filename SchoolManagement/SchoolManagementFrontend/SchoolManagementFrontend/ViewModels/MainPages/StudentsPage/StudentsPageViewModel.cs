using CommunityToolkit.Mvvm.Input;
using SchoolManagementDomain.Core.Models.Exams;
using SchoolManagementDomain.Core.Models.Students;
using SchoolManagementDomain.Core.Models.Teachers;
using SchoolManagementFrontend.Services.Interface;
using SchoolManagementFrontend.Services.Mock;
using SchoolManagementFrontend.ViewModels.MainPages.ExamsPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchoolManagementFrontend.ViewModels.MainPages.StudentsPage
{
    public class StudentsPageViewModel : ViewModelBase
    {
        private readonly IBackendService _backendService;

        public ObservableCollection<ClassGroupViewModel> Classes { get; } = new();
        public StudentsPageViewModel(IBackendService backendService)
        {
            _backendService = backendService;
            DeselectCommand = new RelayCommand(() => SelectedIndividualExam = null);
            Load();
        }

        private StudentViewModel _selectedStudent;
        public StudentViewModel SelectedStudent
        {
            get => _selectedStudent;
            set => SetProperty(ref _selectedStudent, value);
        }

        private IndividualExamViewModel _selectedIndividualExam;
        public IndividualExamViewModel SelectedIndividualExam
        {
            get => _selectedIndividualExam;
            set
            {
                SetProperty(ref _selectedIndividualExam, value);
                if (value != null) value.LoadImages();
                SelectedExamVisible = value == null ? false : true;
            }
        }

        private bool _selectedExamVisible = false;
        public bool SelectedExamVisible
        {
            get => _selectedExamVisible;
            set => SetProperty(ref _selectedExamVisible, value);
        }

        public ICommand? DeselectCommand { get; }

        private async void Load()
        {
            var classes = await _backendService.GetClasses(teacherId: MockDataProvider.Teachers.First().Id);
            foreach (var item in classes)
            {
                Classes.Add(new ClassGroupViewModel(_backendService, item));
            }
        }
    }
}
