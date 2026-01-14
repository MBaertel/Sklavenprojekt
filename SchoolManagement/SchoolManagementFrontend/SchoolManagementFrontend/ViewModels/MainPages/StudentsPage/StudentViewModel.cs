using SchoolManagementDomain.Core.Models.Students;
using SchoolManagementFrontend.Services.Interface;
using SchoolManagementFrontend.ViewModels.MainPages.ExamsPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.ViewModels.MainPages.StudentsPage
{
    public class StudentViewModel
    {
        private readonly IBackendService _backendService;
        private Student _student;

        public string StudentName => _student.Name;

        public ObservableCollection<IndividualExamViewModel> Exams { get; } = new();
        public StudentViewModel(IBackendService backendService, Student student)
        {
            _backendService = backendService;
            _student = student;
            Load();
        }

        private async void Load()
        {
            Exams.Clear();
            var exams = await _backendService.GetIndividualExams(studentId: _student.Id);
            foreach (var exam in exams)
            {
                Exams.Add(new IndividualExamViewModel(exam, _backendService));
            }
        }
    }
}
