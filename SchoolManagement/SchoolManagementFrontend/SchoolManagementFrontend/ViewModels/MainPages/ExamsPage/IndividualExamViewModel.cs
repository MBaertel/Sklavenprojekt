using Avalonia.Controls.Primitives;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using SchoolManagementDomain.Core.Models.Exams;
using SchoolManagementFrontend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchoolManagementFrontend.ViewModels.MainPages.ExamsPage
{
    public class IndividualExamViewModel : ViewModelBase
    {
        private IndividualExam exam;
        private IBackendService _backendService;

        public string StudentName => exam.Student.Name;

        public string SubjectName => exam.BaseExam.Subject.BaseSubject.Name;
        public string TeacherName => exam.BaseExam.Subject.Teacher.Name;

        private int? _score;
        public int? Score
        {
            get => _score;
            set => SetProperty(ref _score, value);
        }

        private string _grade;
        public string Grade
        {
            get => _grade;
            set => SetProperty(ref _grade, value);
        }

        private DateTime? _handedInAt;
        public DateTime? HandedInAt
        {
            get => _handedInAt;
            set => SetProperty(ref _handedInAt, value);
        }


        public bool Finalized => exam.Finalized;

        private string _notes;
        public string Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }

        public ObservableCollection<ExamImages> Images { get; } = new();

        public ICommand SaveCommand { get; }
        public ICommand FinalizeCommand { get; }
        public ICommand UploadImageCommand { get; }
        public ICommand HandInCommand { get; }

        public IndividualExamViewModel(IndividualExam exam,IBackendService backendService)
        {
            this.exam = exam;
            Score = exam.Score;
            Grade = exam.Grade;
            HandedInAt = exam.HandedInAt;
            Notes = exam.Notes;
            _backendService = backendService;

            UploadImageCommand = new RelayCommand(UploadImage);
            SaveCommand = new RelayCommand(Save);
            FinalizeCommand = new RelayCommand(Finalize);
            HandInCommand = new RelayCommand(HandIn);
        }

        public async Task LoadImages()
        {
            Images.Clear();
            var images = await _backendService.GetExamImages(exam.Id);
            foreach (var image in images)
            {
                Images.Add(image);
            }
        }

        private async void UploadImage()
        {
            if (exam.Finalized) return;
            var options = new FilePickerOpenOptions
            {
                Title = "Select Images",
                AllowMultiple = true,
                FileTypeFilter = new[]
                {
                    FilePickerFileTypes.ImageAll
                }
            };

            var files = await App.MainWindow.StorageProvider.OpenFilePickerAsync(options);
            
            if(!files.Any()) return;

            foreach (var file in files)
            {
                await using var stream = await file.OpenReadAsync();
                using var ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                var data = ms.ToArray();

                var examImage = new ExamImages()
                {
                    Exam = exam,
                    Name = file.Name,
                };

                _backendService.UploadExamImage(examImage, data);
            }
        }

        private async void Finalize()
        {
            if (string.IsNullOrEmpty(exam.Grade)) return;
            exam.Grade = Grade;
            exam.Score = Score;
            exam.HandedInAt = HandedInAt;
            exam.Finalized = true;
            exam.Notes = Notes;
            await _backendService.UpdateIndividualExam(exam.Id, exam);
        }

        private async void Save()
        {
            if(!exam.Finalized)
            {
                exam.Grade = Grade;
                exam.Score = Score;
                exam.HandedInAt = HandedInAt;
            }
            exam.Notes = Notes;
            await _backendService.UpdateIndividualExam(exam.Id,exam);
        }

        private async void HandIn()
        {
            HandedInAt = DateTime.Now;
            Save();
        }
    }
}
