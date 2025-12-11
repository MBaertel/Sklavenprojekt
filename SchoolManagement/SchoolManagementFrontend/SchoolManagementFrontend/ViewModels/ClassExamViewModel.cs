using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagementDomain.Core.Models.Exams;
using SchoolManagementFrontend.Services;
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
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public ObservableCollection<IndividualExam> Exams { get; set; }


        public ClassExamViewModel()
        {
        }
    }
}
