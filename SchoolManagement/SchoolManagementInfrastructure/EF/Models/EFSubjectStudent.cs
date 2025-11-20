namespace SchoolManagementInfrastructure.EF.Models
{
    public class EFSubjectStudent
    {
        public EFSubject Subject { get; set; }
        public Guid SubjectId { get; set; }

        public EFStudent Student { get; set; }
        public Guid StudentId { get; set; }
    }
}
