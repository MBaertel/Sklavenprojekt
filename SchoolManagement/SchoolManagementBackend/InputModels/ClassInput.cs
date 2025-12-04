using SchoolManagementDomain.Core.Models.Teachers;
using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementBackend.InputModels
{
    public class ClassInput
    {
        public string Name { get; set; }

        public List<Guid> Students { get; set; }
        public List<Guid> Teachers { get; set; }

        public EFClass ToEF(List<EFClassStudent>? students = null, List<EFClassTeacher>? teachers = null)
        {
            var id = Guid.NewGuid(); 
            var name = Name;
            var cs = students;
            var ct = teachers;

            return new EFClass
            {
                Id = id,
                Name = name,
                ClassStudents = cs != null ? new List<EFClassStudent>() : null,
                ClassTeachers = ct != null ? new List<EFClassTeacher>() : null,
            };
        }
    }
}
