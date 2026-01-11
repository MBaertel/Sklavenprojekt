using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagementDomain.Core.Models.Exams;
using SchoolManagementFrontend.Services;
using SchoolManagementFrontend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.ViewModels
{
    public class ClassExamViewModel : ViewModelBase
    {
        private readonly IBackendService _backendService;

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        private bool _detailsVisible;
        public bool DetailsVisible
        {
            get => _detailsVisible;
            set
            {
                SetProperty(ref _detailsVisible, value);
                if (value == true) Load();
            }
        }

        public ObservableCollection<IndividualExam> Exams { get; set; } = new ObservableCollection<IndividualExam>();

        private ClassExam exam;

        public ClassExamViewModel(ClassExam exam,IBackendService backendService)
        {
            _name = exam.Name;
            _date = exam.Date;
            _backendService = backendService;
            this.exam = exam;
        }

        public async Task Load()
        {
            Exams.Clear();
            var exams = await _backendService.GetIndividualExams(exam.Id);
            foreach (var item in exams)
            {
                Exams.Add(item);
            }
        }
    }
}
