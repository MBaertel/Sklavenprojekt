using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementBackend.OutputModels
{
    public class BaseSubjectDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public static BaseSubjectDTO FromEf(EFBaseSubject efBaseSubject)
        {
            return new BaseSubjectDTO
            {
                Id = efBaseSubject.Id,
                Name = efBaseSubject.Name,
            };
        }
    }
}
