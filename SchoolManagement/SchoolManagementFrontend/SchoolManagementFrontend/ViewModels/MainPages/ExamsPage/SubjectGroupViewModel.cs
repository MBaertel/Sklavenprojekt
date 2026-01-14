using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagementDomain.Core.Models.Subjects;
using SchoolManagementFrontend.Services.Interface;
using SchoolManagementFrontend.ViewModels.Overlays;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchoolManagementFrontend.ViewModels.MainPages.ExamsPage
{
    public class SubjectGroupViewModel
    {
        private readonly IBackendService _backendService;
        private Subject subject;

        public string SubjectName => subject.BaseSubject.Name;
        public string Teacher => subject.Teacher.Name;

        public ICommand PlanExamCommand { get; }

        public ObservableCollection<ClassExamViewModel> Exams { get; set; }

        public SubjectGroupViewModel(Subject subject,IEnumerable<ClassExamViewModel> exams,IBackendService backendService) 
        {
            _backendService = backendService;
            this.subject = subject;
            Exams = new ObservableCollection<ClassExamViewModel>(exams);

            PlanExamCommand = new RelayCommand(() => App.Services.GetRequiredService<IOverlayService>()
                .ShowOverlay(new ClassExamOverlayViewModel(_backendService, subject.Class, subject)));
        }
    }
}
