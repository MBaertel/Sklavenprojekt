using SchoolManagementDomain.Core.Models.Students;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.ViewModels
{
    internal class ClassViewModel : ViewModelBase
    {
        public ObservableCollection<Student> Students { get; set; }
    }
}
