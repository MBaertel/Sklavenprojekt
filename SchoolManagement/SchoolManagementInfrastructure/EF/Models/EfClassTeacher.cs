using SchoolManagementDomain.Core.Models.Students;

namespace SchoolManagementInfrastructure.EF.Models
{
    public class EFClassTeacher
    {
        public EFClass Class { get; set; }
        public Guid ClassId { get; set; }

        public EFTeacher Teacher { get; set; }
        public Guid TeacherId { get; set; }
    }
}
