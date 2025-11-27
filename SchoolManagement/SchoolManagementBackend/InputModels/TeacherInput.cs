using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementBackend.InputModels
{
    public class TeacherInput
    {
        public string Name { get; set; }

        public EFTeacher ToEF()
        {
            return new EFTeacher()
            {
                Id = Guid.NewGuid(),
                Name = Name
            };
        }
    }
}
