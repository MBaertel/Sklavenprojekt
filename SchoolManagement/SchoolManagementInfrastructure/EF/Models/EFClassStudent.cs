namespace SchoolManagementInfrastructure.EF.Models
{
    public class EFClassStudent
    {
        public EFClass Class { get; set; }
        public Guid ClassId { get; set; }

        public EFStudent Student { get; set; }
        public Guid StudentId { get; set; }

        public EFClassStudent() { }

        public EFClassStudent(Guid classId, Guid studentId)
        {
            ClassId = classId;
            StudentId = studentId;
        }
    }
}
