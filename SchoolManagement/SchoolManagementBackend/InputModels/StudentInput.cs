using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementBackend.InputModels
{
    public class StudentInput
    {
        public string Name { get; set; }

        public EFStudent ToEF()
        {
            return new EFStudent
            {
                Name = Name,
                Id = Guid.NewGuid(),
            };
        }
    }
}
