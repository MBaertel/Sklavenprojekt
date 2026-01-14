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
    public class ClassExamViewModel : ViewModelBase
    {
        private readonly IBackendService _backendService;

        public string Name => exam.Name;
        public DateTime Date => exam.Date;
        public int Count => IndividualExams.Count;
        public double Average => IndividualExams.Any() ? Math.Round(IndividualExams.Average(x => x.Score),2) : 0;
        public string StatusString => exam.Open ? "Offen" : "Abgeschlossen";
        public string SubjectName => exam.Subject.BaseSubject.Name;
        public string ClassName => exam.Class.Name;
  
        private bool _detailsVisible; public bool DetailsVisible 
        { 
            get => _detailsVisible; 
            set 
            { 
                SetProperty(ref _detailsVisible, value); 
                if (value == true) Load(); 
            } 
        }

        public ObservableCollection<IndividualExamViewModel> IndividualExams { get; set; } = new ObservableCollection<IndividualExamViewModel>(); 

        private ClassExam exam; 

        public ClassExamViewModel(ClassExam exam, IBackendService backendService) 
        {
            this.exam = exam; 
            this._backendService = backendService;
        }

        public async Task Load() 
        { 
            IndividualExams.Clear(); 
            var exams = await _backendService.GetIndividualExams(exam.Id); 
            foreach (var item in exams) 
            { 
                IndividualExams.Add(new IndividualExamViewModel(item)); 
            } 
        }
    }
}
