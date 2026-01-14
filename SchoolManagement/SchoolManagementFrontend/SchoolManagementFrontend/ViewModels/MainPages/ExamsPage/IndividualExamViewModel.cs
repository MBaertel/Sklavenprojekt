using SchoolManagementDomain.Core.Models.Exams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.ViewModels.MainPages.ExamsPage
{
    public class IndividualExamViewModel
    {
        private IndividualExam exam;

        public string StudentName => exam.Student.Name;
        public int Score => exam.Score;

        public string Grade => exam.Grade;

        public string HandedInAt => exam.HandedInAt.ToString();

        public IndividualExamViewModel(IndividualExam exam)
        {
            this.exam = exam;
        }
    }
}
