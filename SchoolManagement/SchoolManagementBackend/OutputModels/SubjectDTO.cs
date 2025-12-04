using SchoolManagementDomain.Core.Models.Subjects;
using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementBackend.OutputModels
{
    public class SubjectDTO
    {
        public Guid Id { get; set; }

        public BaseSubjectDTO BaseSubject { get; set; }

        public static SubjectDTO FromEf(EFSubject efSubject)
        {
            return new SubjectDTO
            {
                Id = efSubject.Id,
                BaseSubject = BaseSubjectDTO.FromEf(efSubject.BaseSubject),
            };
        }
    }
}
