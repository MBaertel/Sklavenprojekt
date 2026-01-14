using SchoolManagementDomain.Core.Models.Classes;
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
        private Class _class;
        public string ClassName => _class.Name;
        public ObservableCollection<SubjectGroupViewModel> Subjects { get; set; }

        private bool _isExpanded = false;
        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetProperty(ref _isExpanded, value);
        }

        public ClassGroupViewModel(Class @class, IEnumerable<SubjectGroupViewModel> subjects)
        {
            _class = @class;
            Subjects = new ObservableCollection<SubjectGroupViewModel>(subjects);
        }
    }
}
