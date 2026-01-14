using CommunityToolkit.Mvvm.Input;
using ExCSS;
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

namespace SchoolManagementFrontend.ViewModels.Overlays
{
    public class ClassExamOverlayViewModel : ViewModelBase, IOverlay
    {
        private readonly IBackendService _backendService;
        public event Action RequestClose;
        public string Title => IsCreating ? "Create Class Exam" : "Edit Class Exam";
        
        public bool IsCreating { get; }

        private ClassExam _exam;
        public ClassExam Exam
        {
            get => _exam;
            set => SetProperty(ref _exam, value);
        }

        public ObservableCollection<Subject> Subjects { get; } = new();
        public ObservableCollection<Class> Classes { get; } = new();

        private Class _selectedClass;
        public Class SelectedClass
        {
            get => _selectedClass;
            set
            {
                SetProperty(ref _selectedClass, value);
                OnClassSelected();
            }
        }

        private Subject _selectedSubject;
        public Subject SelectedSubject
        {
            get => _selectedSubject;
            set => SetProperty(ref _selectedSubject, value);
        }

        private string _examTitle;
        public string ExamTitle
        {
            get => _examTitle;
            set => SetProperty(ref _examTitle, value);
        }

        private DateTimeOffset _date = DateTimeOffset.Now;
        public DateTimeOffset Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public ICommand SaveCommand { get; }

        public ICommand CloseCommand { get; }

        public ClassExamOverlayViewModel(IBackendService backendService, ClassExam exam = null)
        {
            _backendService = backendService;
            SaveCommand = new RelayCommand(Save);
            CloseCommand = new RelayCommand(Close);
            LoadClasses().Wait();

            if (exam == null)
            {
                Exam = new ClassExam();
                IsCreating = true;
            }
            else
            {
                Exam = exam;
                IsCreating = false;
                if(Exam.Class != null) SelectedClass = Exam.Class;
                if(Exam.Subject != null) SelectedSubject = Exam.Subject;
                if(Exam.Name != null) ExamTitle = Exam.Name;
                if(Exam.Date != null) Date = new DateTimeOffset(Exam.Date);
            }
        }

        public ClassExamOverlayViewModel(IBackendService backendSerice,Class @class = null,Subject subject = null)
        {
            _backendService = backendSerice;
            IsCreating = true;

            SaveCommand = new RelayCommand(Save);
            CloseCommand = new RelayCommand(Close);
            LoadClasses().Wait();

            Exam = new ClassExam();

            if (@class != null)
            {
                SelectedClass = @class;
                SelectedSubject = subject;
            }
        }

        private async Task LoadClasses()
        {
            Classes.Clear();
            var classes = await _backendService.GetClasses(teacherId: MockDataProvider.Teachers.First().Id);
            foreach (var item in classes)
            {
                Classes.Add(item);
            }
        }

        private async Task OnClassSelected()
        {
            Subjects.Clear();
            var subjects = await _backendService.GetSubjects(teacherId: MockDataProvider.Teachers.First().Id);
            subjects = subjects.Where(x => x.Class.Id == _selectedClass.Id).ToList();
            foreach (var item in subjects)
            {
                Subjects.Add(item);
            }
        }

        private void Save()
        {
            if (string.IsNullOrEmpty(ExamTitle)) return;
            if (SelectedClass == null) return;
            if (SelectedSubject == null) return;

            Exam.Subject = SelectedSubject;
            Exam.Class = SelectedSubject.Class;
            Exam.Name = ExamTitle;
            Exam.Date = Date.Date;

            if (IsCreating)
            {
                _backendService.CreateClassExam(Exam);
            }
            else
            {
                _backendService.UpdateClassExam(Exam.Id, Exam);
            }

            RequestClose?.Invoke();    
        }

        public void Close()
        {
            RequestClose?.Invoke();
        }
    }
}
