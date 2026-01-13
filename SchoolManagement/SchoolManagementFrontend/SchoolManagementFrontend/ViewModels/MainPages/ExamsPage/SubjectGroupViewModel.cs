using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.ViewModels.MainPages.ExamsPage
{
    public class SubjectGroupViewModel
    {
        public string SubjectName { get; set; }
        public ObservableCollection<ClassExamViewModel> Exams { get; set; }

        public SubjectGroupViewModel(string subjectName,IEnumerable<ClassExamViewModel> exams) 
        {
            SubjectName = subjectName;
            Exams = new ObservableCollection<ClassExamViewModel>(exams);
        }
    }
}
