using SchoolManagementDomain.Core.Models.Classes;
using SchoolManagementFrontend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.ViewModels.MainPages.StudentsPage
{
    public class ClassGroupViewModel
    {
        private readonly IBackendService _backendService;
        private Class _class;
        public string ClassName => _class.Name;
        public ObservableCollection<StudentViewModel> Students { get; set; } = new();

        public ClassGroupViewModel(IBackendService backendService,Class @class)
        {
            _class = @class;
            _backendService = backendService;
            Load();
        }

        private async void Load()
        {
            var students = await _backendService.GetStudents(classId: _class.Id);
            foreach (var item in students)
            {
                Students.Add(new StudentViewModel(_backendService,item));
            }
        }
    }
}
