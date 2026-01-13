using SchoolManagementDomain.Core.Models.Subjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.ViewModels.MainPages.ExamsPage
{
    public class ClassGroupViewModel : ViewModelBase
    {
        public string ClassName { get; set; }
        public ObservableCollection<SubjectGroupViewModel> Subjects { get; set; }

        private bool _isExpanded = false;
        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetProperty(ref _isExpanded, value);
        }

        public ClassGroupViewModel(string className, IEnumerable<SubjectGroupViewModel> subjects)
        {
            ClassName = className;
            Subjects = new ObservableCollection<SubjectGroupViewModel>(subjects);
        }
    }
}
