using SchoolManagementDomain.Core.Models.Teachers;
using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementBackend.InputModels
{
    public class ClassInput
    {
        public string Name { get; set; }

        public List<Guid> Students { get; set; }
        public List<Guid> Teachers { get; set; }

        public EFClass ToEF()
        {
            var id = Guid.NewGuid(); 
            var name = Name;
            var cs = Students?.Select(x => new EFClassStudent(id, x)).ToList();
            var ct = Teachers?.Select(x => new EFClassTeacher(id, x)).ToList();

            return new EFClass
            {
                Id = id,
                Name = name,
                ClassStudents = cs,
                ClassTeachers = ct,
            };
        }
    }
}
